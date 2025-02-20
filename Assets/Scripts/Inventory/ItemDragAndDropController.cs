using UnityEngine;
using UnityEngine.EventSystems;
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

            if (itemIcon.activeInHierarchy)
            {
                iconTransform.position = Input.mousePosition;

                //UI창을 벗어나있는 상태로 클릭하면 아이템을 들고있는 아이템 슬롯을 비우고 아이템을 카메라 위치값에 드롭(Spawn)시킨다.
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    //ScreenToWorldPoint함수를 이용하여 마우스의 위치값을 3D 월드의 위치값으로 변환
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //z값을 0으로 설정한다
                    worldPosition.z = 0f;
                    //아이템을 드롭시킨다.
                    ItemSpawnManager.Instance.SpawnItem(worldPosition, itemSlot.item, itemSlot.count);
                    //아이템 슬롯을 비운다.
                    itemSlot.Clear();
                    //아이콘을 비활성화한다.
                    UpdateIcon();
                }
            }
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