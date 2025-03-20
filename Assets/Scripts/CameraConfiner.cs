using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class CameraConfiner : MonoBehaviour
    {
        #region Variables
        // CinemachineConfiner 컴포넌트를 에디터에서 연결할 수 있도록 직렬화된 필드
        [SerializeField] CinemachineConfiner confiner;
        #endregion

        private void Start()
        {
            // 카메라의 경계 범위를 업데이트합니다.
            UpdateBounds();
        }

        // 카메라의 경계 범위를 업데이트하는 메서드
        public void UpdateBounds()
        {
            // "CameraConfiner"라는 이름을 가진 GameObject를 찾습니다.
            GameObject go = GameObject.Find("CameraConfiner");
            // 해당 오브젝트가 없다면, CinemachineConfiner의 m_BoundingShape2D를 null로 설정하여 카메라가 제한되는 영역을 없앱니다.
            if (go == null)
            {
                confiner.m_BoundingShape2D = null;
                return;
            }
            // "CameraConfiner"라는 이름을 가진 GameObject를 찾고, 해당 오브젝트에 있는 Collider2D를 가져옵니다.
            Collider2D bounds = go.GetComponent<Collider2D>();

            // CinemachineConfiner의 m_BoundingShape2D를 찾은 Collider2D로 설정하여 카메라가 제한될 영역을 업데이트합니다.
            confiner.m_BoundingShape2D = bounds;
        }
    }
}