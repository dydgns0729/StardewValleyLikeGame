using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // 설치 오브젝트 관련 기능을 다른 클래스들이 쉽게 접근할 수 있도록 중계 역할을 하는 매니저
    public class PlaceableObjectsReferenceManager : MonoBehaviour
    {
        #region Variables
        public PlaceableObjectsManager placeableObjectsManager; // 설치 기능을 수행하는 실제 매니저에 대한 참조
        #endregion

        // 설치 요청을 전달하는 메서드
        public void Place(Item item, Vector3Int pos)
        {
            // placeableObjectsManager가 연결되어 있지 않으면 에러 로그 출력
            if (placeableObjectsManager == null)
            {
                Debug.LogError("PlaceableObjectsManager is not assigned to PlaceableObjectsReferenceManager");
                return;
            }

            // 연결된 매니저를 통해 실제 설치 수행
            placeableObjectsManager.Place(item, pos);
        }

        public bool Check(Vector3Int pos)
        {
            // placeableObjectsManager가 연결되어 있지 않으면 에러 로그 출력
            if (placeableObjectsManager == null)
            {
                Debug.LogError("PlaceableObjectsManager is not assigned to PlaceableObjectsReferenceManager");
                return false;
            }

            // 연결된 매니저를 통해 해당 위치에 오브젝트가 있는지 확인
            return placeableObjectsManager.Check(pos);
        }

        internal void PickUp(Vector3Int gridPosition)
        {
            // placeableObjectsManager가 연결되어 있지 않으면 에러 로그 출력
            if (placeableObjectsManager == null)
            {
                Debug.LogError("PlaceableObjectsManager is not assigned to PlaceableObjectsReferenceManager");
                return;
            }
            // 연결된 매니저를 통해 해당 위치의 오브젝트 회수
            placeableObjectsManager.PickUp(gridPosition);
        }
    }
}
