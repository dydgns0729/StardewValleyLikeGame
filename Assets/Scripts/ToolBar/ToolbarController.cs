using System;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ToolbarController : MonoBehaviour
    {
        #region Variables
        [SerializeField] int toolbarSize = 12; // 툴바 크기 (툴 슬롯 개수)
        int selectedTool;                      // 현재 선택된 툴의 인덱스

        public Action<int> onChanged;           // 툴바 변경 시 호출할 이벤트

        public Item GetItem                     // 현재 선택된 툴바의 아이템을 가져오는 프로퍼티
        {
            get
            {
                // GameManager의 인벤토리 데이터에서 현재 선택된 툴의 아이템을 가져옴
                return GameManager.Instance.inventoryContainer.slots[selectedTool].item;
            }
        }
        #endregion

        private void Update()
        {
            float delta = Input.mouseScrollDelta.y; // 마우스 휠 입력을 감지 (위로 +, 아래로 -)

            // 마우스 휠이 움직였을 때만 실행
            if (delta != 0)
            {
                if (delta > 0) // 휠을 위로 돌렸을 때
                {
                    selectedTool += 1; // 다음 슬롯으로 이동
                    selectedTool = (selectedTool >= toolbarSize) ? 0 : selectedTool; // 끝까지 가면 처음으로
                }
                else // 휠을 아래로 돌렸을 때
                {
                    selectedTool -= 1; // 이전 슬롯으로 이동
                    selectedTool = (selectedTool < 0) ? toolbarSize - 1 : selectedTool; // 처음에서 뒤로 가면 마지막으로
                }
                //Debug.Log("셀렉트" + selectedTool);
                onChanged?.Invoke(selectedTool); // 변경 이벤트 호출
            }
        }

        public void SetSelectedTool(int id)
        {
            selectedTool = id;
        }
    }
}
