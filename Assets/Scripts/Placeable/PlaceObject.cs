using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // 도구 행동: 설치 오브젝트를 배치하는 기능을 수행하는 ToolAction ScriptableObject
    [CreateAssetMenu(menuName = "Data/Tool Action/Place Object")]
    public class PlaceObject : ToolAction
    {
        // 타일맵에 이 도구 행동을 적용할 때 호출되는 메서드
        public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
        {
            // 지정된 위치에 아이템을 설치
            tileMapReadController.placeableObjectsManager.Place(item, gridPosition);

            // 설치 성공 시 true 반환
            return true;
        }

        public override void OnItemUsed(Item item, ItemContainer itemContainer)
        {
            itemContainer.Remove(item);
        }
    }
}
