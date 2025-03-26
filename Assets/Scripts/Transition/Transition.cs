using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyStardewValleylikeGame
{
    public enum TransitionType
    {
        // 순간이동
        Warp,
        // 씬 전환
        Scene,
    }

    public class Transition : MonoBehaviour
    {
        #region Variables
        // 트랜지션 타입
        [SerializeField] TransitionType transitionType;
        // 전환할 씬 이름
        [SerializeField] string sceneNameToTransition;
        // 테스트용 위치 벡터값 (목표 위치로의 전환에 사용)
        [SerializeField] Vector3 targetPosition;


        // 목표 위치를 담을 Transform 변수 (목표 위치로의 전환에 사용)
        Transform destination;
        #endregion
        private void Start()
        {
            // 초기화 시, destination을 해당 오브젝트의 두 번째 자식(인덱스 1)으로 설정
            // transform.GetChild(1)로 자식 객체를 가져와 destination에 할당
            destination = transform.GetChild(1);
        }

        // 외부에서 호출하여 특정 Transform을 목표 위치(destination)로 이동시키는 메서드
        public void InitiateTransition(Transform toTransition)
        {
            //Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();


            switch (transitionType)
            {
                case TransitionType.Warp:
                    //currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toTransition, destination.position - toTransition.position);
                    // toTransition의 위치를 destination의 위치로 설정
                    toTransition.position = destination.position;
                    break;
                case TransitionType.Scene:
                    //currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toTransition, destination.position - toTransition.position);
                    // 씬 전환
                    GameSceneManager.Instance.InitSwitchScene(sceneNameToTransition, targetPosition);
                    break;
            }
        }
    }
}