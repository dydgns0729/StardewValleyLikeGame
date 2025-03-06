using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyStardewValleylikeGame
{
    public class ItemDragAndDropController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject itemIcon;   // 아이템 아이콘을 나타내는 GameObject
        private ItemSlot itemSlot = new ItemSlot();     // 드래그 및 드롭을 위한 현재 아이템 슬롯
        private RectTransform iconTransform;            // 아이콘의 RectTransform (UI 위치 및 크기)
        private Image itemIconImage;                    // 아이콘의 이미지 컴포넌트
        public bool isDraging = false;                  // 드래그 중인지 여부를 나타내는 플래그
        #endregion

        // 아이콘이 활성화되어 있는지 여부를 반환하는 프로퍼티
        public bool IsItemClicked => itemIcon.activeInHierarchy;

        private void Start()
        {
            //참조 변수
            iconTransform = itemIcon.GetComponent<RectTransform>();
            itemIconImage = itemIcon.GetComponent<Image>();
        }

        private void Update()
        {
            // 아이콘이 활성화되어 있으면
            if (itemIcon.activeInHierarchy)
            {
                // 아이콘의 위치를 마우스 위치로 업데이트
                iconTransform.position = Input.mousePosition;

                // 마우스 왼쪽 버튼 클릭 시, UI를 벗어난 경우 아이템을 드롭
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    DropItem(); // 아이템 드롭 메서드 호출
                }
            }
        }

        // 슬롯 클릭 시 호출되는 메서드
        public void OnClick(ItemSlot clickedSlot)
        {
            // 현재 슬롯에 아이템이 없으면
            if (itemSlot.item == null)
            {
                itemSlot.Copy(clickedSlot);        // 클릭한 슬롯의 아이템 정보를 복사
                clickedSlot.Clear();               // 클릭한 슬롯을 비움
            }
            else // 현재 슬롯에 아이템이 있을 경우
            {
                Item tempItem = clickedSlot.item;  // 클릭한 슬롯의 아이템을 임시 변수에 저장
                int tempCount = clickedSlot.count; // 클릭한 슬롯의 아이템 개수를 임시 변수에 저장
                clickedSlot.Copy(itemSlot);        // 현재 슬롯의 아이템 정보를 클릭한 슬롯으로 복사
                itemSlot.Set(tempItem, tempCount); // 임시 변수의 아이템 정보를 현재 슬롯에 설정
            }
            UpdateIcon();                          // 아이콘 업데이트 메서드 호출
            #region 인벤토리와 툴바가 같이 활성화시 사용 추후 수정시 삭제 필

            GameManager.Instance.inventoryContainer.inventoryChanged?.Invoke(); // 인벤토리 변경 이벤트 호출

            #endregion
        }

        // 아이콘 업데이트 메서드
        private void UpdateIcon()
        {
            itemIcon.SetActive(itemSlot.item != null);                            // 현재 슬롯에 아이템이 없으면 아이콘 비활성화, 있으면 활성화
            if (itemSlot.item != null) itemIconImage.sprite = itemSlot.item.icon; // 아이콘 이미지 설정
        }
        #region 드래그 앤 드롭 기능 구현
        // 드래그 시작 시 호출되는 메서드
        public void OnDragStart(ItemSlot draggedSlot)
        {
            isDraging = true;     // 드래그 중 플래그 설정
            OnClick(draggedSlot); // 드래그하는 슬롯 클릭 처리
        }

        // 인벤토리 UI에 드롭 시 호출되는 메서드
        public void DropInInventoryUI(ItemSlot targetSlot)
        {
            isDraging = false;   // 드래그 중 플래그 해제
            OnClick(targetSlot); // 타겟 슬롯 클릭 처리
        }

        // 드래그 종료 시 호출되는 메서드
        public void OnEndDrag()
        {
            isDraging = false; // 드래그 중 플래그 해제
            DropItem();        // 아이템 드롭 메서드 호출
        }

        // 아이템 드롭 메서드
        private void DropItem()
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);        // 마우스 위치를 3D 월드 좌표로 변환
            worldPosition.z = 0f;                                                               // z 값 0으로 설정 (2D 게임을 위해)
            ItemSpawnManager.Instance.SpawnItem(worldPosition, itemSlot.item, itemSlot.count);  // 아이템을 월드에 드롭
            itemSlot.Clear();                                                                   // 현재 슬롯 비우기
            UpdateIcon();                                                                       // 아이콘 업데이트 메서드 호출
        }
        #endregion
    }
}
