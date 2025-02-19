using UnityEngine;
using UnityEngine.UI;

namespace MyStardewValleylikeGame
{
    public class ItemDragAndDropController : MonoBehaviour
    {
        [SerializeField] ItemSlot itemSlot;
        [SerializeField] GameObject itemIcon;
        RectTransform iconTransform;
        Image itemIconImage;

        private void Start()
        {
            itemSlot = new ItemSlot();
            iconTransform = itemIcon.GetComponent<RectTransform>();
            itemIconImage = itemIcon.GetComponent<Image>();
        }

        private void Update()
        {
            iconTransform.position = Input.mousePosition;
        }

        public void OnClick(ItemSlot itemSlot)
        {
            if (this.itemSlot.item == null)
            {
                ////마우스 클릭시 마우스 인벤토리 슬롯이 비어있으면 인벤토리 슬롯안에 내용을 마우스 인벤토리 슬롯으로 복사해준다.
                this.itemSlot.Copy(itemSlot);
                itemSlot.Clear();
            }
            else
            {
                //마우스 클릭시 마우스 인벤토리 슬롯이 비어있지않으면 마우스 안에있는 내용과 인벤토리 슬롯안에 내용을 바꿔준다.
                Item item = itemSlot.item;
                int count = itemSlot.count;

                //클릭한 아이템 슬롯에 마우스 아이템 슬롯의 내용을 복사
                itemSlot.Copy(this.itemSlot);
                //마우스 아이템 슬롯에 클릭한 아이템 슬롯의 내용을 복사
                this.itemSlot.Set(item, count);
            }

            UpdateIcon();
        }

        private void UpdateIcon()
        {
            if (this.itemSlot.item == null)
            {
                itemIconImage.sprite = null;
                itemIcon.SetActive(false);
            }
            else
            {
                itemIconImage.sprite = this.itemSlot.item.icon;
                itemIcon.SetActive(true);
            }
        }
    }
}