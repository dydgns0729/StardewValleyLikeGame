using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyStardewValleylikeGame
{
    // 밭을 가는 클래스
    [CreateAssetMenu(menuName = "Data/Tool Action/Plow")]
    public class PlowTile : ToolAction
    {
        // 밭을 갈 수 있는 타일 리스트
        [SerializeField] List<TileBase> canPlow;

        // 밭을 갈 수 있는지 확인하고, 가능하다면 밭을 간다.
        public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
        {
            // 해당 위치의 타일을 가져옴
            TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);
            // 해당 타일이 밭을 갈 수 있는지 확인
            if (!canPlow.Contains(tileToPlow))
            {
                return false;
            }
            Debug.Log("PlowTile.OnApplyToTileMap");
            // 해당 위치에 밭을 간다.
            tileMapReadController.cropsManager.Plow(gridPosition);

            return true;
        }
    }
}