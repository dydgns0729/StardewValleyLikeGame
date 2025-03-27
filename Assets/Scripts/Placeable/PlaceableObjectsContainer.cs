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
        public PlaceableObject(Item item, Transform target, Vector3Int pos)
        {
            placedItem = item;
            targetObject = target;
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
    }
}
