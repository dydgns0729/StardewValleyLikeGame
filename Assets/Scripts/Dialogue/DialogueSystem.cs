using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyStardewValleylikeGame
{
    /// <summary>
    /// 게임 내에서 NPC와의 대화를 관리하는 시스템
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        // 대화 내용을 표시할 UI 텍스트 (대사)
        [SerializeField] TextMeshProUGUI targetText;

        // NPC의 이름을 표시할 UI 텍스트 (이름)
        [SerializeField] TextMeshProUGUI nameText;

        // NPC의 초상화를 표시할 UI 이미지
        [SerializeField] Image portrait;

        // 현재 진행 중인 대화 컨테이너
        DialogueContainer currentDialogue;

        // 현재 대화에서 몇 번째 줄을 출력 중인지 나타내는 변수
        int currentTextLine;

        #region 텍스트가 한글자씩 노출되게 만들어주는데 필요한 변수
        [Range(0, 1f)]
        [SerializeField] float visibleTextPercent;      // 현재까지 출력된 텍스트의 비율 (0 ~ 1)
        [SerializeField] float timePerLetter = 0.05f;   // 한 글자당 출력되는 시간 (0.05초)

        float totalTimeToType; // 현재 문장을 출력하는 데 걸리는 총 시간
        float currentTime; // 현재까지 경과된 시간
        string lineToShow; // 현재 출력할 대사
        #endregion

        private void Update()
        {
            // 마우스 왼쪽 버튼을 클릭하면 다음 대사 출력
            if (Input.GetMouseButtonDown(0))
            {
                PushText();
            }
            // 타이핑 효과 적용 (한 글자씩 출력)
            TypeOutText();
        }

        /// <summary>
        /// 현재 진행 중인 대사의 타이핑 효과를 적용하는 메서드
        /// </summary>
        private void TypeOutText()
        {
            // 대사 출력이 끝난 경우 (더 이상 글자를 추가할 필요 없음)
            if (visibleTextPercent >= 1f) return;
            // 현재 시간을 누적하여 진행도 계산
            currentTime += Time.deltaTime;
            // 현재 진행도를 0~1 사이 값으로 보정 (0% ~ 100%)
            visibleTextPercent = Mathf.Clamp01(currentTime / totalTimeToType);
            // 화면에 보이는 텍스트 업데이트
            UpdateText();
        }

        /// <summary>
        /// 현재 visibleTextPercent 값을 기반으로 화면에 보이는 텍스트를 업데이트하는 메서드
        /// </summary>
        void UpdateText()
        {
            // 현재까지 출력해야 할 글자 개수 계산
            int letterCount = (int)(lineToShow.Length * visibleTextPercent);
            // 최적화: TMP의 maxVisibleCharacters 활용 (새로운 문자열을 생성하지 않음)
            targetText.maxVisibleCharacters = letterCount;
        }

        /// <summary>
        /// 다음 대사를 출력하는 메서드
        /// </summary>
        private void PushText()
        {
            // 만약 타이핑 효과가 아직 끝나지 않았다면, 전체 문장을 한 번에 표시
            if (visibleTextPercent < 1f)
            {
                visibleTextPercent = 1f;
                UpdateText();
                return;
            }

            // 대사가 모두 출력되었으면 대화 종료
            if (currentTextLine >= currentDialogue.line.Count)
            {
                Conclude();
            }
            else
            {
                // 다음 대사를 설정하고 타이핑 효과 적용
                CycleLine();
            }
        }

        /// <summary>
        /// 현재 대사 내용을 초기화하고 타이핑 효과를 적용하는 메서드
        /// </summary>
        void CycleLine()
        {
            // 현재 대사 설정
            lineToShow = currentDialogue.line[currentTextLine];

            // 대사의 총 출력 시간 계산 (한 글자당 timePerLetter초 걸림)
            totalTimeToType = lineToShow.Length * timePerLetter;

            // 진행 시간 초기화
            currentTime = 0f;

            // 텍스트 진행도를 0으로 초기화하여 처음부터 출력
            visibleTextPercent = 0f;

            // 전체 문장을 먼저 적용한 뒤, maxVisibleCharacters로 가시성을 조절
            targetText.text = lineToShow;
            targetText.maxVisibleCharacters = 0; // 첫 글자부터 출력되도록 초기화

            // 현재 대화의 인덱스 증가 (다음 클릭 시 다음 대사로 이동)
            currentTextLine++;
        }

        /// <summary>
        /// 대화 시스템을 초기화하고 대화를 시작하는 메서드
        /// </summary>
        /// <param name="dialogueContainer">진행할 대화 데이터</param>
        public void Initialize(DialogueContainer dialogueContainer)
        {
            // 대화 UI 활성화
            Show(true);

            // 현재 대화 설정
            currentDialogue = dialogueContainer;
            // 대화의 시작 인덱스 설정
            currentTextLine = 0;
            // 첫 번째 대사 설정 및 출력
            CycleLine();

            // 초상화 및 이름 업데이트
            UpdatePortrait();
        }

        // 초상화와 이름을 업데이트하는 메서드
        private void UpdatePortrait()
        {
            // Actor 스크립터블 오브젝트에 저장된 정보를 UI에 반영
            portrait.sprite = currentDialogue.actor.portrait;
            nameText.text = currentDialogue.actor.name;
        }

        /// <summary>
        /// 대화창의 활성화 여부를 설정하는 메서드
        /// </summary>
        /// <param name="b">true: 활성화 / false: 비활성화</param>
        private void Show(bool b)
        {
            gameObject.SetActive(b);
        }

        /// <summary>
        /// 대화를 종료하는 메서드
        /// </summary>
        private void Conclude()
        {
            // 대화 UI 비활성화
            Show(false);
        }
    }
}