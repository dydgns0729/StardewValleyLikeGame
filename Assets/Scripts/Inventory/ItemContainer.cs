using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // 아이템을 저장하는 슬롯 클래스
    [Serializable]
    public class ItemSlot
    {
        #region Variables
        public Item item;  // 현재 슬롯에 저장된 아이템
        public int count;  // 해당 아이템의 개수 (스택 가능할 경우 사용)
        #endregion

        //아이템 슬롯에 정보를 복사하는 함수
        public void Copy(ItemSlot itemSlot)
        {
            item = itemSlot.item;
            count = itemSlot.count;
        }

        //아이템 슬롯에 아이템과 개수를 설정하는 함수
        public void Set(Item item, int count)
        {
            this.item = item;
            this.count = count;
        }

        //아이템 슬롯을 초기화하는 함수
        public void Clear()
        {
            item = null;
            count = 0;
        }
    }

    // 아이템 컨테이너를 ScriptableObject로 생성 (아이템 저장 용도)
    [CreateAssetMenu(menuName = "Data/Item Container")]
    public class ItemContainer : ScriptableObject
    {
        public List<ItemSlot> slots;  // 인벤토리 슬롯 리스트 (아이템을 담을 공간)

        public Action inventoryChanged;  // 인벤토리 변경 시 호출할 델리게이트


        // 아이템을 추가하는 함수
        public void Add(Item item, int count = 1)
        {
            // 아이템이 스택 가능한 경우
            if (item.stackable)
            {
                // 이미 동일한 아이템이 존재하는 슬롯 찾기
                ItemSlot itemSlot = slots.Find(slot => slot.item == item);
                if (itemSlot != null)
                {
                    // 같은 아이템이 존재하면 개수만 증가
                    itemSlot.count += count;
                }
                else
                {
                    // 동일한 아이템이 없으면 빈 슬롯(아이템이 null인 곳) 찾기
                    itemSlot = slots.Find(slot => slot.item == null);
                    if (itemSlot != null)
                    {
                        // 빈 슬롯에 아이템 추가 및 개수 설정
                        itemSlot.item = item;
                        itemSlot.count = count;
                    }
                }
            }
            else  // 아이템이 스택 불가능한 경우 (예: 무기, 도구 등)
            {
                // 빈 슬롯(아이템이 null인 곳) 찾기
                ItemSlot itemSlot = slots.Find(slot => slot.item == null);
                if (itemSlot != null)
                {
                    // 빈 슬롯에 아이템 추가 (개수는 필요 없음)
                    itemSlot.item = item;
                }
            }
            inventoryChanged?.Invoke();  // 인벤토리 변경 이벤트 호출
        }

        // 아이템을 제거하는 함수
        public void Remove(Item itemToRemove, int count = 1)
        {
            if (slots == null || itemToRemove == null) return;

            if (itemToRemove.stackable)
            {
                //전체 개수를 먼저 확인
                int totalItemCount = slots.Where(slot => slot.item == itemToRemove)
                                          .Sum(slot => slot.count);
                if (totalItemCount < count)
                {
                    Debug.Log("사용하려는 아이템이 가지고 있는 아이템보다 많습니다");
                    return;
                } // 개수가 부족하면 제거하지 않음

                // 아이템이 스택 가능한 경우, 해당 아이템을 가진 모든 슬롯 찾기
                List<ItemSlot> itemSlots = slots.FindAll(slot => slot.item == itemToRemove);
                if (itemSlots.Count == 0) return; // 아이템이 없으면 종료

                // 가져온 슬롯들을 순회하면서 개수만큼 제거
                foreach (ItemSlot itemSlot in itemSlots)
                {
                    if (count <= 0) break; // 필요한 만큼 다 뺐으면 종료

                    // 현재 슬롯의 개수가 필요한 개수보다 작거나 같으면
                    if (itemSlot.count <= count)
                    {
                        count -= itemSlot.count; // 부족한 개수만큼 계속 감소
                        itemSlot.Clear(); // 슬롯 비우기
                    }
                    else
                    {
                        itemSlot.count -= count;
                        break; // 필요한 만큼 다 뺐으므로 루프 종료
                    }
                }
            }
            else
            {
                // 아이템이 스택 불가능한 경우, 해당 아이템을 가진 슬롯 찾아서 제거
                ItemSlot itemSlot = slots.Find(slot => slot.item == itemToRemove);
                // 해당 아이템을 가진 슬롯이 존재하면
                if (itemSlot != null)
                {
                    // 슬롯 비우기
                    itemSlot.Clear();
                }
            }

            inventoryChanged?.Invoke(); // UI 업데이트
        }

        internal bool CheckFreeSpace()
        {
            // slots 리스트를 순회하여 각 슬롯의 아이템이 비어 있는지 확인합니다.
            for (int i = 0; i < slots.Count; i++)
            {
                // 슬롯에 아이템이 없다면 (즉, 비어 있다면) true를 반환하여 빈 공간이 있음을 알려줍니다.
                if (slots[i].item == null)
                {
                    return true;
                }
            }
            // 모든 슬롯에 아이템이 존재하면 false를 반환하여 빈 공간이 없음을 알려줍니다.
            return false;
        }

        internal bool CheckItem(ItemSlot checkingItem)
        {
            // slots 리스트에서 checkingItem과 동일한 아이템을 가진 슬롯을 찾습니다.
            ItemSlot itemSlot = slots.Find(slot => slot.item == checkingItem.item);

            // 해당 아이템이 존재하지 않으면 false를 반환합니다.
            if (itemSlot == null) return false;

            // 아이템이 스택 가능한 경우, 슬롯에 있는 수량이 필요한 수량 이상인지 확인합니다.
            if (checkingItem.item.stackable)
            {
                return itemSlot.count >= checkingItem.count;
            }

            // 아이템이 스택 불가능한 경우, 슬롯에 해당 아이템이 존재하면 true를 반환합니다.
            return true;
        }

        #region 250324 스크립터블 오브젝트가 자동으로 저장되지 않는 문제 해결을 위한 코드 추가
        // 인벤토리 저장
        public void SaveInventory()
        {
            // 에셋이 수정되었음을 Unity에 알리기
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);  // 수정된 상태를 Unity에 알림
            AssetDatabase.SaveAssets();  // 에셋에 저장
            Debug.Log("Inventory saved manually.");
#endif
        }
        #endregion

    }
}
