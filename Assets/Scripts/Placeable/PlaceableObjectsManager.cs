using System;
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
        // 설치된 오브젝트 정보를 저장하는 컨테이너
        [SerializeField] PlaceableObjectsContainer placeableObjects;

        // 오브젝트를 배치할 대상 타일맵
        [SerializeField] Tilemap targetTilemap;
        #endregion

        // 게임 시작 시 GameManager에서 이 매니저에 접근할 수 있도록 참조를 설정
        private void Start()
        {
            // GameManager에서 PlaceableObjectsReferenceManager를 찾아서 placeableObjectsManager에 이 객체를 할당
            GameManager.Instance.GetComponent<PlaceableObjectsReferenceManager>().placeableObjectsManager = this;
            // 설치 가능한 오브젝트들을 타일맵에 시각화
            VisualizeMap();
        }

        // 오브젝트 매니저가 파괴될 때 설치된 오브젝트들의 targetObject를 null로 설정
        private void OnDestroy()
        {
            // 설치된 모든 오브젝트에 대해 targetObject 참조를 해제
            for (int i = 0; i < placeableObjects.placeableObjects.Count; i++)
            {
                // targetObject를 null로 설정하여 참조 해제
                placeableObjects.placeableObjects[i].targetObject = null;
            }
        }

        // 설치 가능한 오브젝트들을 타일맵에 시각화
        private void VisualizeMap()
        {
            // 각 설치 가능한 오브젝트들을 시각화
            for (int i = 0; i < placeableObjects.placeableObjects.Count; i++)
            {
                // 각 오브젝트를 타일맵에 배치
                VisualizeItem(placeableObjects.placeableObjects[i]);
            }
        }

        // 단일 오브젝트를 타일맵에 시각화하는 메서드
        private void VisualizeItem(PlaceableObject placeableObject)
        {
            // 아이템 프리팹을 인스턴스화하고 이 매니저의 자식으로 설정
            GameObject go = Instantiate(placeableObject.placedItem.itemPrefab, transform);

            // 타일맵의 셀 위치를 월드 좌표로 변환하고, 셀의 중앙에 오브젝트를 위치시키도록 보정
            Vector3 position = targetTilemap.CellToWorld(placeableObject.positionOnGrid) + targetTilemap.cellSize / 2;
            go.transform.position = position;

            // 생성된 오브젝트의 targetObject를 설정하여 추후 위치나 상태를 변경할 수 있도록 설정
            placeableObject.targetObject = go.transform;
        }

        // 해당 위치에 오브젝트가 있는지 확인하는 메서드
        public bool Check(Vector3Int position)
        {
            // 해당 위치에 이미 설치된 오브젝트가 있는지 확인
            return placeableObjects.Get(position) != null;
        }

        // 아이템을 그리드 위치에 설치하는 메서드
        public void Place(Item item, Vector3Int positionOnGrid)
        {
            // 새로 배치할 오브젝트를 PlaceableObject로 생성
            PlaceableObject placeableObject = new PlaceableObject(item, positionOnGrid);

            // 생성된 오브젝트를 타일맵에 시각화
            VisualizeItem(placeableObject);

            // 설치된 오브젝트 정보를 리스트에 추가
            placeableObjects.placeableObjects.Add(placeableObject);
        }

        // 그리드 위치에서 아이템을 회수하는 메서드
        internal void PickUp(Vector3Int gridPosition)
        {
            // 해당 위치에서 설치된 오브젝트를 가져옴
            PlaceableObject placedObject = placeableObjects.Get(gridPosition);

            // 해당 위치에 오브젝트가 없다면 종료
            if (placedObject == null)
            {
                Debug.Log("해당 위치 " + gridPosition + "에 회수할 오브젝트가 없습니다.");
                return;
            }

            // 아이템을 월드 좌표로 변환하여 아이템을 생성
            ItemSpawnManager.Instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), placedObject.placedItem, 1);

            // 오브젝트의 실제 게임 오브젝트를 파괴
            Destroy(placedObject.targetObject.gameObject);

            // 설치된 오브젝트 리스트에서 해당 오브젝트를 제거
            placeableObjects.Remove(placedObject);
        }
    }
}
