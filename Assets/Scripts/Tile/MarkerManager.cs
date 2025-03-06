using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyStardewValleylikeGame
{
    public class MarkerManager : MonoBehaviour
    {
        #region Variables
        //마커를 표시할 타일맵
        [SerializeField] Tilemap targetTilemap;
        //마커로 사용할 타일
        [SerializeField] TileBase tileBase;

        //현재 마커가 위치할 타일맵 좌표(Vector3Int).
        public Vector3Int markedCellPosition;
        //이전 프레임에서 마커가 있던 위치를 저장하는 변수
        Vector3Int oldCellPosition;

        //마커를 표시할지 여부를 결정하는 변수
        bool show;
        #endregion

        private void Update()
        {
            if (!show) return;

            // 이전 위치와 현재 위치가 같으면 실행하지 않음
            if (oldCellPosition == markedCellPosition) return;

            //Debug.Log("마커 위치: " + markedCellPosition);
            //이전 위치(oldCellPosition)에 있던 마커를 제거 (null을 설정하면 타일이 삭제됨).
            targetTilemap.SetTile(oldCellPosition, null);

            //현재 위치(markedCellPosition)에 마커 타일(tileBase)을 배치.
            targetTilemap.SetTile(markedCellPosition, tileBase);

            //현재 위치를 oldCellPosition에 저장해서, 다음 프레임에서 이 위치의 타일을 지울 수 있도록 업데이트.
            oldCellPosition = markedCellPosition;
        }

        internal void Show(bool selectable)
        {
            show = selectable;
            targetTilemap.gameObject.SetActive(show);
        }
    }
}