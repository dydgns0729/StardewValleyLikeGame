using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // 인벤토리 UI 패널을 관리하는 클래스
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField]
        ItemContainer inventory;  // 인벤토리 데이터 (ScriptableObject)

        [SerializeField]
        List<InventoryButton> inventoryButtons;  // UI 버튼 리스트 (슬롯 역할)

        private void Awake()
        {
            // 각 버튼에 인덱스를 설정
            SetIndex();
        }

        private void OnEnable()
        {
            // 현재 인벤토리 데이터를 UI에 반영
            Show();
        }

        // 인벤토리 슬롯의 인덱스를 InventoryButton에 설정
        private void SetIndex()
        {
            // InventoryPanel의 자식 오브젝트 중 InventoryButton 컴포넌트를 모두 찾아 리스트에 추가
            inventoryButtons.AddRange(this.transform.GetComponentsInChildren<InventoryButton>());
            for (int i = 0; i < inventory.slots.Count; i++)
            {
                inventoryButtons[i].SetIndex(i);  // 각 버튼에 인덱스 부여
            }
        }

        // UI를 갱신하여 인벤토리 데이터를 반영하는 함수
        private void Show()
        {
            for (int i = 0; i < inventory.slots.Count; i++)
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
