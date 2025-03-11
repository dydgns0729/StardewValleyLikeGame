using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MyStardewValleylikeGame
{
    public class DayTimeController : MonoBehaviour
    {
        #region Variables
        private const float SecondsInDay = 86400f;  // 하루(24시간)를 초단위로 계산
        private float time;                         // 현재 시간
        [SerializeField] private float timeScale = 60f; // 시간 스케일 조정
        [SerializeField] private float startAtTime = 21600f; // 게임 시작 시간 (6시)

        [SerializeField] private Color nightLightColor; // 밤 컬러
        [SerializeField] private Color dayLightColor;   // 낮 컬러
        [SerializeField] private AnimationCurve nightTimeCurve; // 밤-낮 색 전환 커브

        [SerializeField] private TextMeshProUGUI timeText; // UI 텍스트
        [SerializeField] private Light2D globalLight;      // 글로벌 라이트

        [SerializeField] private int days = 0;

        // 매 프레임 컬러 계산 최소화를 위한 캐싱
        private float previousCurveValue = -1f;
        private Color previousColor;

        int Hours
        {
            get { return (int)(time / 3600f); } // 시간 단위
        }

        int Minutes
        {
            get { return (int)(time % 3600 / 60f); } // 분 단위
        }

        List<TimeAgent> agents;
        [SerializeField] private float phaseLength = 900f;     //스폰체크할 시간
        private int oldPhase = 0;
        #endregion

        private void Awake()
        {
            agents = new List<TimeAgent>();
            time = startAtTime;
        }

        public void Subscribe(TimeAgent agent)
        {
            agents.Add(agent);
        }

        public void Unsubscribe(TimeAgent agent)
        {
            agents.Remove(agent);
        }

        private void Update()
        {
            // 시간 업데이트
            time += Time.deltaTime * timeScale;

            // 시간 계산
            UpdateTimeDisplay();

            // 라이트 색상 업데이트
            UpdateLighting();

            // 하루가 지나면 다음 날로 전환
            if (time >= SecondsInDay)
            {
                NextDay();
            }

            TimeAgent();
        }

        private void UpdateTimeDisplay()
        {
            // 텍스트 업데이트
            timeText.text = $"{Hours:00} : {Minutes:00}";
        }

        private void UpdateLighting()
        {
            // AnimationCurve에서 현재 값 가져오기
            float curveData = nightTimeCurve.Evaluate(Hours + Minutes / 60f);

            // 이전 값과 동일하면 계산 생략
            if (Mathf.Approximately(curveData, previousCurveValue)) return;

            // 컬러 업데이트
            previousCurveValue = curveData;
            previousColor = Color.Lerp(dayLightColor, nightLightColor, curveData);
            globalLight.color = previousColor;
        }

        private void NextDay()
        {
            time = 0f;
            days++;
        }

        private void TimeAgent()
        {
            int phase = (int)(time / phaseLength);

            if (oldPhase != phase)
            {
                oldPhase = phase;
                for (int i = 0; i < agents.Count; i++)
                {
                    agents[i].Invoke();
                }
            }
        }
    }
}