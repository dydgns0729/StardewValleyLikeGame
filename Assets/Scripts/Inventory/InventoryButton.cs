using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyStardewValleylikeGame
{
    // 인벤토리 슬롯(버튼)을 관리하는 클래스
    public class InventoryButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        Image icon;  // 아이템 아이콘 이미지

        [SerializeField]
        TextMeshProUGUI text;  // 아이템 개수를 표시하는 텍스트

        int myIndex;  // 해당 버튼의 인벤토리 슬롯 인덱스

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
            ItemContainer inventory = GameManager.Instance.inventoryContainer;
            GameManager.Instance.dragAndDropController.OnClick(inventory.slots[myIndex]);
        }
    }
}
