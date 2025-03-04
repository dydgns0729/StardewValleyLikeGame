using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyStardewValleylikeGame
{
    public class InventoryButton : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        #region Variables
        [SerializeField] private Image icon;            // 아이템 아이콘을 나타내는 Image 컴포넌트
        [SerializeField] private TextMeshProUGUI text;  // 아이템 개수를 표시하는 TextMeshProUGUI 컴포넌트
        [SerializeField] private Image highlightImage;  // 선택된 아이템을 표시하는 하이라이트 이미지
        public int myIndex;                             // 현재 버튼의 인덱스

        private GameManager gameManager;                //게임매니저 참조변수
        #endregion

        //게임 매니저를 필드로 참조
        private void Start()
        {
            //게임매니저 할당
            gameManager = GameManager.Instance;
        }

        // 인덱스를 설정하는 메서드
        public void SetIndex(int index) => myIndex = index;

        // 슬롯의 아이템 정보를 버튼에 설정하는 메서드
        public void SetItem(ItemSlot slot)
        {
            icon.gameObject.SetActive(true);                                // 아이콘을 활성화
            icon.sprite = slot.item.icon;                                   // 슬롯의 아이템 아이콘 설정
            text.gameObject.SetActive(slot.item.stackable);                 // 아이템이 스택 가능한 경우 텍스트 활성화
            text.text = slot.item.stackable ? slot.count.ToString() : "";   // 스택 가능한 경우 개수를 표시, 그렇지 않으면 빈 문자열
        }

        // 버튼을 초기화하는 메서드
        public void Clean()
        {
            icon.sprite = null;                                             // 아이콘 스프라이트 초기화
            icon.gameObject.SetActive(false);                               // 아이콘 비활성화
            text.text = "";                                                 // 텍스트 초기화
            text.gameObject.SetActive(false);                               // 텍스트 비활성화
        }

        // 포인터 클릭 이벤트 처리 메서드
        public void OnPointerClick(PointerEventData eventData)
        {
            ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
            itemPanel.OnClick(myIndex);

            #region 툴바 추가 전
            // 드래그 앤 드롭 컨트롤러의 클릭 처리 메서드 호출
            //gameManager.dragAndDropController.OnClick(gameManager.inventoryContainer.slots[myIndex]);
            // 인벤토리 패널을 보여줌
            //transform.parent.GetComponent<InventoryPanel>().Show();
            #endregion 
        }

        //툴바에 현재 선택된 아이템을 표시하는 메서드 
        public void Highlight(bool isOn)
        {
            highlightImage.gameObject.SetActive(isOn);
        }

        #region 드래그 앤 드롭 기능 구현
        // 드래그 시작 이벤트 처리 메서드
        public void OnBeginDrag(PointerEventData eventData)
        {
            // 현재 슬롯이 비어있으면 리턴
            if (gameManager.inventoryContainer.slots[myIndex].item == null) return;
            // 드래그 앤 드롭 컨트롤러의 드래그 시작 메서드 호출
            gameManager.dragAndDropController.OnDragStart(gameManager.inventoryContainer.slots[myIndex]);
            // 인벤토리 패널을 최신화
            transform.parent.GetComponent<InventoryPanel>().Show();
        }

        // 드래그 중 이벤트 처리 메서드 (현재는 빈 메서드)
        public void OnDrag(PointerEventData eventData) { }

        // 드래그 종료 이벤트 처리 메서드
        public void OnEndDrag(PointerEventData eventData)
        {
            // 드래그 중이 아니면 리턴
            if (!gameManager.dragAndDropController.isDraging) return;
            // 드래그 앤 드롭 컨트롤러의 드래그 종료 메서드 호출
            gameManager.dragAndDropController.OnEndDrag();
        }

        // 드롭 이벤트 처리 메서드
        public void OnDrop(PointerEventData eventData)
        {
            // 드래그 중이 아니면 리턴
            if (!gameManager.dragAndDropController.isDraging) return;
            // 드래그 앤 드롭 컨트롤러의 인벤토리에 드롭 메서드 호출
            gameManager.dragAndDropController.DropInInventoryUI(gameManager.inventoryContainer.slots[myIndex]);
            // 인벤토리 패널을 최신화
            transform.parent.GetComponent<InventoryPanel>().Show();
        }
        #endregion
    }
}
