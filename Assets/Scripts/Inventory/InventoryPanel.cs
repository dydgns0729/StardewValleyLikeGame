namespace MyStardewValleylikeGame
{
    // 인벤토리 UI 패널을 관리하는 클래스
    public class InventoryPanel : ItemPanel
    {
        public override void OnEnable()
        {
            base.OnEnable();
            // 인벤토리 데이터 변경 시 UI 갱신
            inventory.inventoryChanged += Show;
        }

        public override void OnClick(int id)
        {
            // 인벤토리 슬롯 클릭 시, 드래그 앤 드롭 컨트롤러의 OnClick 메서드 호출(아이템 이동)
            GameManager.Instance.dragAndDropController.OnClick(inventory.slots[id]);
            // 인벤토리 UI 갱신
            Show();
        }

        public override void OnDragStart(int id)
        {
            GameManager.Instance.dragAndDropController.OnDragStart(inventory.slots[id]);
            Show();
        }

        public override void OnDragEnd(int id)
        {
            GameManager.Instance.dragAndDropController.DropInInventoryUI(inventory.slots[id]);
            Show();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            // 인벤토리 데이터 변경 시 UI 갱신
            inventory.inventoryChanged -= Show;
        }
    }
}
