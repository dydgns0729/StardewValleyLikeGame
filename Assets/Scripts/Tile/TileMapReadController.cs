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

        // 여러 개의 TileData를 저장하는 리스트 (Unity 에디터에서 할당)
        [SerializeField] List<TileData> tileData;

        // 특정 TileBase에 매칭되는 TileData를 빠르게 조회하기 위한 딕셔너리
        Dictionary<TileBase, TileData> dataFromTiles;
        #endregion

        private void Start()
        {
            // 딕셔너리 초기화 (타일-데이터 매핑을 저장할 공간 생성)
            dataFromTiles = new Dictionary<TileBase, TileData>();

            // tileData 리스트를 순회하며 각 타일과 연결된 데이터를 딕셔너리에 저장
            foreach (TileData tiledata in tileData)
            {
                foreach (TileBase tileBase in tiledata.tiles) // 해당 TileData에 포함된 모든 타일을 가져옴
                {
                    dataFromTiles.Add(tileBase, tiledata); // 타일을 키로, TileData를 값으로 저장
                }
            }
        }

        private void Update()
        {
            // 마우스 왼쪽 버튼을 클릭하면 해당 위치의 타일 정보를 가져옴
            if (Input.GetMouseButtonDown(0))
            {
                GetTileData(GetTileBase(GetGridPosition(Input.mousePosition, true)));
            }
        }

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

        /// <summary>
        /// 주어진 타일에 해당하는 TileData를 반환
        /// </summary>
        /// <param name="tileBase">찾고 싶은 TileBase</param>
        /// <returns>TileBase에 해당하는 TileData</returns>
        public TileData GetTileData(TileBase tileBase)
        {
            // 딕셔너리에서 해당 타일의 데이터를 가져옴
            //Debug.Log("TileData is " + dataFromTiles[tileBase]); // 검색된 데이터 로그 출력
            return dataFromTiles[tileBase];
        }
    }
}
