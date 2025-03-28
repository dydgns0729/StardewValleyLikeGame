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
            // 해당 위치에 이미 오브젝트가 있는지 확인
            if (tileMapReadController.placeableObjectsManager.Check(gridPosition)) return false;
            // 지정된 위치에 아이템을 설치
            tileMapReadController.placeableObjectsManager.Place(item, gridPosition);

            // 설치 성공 시 true 반환
            return true;
        }

        // 아이템 사용 시 호출되는 메서드
        public override void OnItemUsed(Item item, ItemContainer itemContainer)
        {
            // 아이템 사용 시 아이템 컨테이너에서 아이템 제거
            itemContainer.Remove(item);
        }
    }
}
