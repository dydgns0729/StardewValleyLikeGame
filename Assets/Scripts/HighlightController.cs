using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class HighlightController : MonoBehaviour
    {
        #region Variables
        [SerializeField] GameObject highlighter;  // 하이라이트 효과를 담당하는 게임 오브젝트. Unity 에디터에서 할당.
        GameObject currentTarget;                 // 현재 하이라이트 중인 타겟 게임 오브젝트.
        #endregion

        /// <summary>
        /// 지정된 타겟을 하이라이트합니다.
        /// 만약 현재 타겟과 같다면 중복 처리를 피하기 위해 아무 작업도 하지 않습니다.
        /// </summary>
        public void Highlight(GameObject target)
        {
            if (currentTarget == target) return;  // 이미 하이라이트된 타겟이면 종료.

            currentTarget = target;               // 새로운 타겟으로 설정.
            Vector3 position = target.transform.position + (Vector3.up * 0.5f);  // 타겟의 위치를 가져옴.
            Highlight(position);                  // 해당 위치에 하이라이트 표시.
        }

        /// <summary>
        /// 주어진 위치로 하이라이트 오브젝트를 이동시키고 활성화합니다.
        /// </summary>
        public void Highlight(Vector3 position)
        {
            highlighter.transform.position = position;  // 하이라이트 오브젝트를 지정된 위치로 이동.
            highlighter.SetActive(true);                // 하이라이트 오브젝트를 활성화.
        }

        /// <summary>
        /// 현재 하이라이트를 숨깁니다.
        /// </summary>
        public void Hide()
        {
            currentTarget = null;             // 현재 타겟을 초기화.
            highlighter.SetActive(false);     // 하이라이트 오브젝트를 비활성화.
        }
    }
}










//using UnityEngine;



//namespace MyStardewValleylikeGame
//{
//    public class HighlightController : MonoBehaviour
//    {
//        #region Variables
//        [SerializeField] GameObject highlighter;  // 하이라이트 효과를 담당하는 게임 오브젝트. Unity 에디터에서 할당.
//        GameObject currentTarget;                 // 현재 하이라이트 중인 타겟 게임 오브젝트.
//        #endregion

//        /// <summary>
//        /// 지정된 타겟을 하이라이트합니다.
//        /// 만약 현재 타겟과 같고 위치 변화가 없다면 중복 처리를 피하기 위해 아무 작업도 하지 않습니다.
//        /// </summary>
//        public void Highlight(GameObject target)
//        {
//            if (currentTarget == target)
//            {
//                // 타겟 위치가 변하지 않았다면 중복 처리 방지.
//                if (highlighter.transform.position == target.transform.position) return;
//            }

//            // 새로운 타겟 또는 위치 변경 시 타겟 및 위치 갱신
//            currentTarget = target;
//            Vector3 position = target.transform.position;
//            Highlight(position);                  // 해당 위치에 하이라이트 표시.
//        }

//        /// <summary>
//        /// 주어진 위치로 하이라이트 오브젝트를 이동시키고 활성화합니다.
//        /// </summary>
//        public void Highlight(Vector3 position)
//        {
//            // 위치가 다를 때만 이동
//            if (highlighter.transform.position != position)
//            {
//                highlighter.transform.position = position;
//            }

//            // 비활성화된 경우에만 활성화
//            if (!highlighter.activeSelf)
//            {
//                highlighter.SetActive(true);
//            }
//        }

//        /// <summary>
//        /// 현재 하이라이트를 숨깁니다.
//        /// </summary>
//        public void Hide()
//        {
//            currentTarget = null;                 // 현재 타겟을 초기화.
//            if (highlighter.activeSelf)
//            {
//                highlighter.SetActive(false);     // 하이라이트 오브젝트를 비활성화.
//            }
//        }
//    }
//}
