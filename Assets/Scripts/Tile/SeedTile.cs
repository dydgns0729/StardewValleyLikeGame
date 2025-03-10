using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyStardewValleylikeGame
{
    [CreateAssetMenu(menuName = "Data/Tool Action/Seed Tile")]
    public class SeedTile : ToolAction
    {
        public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
        {
            // 해당 위치에 밭이 갈려있는지 확인
            if (!tileMapReadController.cropsManager.Check(gridPosition)) return false;

            // 해당 위치에 씨앗을 심는다.
            tileMapReadController.cropsManager.Seed(gridPosition);

            return true;
        }
    }
}