using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyStardewValleylikeGame
{
    public class ToolbarController : MonoBehaviour
    {
        #region Variables
        [SerializeField] int toolbarSize = 12; // 툴바 크기 (툴 슬롯 개수)
        int selectedTool;                      // 현재 선택된 툴의 인덱스

        public Action<int> onChanged;           // 툴바 변경 시 호출할 이벤트

        [SerializeField] IconHighlight iconHighlight; // 아이콘 하이라이트를 참조하는 변수


        public Item GetItem                     // 현재 선택된 툴바의 아이템을 가져오는 프로퍼티
        {
            get
            {
                // GameManager의 인벤토리 데이터에서 현재 선택된 툴의 아이템을 가져옴
                return GameManager.Instance.inventoryContainer.slots[selectedTool].item;
            }
        }
        #region KeyMapping 추가
        private Dictionary<KeyCode, int> toolMappings = new()
        {
            { KeyCode.Alpha1, 0 }, { KeyCode.Alpha2, 1 }, { KeyCode.Alpha3, 2 },
            { KeyCode.Alpha4, 3 }, { KeyCode.Alpha5, 4 }, { KeyCode.Alpha6, 5 },
            { KeyCode.Alpha7, 6 }, { KeyCode.Alpha8, 7 }, { KeyCode.Alpha9, 8 },
            { KeyCode.Alpha0, 9 }, { KeyCode.Minus, 10 }, { KeyCode.Equals, 11 }
        };
        #endregion

        #endregion

        private void Start()
        {
            onChanged += UpdateHighlightIcon;
            UpdateHighlightIcon(selectedTool);
        }

        private void Update()
        {
            CheckNumberInput(); // 숫자 키 입력을 체크하는 메서드 호출
            CheckNumberWheel(); // 마우스 휠 입력을 체크하는 메서드 호출
        }

        public void SetSelectedTool(int id)
        {
            selectedTool = id;
            onChanged?.Invoke(selectedTool);
        }

        private void CheckNumberWheel()
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
                SetSelectedTool(selectedTool);
            }
        }

        // 숫자 키 입력을 체크하는 메서드
        private void CheckNumberInput()
        {
            foreach (var kvp in toolMappings)
            {
                if (Input.GetKeyDown(kvp.Key))
                {
                    selectedTool = kvp.Value;
                    break; // 첫 번째로 눌린 키만 처리
                }
            }
            SetSelectedTool(selectedTool);
        }

        // 하이라이트 아이콘을 업데이트하는 메서드
        void UpdateHighlightIcon(int id)
        {
            // 현재 아이템을 가져옴
            Item item = GetItem;
            // 아이템이 없으면 하이라이트를 숨기고 종료
            if (item == null)
            {
                iconHighlight.Set(null); // 아이콘을 비움
                return;
            }

            // 하이라이트 표시 여부 설정
            iconHighlight.Show = item.iconHighlight;

            // 아이템이 하이라이트 표시 상태이면 해당 아이콘으로 설정
            if (item.iconHighlight)
            {
                iconHighlight.Set(item.icon); // 하이라이트 아이콘 설정
            }
        }
    }
}