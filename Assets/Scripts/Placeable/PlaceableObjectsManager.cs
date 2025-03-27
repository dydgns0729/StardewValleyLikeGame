using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyStardewValleylikeGame
{
    // 설치 가능한 오브젝트들을 관리하는 매니저 클래스
    public class PlaceableObjectsManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] PlaceableObjectsContainer placeableObjects; // 설치된 오브젝트 정보를 저장하는 컨테이너
        [SerializeField] Tilemap targetTilemap; // 오브젝트를 배치할 대상 타일맵
        #endregion

        private void Start()
        {
            // 게임 시작 시 GameManager에서 이 매니저에 접근할 수 있도록 참조 설정
            GameManager.Instance.GetComponent<PlaceableObjectsReferenceManager>().placeableObjectsManager = this;
        }

        // 아이템을 그리드 위치에 설치하는 메서드
        public void Place(Item item, Vector3Int positionOnGrid)
        {
            // 아이템 프리팹을 생성하고 이 매니저의 자식으로 설정
            GameObject go = Instantiate(item.itemPrefab, transform);

            // 셀 위치를 월드 위치로 변환하고 셀의 중앙에 위치하도록 보정
            Vector3 position = targetTilemap.CellToWorld(positionOnGrid) + targetTilemap.cellSize / 2;
            go.transform.position = position;

            // 설치된 오브젝트 정보를 리스트에 추가
            placeableObjects.placeableObjects.Add(new PlaceableObject(item, go.transform, positionOnGrid));
        }
    }
}
