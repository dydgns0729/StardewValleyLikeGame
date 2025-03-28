using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    [CreateAssetMenu(menuName = "Data/Tool Action/Harvest")]
    // 'OnTilePickUpAction' 클래스는 도구 사용 시 타일에서 수확을 처리하는 액션을 정의합니다.
    // 이 클래스는 ToolAction을 상속받으며, OnApplyToTileMap 메서드를 오버라이드하여
    // 특정 타일을 수확할 때 실행될 동작을 정의합니다.
    public class OnTilePickUpAction : ToolAction
    {
        public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
        {
            // 타일의 위치(gridPosition)를 기준으로, cropsManager에서 PickUp 메서드를 호출하여 해당 타일에서 작물을 수확합니다.
            tileMapReadController.cropsManager.PickUp(gridPosition);
            // 타일의 위치(gridPosition)를 기준으로, placeableObjectsManager에서 PickUp 메서드를 호출하여 해당 타일에서 오브젝트를 회수합니다.
            tileMapReadController.placeableObjectsManager.PickUp(gridPosition);


            return true;
        }
    }
}