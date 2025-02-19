using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // 아이템을 저장하는 슬롯 클래스
    [Serializable]
    public class ItemSlot
    {
        public Item item;  // 현재 슬롯에 저장된 아이템
        public int count;  // 해당 아이템의 개수 (스택 가능할 경우 사용)

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
        }
    }
}
