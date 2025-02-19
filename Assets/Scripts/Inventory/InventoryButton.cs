using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyStardewValleylikeGame
{
    // 인벤토리 슬롯(버튼)을 관리하는 클래스
    public class InventoryButton : MonoBehaviour, IPointerClickHandler/*, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler*/
    {
        [SerializeField]
        Image icon;  // 아이템 아이콘 이미지

        [SerializeField]
        TextMeshProUGUI text;  // 아이템 개수를 표시하는 텍스트

        //int myIndex;  // 해당 버튼의 인벤토리 슬롯 인덱스
        public int myIndex;  // 해당 버튼의 인벤토리 슬롯 인덱스

        // 슬롯의 인덱스를 설정하는 함수
        public void SetIndex(int index) => myIndex = index;

        // 슬롯에 아이템을 설정하는 함수
        public void SetItem(ItemSlot slot)
        {
            icon.gameObject.SetActive(true);  // 아이콘 활성화
            icon.sprite = slot.item.icon;  // 아이콘 이미지 설정

            if (slot.item.stackable)  // 아이템이 스택 가능한 경우
            {
                text.gameObject.SetActive(true);  // 개수 텍스트 활성화
                text.text = slot.count.ToString();  // 개수 표시
            }
            else  // 스택 불가능한 아이템 (도구 등)
            {
                text.gameObject.SetActive(false);  // 개수 텍스트 비활성화
                text.text = "";  // 텍스트 초기화
            }
        }

        // 슬롯을 초기화하는 함수 (아이템이 없는 상태로 변경)
        public void Clean()
        {
            icon.sprite = null;  // 아이콘 제거
            icon.gameObject.SetActive(false);  // 아이콘 숨기기
            text.text = "";  // 텍스트 초기화
            text.gameObject.SetActive(false);  // 텍스트 숨기기
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("클릭했음!");
            // 클릭 시 해당 슬롯의 아이템을 드래그 앤 드롭 컨트롤러에 전달
            ItemContainer inventory = GameManager.Instance.inventoryContainer;
            // 1. 마우스 클릭시 마우스 인벤토리 슬롯이 비어있는지 확인하고 결과에따라 메서드 내용을 실행한다
            GameManager.Instance.dragAndDropController.OnClick(inventory.slots[myIndex]);
            // 2. 마우스 클릭을 할 때마다 인벤토리 패널의 내용을 최신화한다
            transform.parent.GetComponent<InventoryPanel>().Show();
        }

        #region TODO :: 드래그 앤 드롭 기능 구현
        //public void OnBeginDrag(PointerEventData eventData)
        //{
        //    // 드래그 시작 시 해당 슬롯의 아이템을 드래그 앤 드롭 컨트롤러에 전달
        //    ItemContainer inventory = GameManager.Instance.inventoryContainer;
        //    GameManager.Instance.dragAndDropController.OnClick(inventory.slots[myIndex]);
        //    transform.parent.GetComponent<InventoryPanel>().Show();
        //    Debug.Log("드래그 시작!");
        //}

        //public void OnDrag(PointerEventData eventData)
        //{
        //    Debug.Log("드래그 중!");
        //    GameManager.Instance.dragAndDropController.ItemIconMoveToMouse();
        //}

        //public void OnEndDrag(PointerEventData eventData)
        //{
        //    Debug.Log("드래그 끝! 현재 아직 사용하지 않음");
        //    // 드래그 종료 시 해당 슬롯의 아이템을 드래그 앤 드롭 컨트롤러에 전달
        //    //ItemContainer inventory = GameManager.Instance.inventoryContainer;
        //    //GameManager.Instance.dragAndDropController.OnClick(inventory.slots[myIndex]);
        //    //transform.parent.GetComponent<InventoryPanel>().Show();
        //}

        //public void OnDrop(PointerEventData eventData)
        //{
        //    Debug.Log("드랍");
        //    // 드래그 종료 시 해당 슬롯의 아이템을 드래그 앤 드롭 컨트롤러에 전달
        //    ItemContainer inventory = GameManager.Instance.inventoryContainer;
        //    GameManager.Instance.dragAndDropController.OnClick(inventory.slots[myIndex]);
        //    transform.parent.GetComponent<InventoryPanel>().Show();
        //}
        #endregion
    }
}
