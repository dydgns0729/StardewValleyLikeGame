using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // 설치 가능한 오브젝트 정보를 담는 직렬화 가능한 클래스
    [Serializable]
    public class PlaceableObject
    {
        #region Variables
        public Item placedItem;             // 설치된 아이템 정보
        public Transform targetObject;      // 실제 씬에 배치된 오브젝트의 Transform
        public Vector3Int positionOnGrid;   // 그리드 상의 위치
        #endregion

        // 생성자: 설치 아이템, 오브젝트, 위치를 초기화
        public PlaceableObject(Item item, Vector3Int pos)
        {
            placedItem = item;
            positionOnGrid = pos;
        }
    }

    // 설치 가능한 오브젝트들을 저장하는 ScriptableObject 컨테이너
    [CreateAssetMenu(menuName = "Data/Placeable Objects Container")]
    public class PlaceableObjectsContainer : ScriptableObject
    {
        #region Variables
        public List<PlaceableObject> placeableObjects; // 설치 가능한 오브젝트 리스트
        #endregion

        // 지정된 그리드 위치에 해당하는 PlaceableObject를 반환하는 메서드
        internal PlaceableObject Get(Vector3Int position)
        {
            // 리스트에서 주어진 위치에 해당하는 PlaceableObject를 찾음
            return placeableObjects.Find(x => x.positionOnGrid == position);
        }

        // 주어진 PlaceableObject를 리스트에서 제거하는 메서드
        internal void Remove(PlaceableObject placedObject)
        {
            // 리스트에서 해당 오브젝트를 제거
            placeableObjects.Remove(placedObject);
        }
    }
}
