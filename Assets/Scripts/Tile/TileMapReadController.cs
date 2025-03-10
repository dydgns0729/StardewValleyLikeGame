using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyStardewValleylikeGame
{
    public class TileMapReadController : MonoBehaviour
    {
        #region Variables
        // Tilemap 컴포넌트 참조 (Unity 에디터에서 할당)
        [SerializeField] Tilemap tilemap;

        // 작물 관리자를 참조하는 변수
        public CropsManager cropsManager;
        #endregion

        /// <summary>
        /// 주어진 화면 좌표 또는 월드 좌표를 그리드 좌표(Vector3Int)로 변환
        /// </summary>
        /// <param name="position">변환할 좌표 (화면 좌표 또는 월드 좌표)</param>
        /// <param name="mousePosition">true면 마우스 좌표를, false면 월드 좌표를 변환</param>
        /// <returns>그리드 셀 위치 (Vector3Int)</returns>
        public Vector3Int GetGridPosition(Vector2 position, bool mousePosition = false)
        {
            Vector3 worldPosition;

            if (mousePosition)
            {
                // 마우스 화면 좌표를 월드 좌표로 변환
                worldPosition = Camera.main.ScreenToWorldPoint(position);
            }
            else
            {
                // 이미 월드 좌표가 주어진 경우 그대로 사용
                worldPosition = position;
            }

            // 월드 좌표를 타일맵의 그리드 셀 좌표로 변환
            Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

            //Debug.Log("gridPosition = " + gridPosition); // 변환된 좌표를 로그로 출력

            return gridPosition;
        }

        /// <summary>
        /// 주어진 그리드 위치에 있는 타일을 반환
        /// </summary>
        /// <param name="gridPosition">그리드 셀 좌표 (Vector3Int)</param>
        /// <returns>해당 위치의 TileBase (없으면 null)</returns>
        public TileBase GetTileBase(Vector3Int gridPosition)
        {
            // 타일맵에서 주어진 위치의 타일을 가져옴
            TileBase tile = tilemap.GetTile(gridPosition);

            //Debug.Log("TileBase is " + tile); // 찾은 타일 정보를 로그로 출력

            return tile;
        }
    }
}
