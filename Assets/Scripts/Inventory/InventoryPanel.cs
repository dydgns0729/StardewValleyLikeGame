namespace MyStardewValleylikeGame
{
    // 인벤토리 UI 패널을 관리하는 클래스
    public class InventoryPanel : ItemPanel
    {
        public override void OnClick(int id)
        {
            // 인벤토리 슬롯 클릭 시, 드래그 앤 드롭 컨트롤러의 OnClick 메서드 호출(아이템 이동)
            GameManager.Instance.dragAndDropController.OnClick(inventory.slots[id]);
            // 인벤토리 UI 갱신
            Show();
        }
    }
}
