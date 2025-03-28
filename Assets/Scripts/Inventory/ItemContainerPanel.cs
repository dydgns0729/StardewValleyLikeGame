using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // ItemContainerPanel 클래스는 ItemPanel을 상속하여 인벤토리 UI에서 아이템 클릭, 드래그, 드롭 기능을 처리한다.
    public class ItemContainerPanel : ItemPanel
    {
        // 인벤토리 슬롯 클릭 시 호출되는 메서드
        public override void OnClick(int id)
        {
            // 인벤토리 슬롯 클릭 시, 드래그 앤 드롭 컨트롤러의 OnClick 메서드를 호출하여 아이템을 이동
            GameManager.Instance.dragAndDropController.OnClick(inventory.slots[id]);

            // UI를 갱신하여 변경된 내용을 반영
            Show();
        }

        // 드래그 시작 시 호출되는 메서드
        public override void OnDragStart(int id)
        {
            // 드래그 앤 드롭 컨트롤러의 OnDragStart 메서드를 호출하여 드래그가 시작됨
            GameManager.Instance.dragAndDropController.OnDragStart(inventory.slots[id]);

            // UI를 갱신하여 드래그 상태를 반영
            Show();
        }

        // 드래그 종료 시 호출되는 메서드
        public override void OnDragEnd(int id)
        {
            // 드래그 앤 드롭 컨트롤러의 DropInInventoryUI 메서드를 호출하여 드래그된 아이템을 인벤토리 UI에 드롭
            GameManager.Instance.dragAndDropController.DropInInventoryUI(inventory.slots[id]);

            // UI를 갱신하여 드래그 후 변경된 내용을 반영
            Show();
        }
    }
}
