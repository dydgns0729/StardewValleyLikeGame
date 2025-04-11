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
        int selectedTool;                                      // 현재 선택된 툴의 인덱스
        #endregion

        public override void OnEnable()
        {
            base.OnEnable();
            // 인벤토리 데이터 변경 시 UI 갱신
            inventory.inventoryChanged += Show;
        }

        private void Start()
        {
            toolbarController.onChanged += Highlight;  // 툴바 변경 시 호출할 이벤트 추가

            Highlight(0); // 초기 선택된 툴을 표시

            Numbering();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            // 인벤토리 데이터 변경 시 UI 갱신
            inventory.inventoryChanged -= Show;
        }

        public override void OnClick(int id)
        {
            Debug.Log("ToolbarItemPanel.OnClick() 1");
            // 드래그 앤 드롭 중인 아이템이 있는 경우
            if (GameManager.Instance.dragAndDropController.IsItemClicked)
            {
                Debug.Log("ToolbarItemPanel.OnClick() 2");
                // 드래그 앤 드롭 컨트롤러의 클릭 처리 메서드 호출
                GameManager.Instance.dragAndDropController.OnClick(inventory.slots[id]);
                return;
            }
            Debug.Log("ToolbarItemPanel.OnClick() 3");
            // 툴바 컨트롤러에 선택된 툴의 인덱스를 설정
            toolbarController.SetSelectedTool(id);
            // 선택된 아이템을 표시
            Highlight(id);
        }

        public override void OnDragStart(int id)
        {
            GameManager.Instance.dragAndDropController.OnDragStart(inventory.slots[id]);
            Show();
        }

        public override void OnDragEnd(int id)
        {
            GameManager.Instance.dragAndDropController.DropInInventoryUI(inventory.slots[id]);
            Show();
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