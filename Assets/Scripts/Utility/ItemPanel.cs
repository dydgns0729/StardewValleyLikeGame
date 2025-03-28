using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ItemPanel : MonoBehaviour
    {
        #region Variables
        public ItemContainer inventory;  // 인벤토리 데이터 (ScriptableObject)

        public List<InventoryButton> inventoryButtons;  // UI 버튼 리스트 (슬롯 역할)

        public List<TextMeshProUGUI> numberTexts;       // 인벤토리 버튼의 넘버링을 표시하는 TextMeshProUGUI 컴포넌트 리스트
        #endregion

        private void Awake()
        {
            // 각 버튼에 인덱스를 설정
            SetIndex();
        }

        public virtual void OnEnable()
        {
            // 현재 인벤토리 데이터를 UI에 반영
            Show();
            //inventory.inventoryChanged += Show;  // 인벤토리 변경 시 UI 갱신
        }

        private void Start()
        {
            Numbering();
        }

        public void Numbering()
        {
            // 자식 오브젝트에서 "NumberText"라는 이름을 가진 TextMeshProUGUI 컴포넌트들을 가져와 리스트에 저장합니다.
            numberTexts = GetComponentsInChildren<TextMeshProUGUI>().Where(textBox => textBox.name == "NumberText").ToList();

            // numberTexts 리스트의 각 항목에 대해 순차적으로 처리합니다.
            for (int i = 0; i < numberTexts.Count; i++)
            {
                // 번호를 문자열로 변환하여 텍스트에 설정합니다. (1부터 시작하여 i+1로 설정)
                numberTexts[i].text = (i + 1).ToString();

                // 9 이상인 인덱스에 대해서 특별한 텍스트 설정을 합니다.
                if (i >= 9)
                {
                    switch (i)
                    {
                        // 9번째 인덱스는 "0"으로 설정
                        case 9:
                            numberTexts[i].text = "0";
                            break;

                        // 10번째 인덱스는 "-"로 설정
                        case 10:
                            numberTexts[i].text = "-";
                            break;

                        // 11번째 인덱스는 "="로 설정
                        case 11:
                            numberTexts[i].text = "=";
                            break;
                    }
                }
            }
        }

        public virtual void OnDisable()
        {
            //inventory.inventoryChanged -= Show;  // 이벤트 제거
        }

        // 인벤토리 슬롯의 인덱스를 InventoryButton에 설정
        private void SetIndex()
        {
            // InventoryPanel의 자식 오브젝트 중 InventoryButton 컴포넌트를 모두 찾아 리스트에 추가
            inventoryButtons.AddRange(this.transform.GetComponentsInChildren<InventoryButton>());
            #region 원본
            //for (int i = 0; i < inventory.slots.Count && i < inventoryButtons.Count; i++)
            //{
            //    inventoryButtons[i].SetIndex(i);  // 각 버튼에 인덱스 부여
            //}
            #endregion
            for (int i = 0; i < inventoryButtons.Count; i++)
            {
                inventoryButtons[i].SetIndex(i);  // 각 버튼에 인덱스 부여
            }
        }

        // UI를 갱신하여 인벤토리 데이터를 반영하는 함수
        public virtual void Show()
        {
            #region 원본
            //for (int i = 0; i < inventory.slots.Count; i++)
            //{
            //    // 인벤토리 슬롯에 아이템이 존재하는 경우
            //    if (inventory.slots[i].item != null)
            //    {
            //        inventoryButtons[i].SetItem(inventory.slots[i]);  // 해당 슬롯의 아이템을 버튼에 설정
            //    }
            //    else
            //    {
            //        inventoryButtons[i].Clean();  // 빈 슬롯이면 UI 버튼을 초기화
            //    }
            //}
            #endregion
            for (int i = 0; i < inventory.slots.Count && i < inventoryButtons.Count; i++)
            {
                // 인벤토리 슬롯에 아이템이 존재하는 경우
                if (inventory.slots[i].item != null)
                {
                    inventoryButtons[i].SetItem(inventory.slots[i]);  // 해당 슬롯의 아이템을 버튼에 설정
                }
                else
                {
                    inventoryButtons[i].Clean();  // 빈 슬롯이면 UI 버튼을 초기화
                }
            }
        }

        public virtual void OnClick(int id)
        {

        }

        public virtual void OnDragStart(int id)
        {

        }

        public virtual void OnDragEnd(int id)
        {

        }
    }
}