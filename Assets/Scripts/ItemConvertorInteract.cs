using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // 아이템 변환기 인터랙션 클래스
    public class ItemConvertorInteract : Interactable
    {
        #region Variables
        [SerializeField] Item convertableItem; // 변환 가능한 아이템
        [SerializeField] Item producedItem; // 생산된 아이템
        [SerializeField] int produceditemCount = 1; // 생산된 아이템의 수량

        Animator animator; // 애니메이터 컴포넌트

        ItemSlot itemSlot; // 아이템 슬롯

        [SerializeField] float timeToProcess = 5f; // 아이템 변환에 걸리는 시간
        float timer; // 타이머 변수
        #endregion

        // 시작 시 초기화
        private void Start()
        {
            itemSlot = new ItemSlot(); // 새로운 아이템 슬롯 생성
            animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
        }

        // 매 프레임마다 호출되는 업데이트 함수
        private void Update()
        {
            if (itemSlot == null) return; // 아이템 슬롯이 없으면 종료

            if (timer > 0f) // 타이머가 0보다 크면
            {
                timer -= Time.deltaTime; // 타이머 감소
                if (timer <= 0f) // 타이머가 끝나면
                {
                    CompleteItemConversion(); // 아이템 변환 완료
                }
            }
        }

        // 아이템 변환 완료 후 실행되는 함수
        private void CompleteItemConversion()
        {
            animator.SetBool("Working", false); // 작업 상태 애니메이션 종료
            itemSlot.Clear(); // 아이템 슬롯 비우기
            itemSlot.Set(producedItem, produceditemCount); // 변환된 아이템 설정
        }

        // 캐릭터가 상호작용할 때 호출되는 함수
        public override void Interact(Character character)
        {
            // 아이템 슬롯이 비어있으면
            if (itemSlot.item == null)
            {
                // 드래그 앤 드롭으로 변환 가능한 아이템이 있으면 변환 시작
                if (GameManager.Instance.dragAndDropController.Check(convertableItem))
                {
                    StartItemProcessing(); // 아이템 처리 시작
                }
            }

            // 아이템 슬롯에 아이템이 있을 경우, 타이머가 0 이하일 때
            if (itemSlot.item != null && timer < 0f)
            {
                // 아이템을 인벤토리에 추가하고, 아이템 슬롯 비우기
                GameManager.Instance.inventoryContainer.Add(itemSlot.item, itemSlot.count);
                itemSlot.Clear();
            }
        }

        // 아이템 처리를 시작하는 함수
        private void StartItemProcessing()
        {
            animator.SetBool("Working", true); // 작업 중 애니메이션 활성화

            // 드래그 앤 드롭 컨트롤러의 아이템 슬롯 정보를 복사
            itemSlot.Copy(GameManager.Instance.dragAndDropController.itemSlot);
            GameManager.Instance.dragAndDropController.RemoveItem(); // 아이템 제거

            timer = timeToProcess; // 타이머 설정
        }
    }
}
