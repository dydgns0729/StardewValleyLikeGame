using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ToolbarItemPanel : ItemPanel
    {
        #region Variables
        [SerializeField] ToolbarController toolbarController;  // 툴바 컨트롤러 참조 변수
        List<TextMeshProUGUI> numberTexts;                     // 인벤토리 버튼의 넘버링을 표시하는 TextMeshProUGUI 컴포넌트 리스트
        int selectedTool;                                      // 현재 선택된 툴의 인덱스
        #endregion

        private void Start()
        {
            toolbarController.onChanged += Highlight;  // 툴바 변경 시 호출할 이벤트 추가

            Highlight(0); // 초기 선택된 툴을 표시

            #region numberTexts 추가
            numberTexts = GetComponentsInChildren<TextMeshProUGUI>().Where(textBox => textBox.name == "NumberText").ToList();
            for (int i = 0; i < numberTexts.Count; i++)
            {
                numberTexts[i].text = (i + 1).ToString();
                if (i >= 9)
                {
                    switch (i)
                    {
                        case 9:
                            numberTexts[i].text = "0";
                            break;
                        case 10:
                            numberTexts[i].text = "-";
                            break;
                        case 11:
                            numberTexts[i].text = "=";
                            break;
                    }
                }
            }

            #endregion
        }

        public override void OnClick(int id)
        {
            // 드래그 앤 드롭 중인 아이템이 있는 경우
            if (GameManager.Instance.dragAndDropController.IsItemClicked)
            {
                // 드래그 앤 드롭 컨트롤러의 클릭 처리 메서드 호출
                GameManager.Instance.dragAndDropController.OnClick(inventory.slots[id]);
                return;
            }

            // 툴바 컨트롤러에 선택된 툴의 인덱스를 설정
            toolbarController.SetSelectedTool(id);
            // 선택된 아이템을 표시
            Highlight(id);
        }

        // 툴바에 현재 선택된 아이템을 표시하는 메서드
        public void Highlight(int id)
        {
            // 이전에 선택된 툴의 하이라이트를 해제
            inventoryButtons[selectedTool].Highlight(false);
            // 선택된 툴의 인덱스를 설정
            selectedTool = id;
            // 선택된 툴을 하이라이트
            inventoryButtons[selectedTool].Highlight(true);
        }
    }
}