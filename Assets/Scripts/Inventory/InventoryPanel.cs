using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // 인벤토리 UI 패널을 관리하는 클래스
    public class InventoryPanel : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        ItemContainer inventory;  // 인벤토리 데이터 (ScriptableObject)

        [SerializeField]
        List<InventoryButton> inventoryButtons;  // UI 버튼 리스트 (슬롯 역할)
        #endregion

        private void Awake()
        {
            // 각 버튼에 인덱스를 설정
            SetIndex();
        }

        private void OnEnable()
        {
            // 현재 인벤토리 데이터를 UI에 반영
            Show();
            inventory.inventoryChanged += Show;  // 인벤토리 변경 시 UI 갱신
        }

        private void OnDisable()
        {
            inventory.inventoryChanged -= Show;  // 이벤트 제거
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
        public void Show()
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

            for (int i = 0; i < inventoryButtons.Count; i++)
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
    }
}
