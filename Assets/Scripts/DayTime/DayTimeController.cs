using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MyStardewValleylikeGame
{
    public class DayTimeController : MonoBehaviour
    {
        #region Variables
        // 하루(24시간)를 초단위로 계산
        private const float SecondsInDay = 86400f;

        private float time; // 현재 시간

        // 시간 스케일 조정 (게임 내 시간이 실제 시간보다 빨리 흐를 수 있도록 설정)
        [SerializeField] private float timeScale = 60f;

        // 게임 시작 시 설정할 초기 시간 (6시)
        [SerializeField] private float startAtTime = 21600f;

        // 밤과 낮의 색을 정의할 컬러 변수
        [SerializeField] private Color nightLightColor; // 밤 컬러
        [SerializeField] private Color dayLightColor;   // 낮 컬러

        // 밤-낮 색 전환을 위한 애니메이션 커브 (시간에 따라 색을 변화시킬 때 사용)
        [SerializeField] private AnimationCurve nightTimeCurve;

        // 화면에 표시할 시간 텍스트 (시간을 나타내는 UI)
        [SerializeField] private TextMeshProUGUI timeText;

        // 글로벌 라이트 (장면 전체에 적용되는 라이트, 밤과 낮의 밝기를 조절)
        [SerializeField] private Light2D globalLight;

        // 게임이 시작될 때의 날 수 (매일 1씩 증가)
        [SerializeField] private int days = 0;

        // 색상 계산을 최소화하기 위한 변수들 (성능 최적화)
        private float previousCurveValue = -1f;  // 이전의 커브 값
        private Color previousColor; // 이전에 적용된 라이트 색상

        // 시간과 분을 계산하는 프로퍼티들
        int Hours
        {
            get { return (int)(time / 3600f); } // 시간 단위 계산 (초 -> 시간)
        }

        int Minutes
        {
            get { return (int)(time % 3600 / 60f); } // 분 단위 계산 (초 -> 분)
        }

        // 게임 내 시간 이벤트를 관리하는 에이전트들
        List<TimeAgent> agents;

        // 특정 시간 주기마다 발생하는 이벤트 간격
        [SerializeField] private float phaseLength = 900f;

        // 이전 이벤트 단계 추적 (성능 최적화 및 중복 호출 방지)
        private int oldPhase = 0;
        #endregion

        private void Awake()
        {
            // 에이전트 리스트 초기화
            agents = new List<TimeAgent>();

            // 시간 초기화 (게임 시작 시간)
            time = startAtTime;
        }

        // 에이전트를 구독 (게임 내 특정 이벤트를 추적하는 에이전트 등록)
        public void Subscribe(TimeAgent agent)
        {
            agents.Add(agent);
        }

        // 에이전트를 구독 취소 (이벤트 추적을 중단하는 에이전트 제거)
        public void Unsubscribe(TimeAgent agent)
        {
            agents.Remove(agent);
        }

        private void Update()
        {
            // 매 프레임 시간 업데이트 (Time.deltaTime은 프레임당 시간 차이, timeScale을 곱해 시간 흐름을 제어)
            time += Time.deltaTime * timeScale;

            // 시간 텍스트 업데이트
            UpdateTimeDisplay();

            // 라이트 색상 업데이트
            UpdateLighting();

            // 하루가 지나면 새로운 날로 전환
            if (time >= SecondsInDay)
            {
                NextDay();
            }

            // 시간에 따른 이벤트 발생 (시간 주기에 맞춰 에이전트 호출)
            TimeAgent();
        }

        private void UpdateTimeDisplay()
        {
            // UI 텍스트에 시간 표시 (Hours와 Minutes는 각각 시간과 분을 나타냄)
            timeText.text = $"{Hours:00} : {Minutes:00}";
        }

        private void UpdateLighting()
        {
            // 시간에 맞춰 밤과 낮의 색을 전환하기 위해 커브 값을 가져옴
            float curveData = nightTimeCurve.Evaluate(Hours + Minutes / 60f);

            // 이전 값과 동일하면 계산을 생략하여 성능 최적화
            if (Mathf.Approximately(curveData, previousCurveValue)) return;

            // 새로운 커브 값을 계산하고, 해당 값에 맞는 색상으로 전환
            previousCurveValue = curveData;
            previousColor = Color.Lerp(dayLightColor, nightLightColor, curveData);
            globalLight.color = previousColor; // 글로벌 라이트 색상 적용
        }

        private void NextDay()
        {
            // 하루가 끝나면 시간 초기화 및 날 수 증가
            time = 0f;
            days++;
        }

        private void TimeAgent()
        {
            // 현재 시간에 해당하는 이벤트 단계 계산 (phaseLength 간격으로 이벤트 발생)
            int phase = (int)(time / phaseLength);

            // 이전 단계와 다르면 이벤트를 호출
            if (oldPhase != phase)
            {
                oldPhase = phase;
                for (int i = 0; i < agents.Count; i++)
                {
                    agents[i].Invoke(); // 모든 구독된 에이전트 호출
                }
            }
        }
    }
}
