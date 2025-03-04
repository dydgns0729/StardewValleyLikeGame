using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ToolbarItemPanel : ItemPanel
    {
        #region Variables
        [SerializeField] ToolbarController toolbarController;  // 툴바 컨트롤러 참조 변수
        int selectedTool;                                      // 현재 선택된 툴의 인덱스
        #endregion

        private void Start()
        {
            toolbarController.onChanged += Highlight;  // 툴바 변경 시 호출할 이벤트 추가

            Highlight(0); // 초기 선택된 툴을 표시
        }

        public override void OnClick(int id)
        {
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