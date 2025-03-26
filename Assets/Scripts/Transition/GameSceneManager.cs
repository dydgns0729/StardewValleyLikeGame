using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyStardewValleylikeGame
{
    public class GameSceneManager : PersistentSingleton<GameSceneManager>
    {
        #region Variables
        // 현재 활성화된 씬의 이름을 저장하는 변수
        string currentScene;

        // 화면에 틴트 효과를 적용할 ScreenTint 객체
        [SerializeField] ScreenTint screenTint;

        // 씬 전환 시 CameraConfiner 컴포넌트를 사용하여 카메라의 경계를 설정
        [SerializeField] CameraConfiner cameraConfiner;

        // 비동기적으로 씬을 로드하고 언로드하기 위한 AsyncOperation 객체
        AsyncOperation unload;
        AsyncOperation load;

        #endregion

        // 게임 시작 시 현재 씬의 이름을 저장
        void Start()
        {
            // 현재 활성화된 씬의 이름을 가져와 currentScene 변수에 할당
            currentScene = SceneManager.GetActiveScene().name;
        }

        // 씬 전환 초기화 함수
        // 씬 전환을 시작하고, 타겟 위치로 플레이어를 이동시킴
        public void InitSwitchScene(string to, Vector3 targetPosition)
        {
            // 씬 전환을 처리하는 코루틴 시작
            StartCoroutine(Transition(to, targetPosition));
        }

        // 씬 전환을 처리하는 코루틴
        // 화면 틴트를 적용하고, 씬 전환 후 화면 틴트를 해제
        IEnumerator Transition(string to, Vector3 targetPosition)
        {
            // 화면 틴트를 적용하여 화면을 덮음
            screenTint.Tint();

            // 틴트 진행 시간에 맞춰 대기 (틴트가 거의 끝날 때까지 기다림)
            yield return new WaitForSeconds((1f - screenTint.tintedTime) / screenTint.tintedSpeed);

            // 씬 전환
            SwitchScene(to, targetPosition);

            // 씬 로드와 언로드 작업이 완료될 때까지 대기
            while (load != null & unload != null)
            {
                // 로딩이 완료되면 load를 null로 설정
                if (load.isDone) load = null;
                // 언로드가 완료되면 unload를 null로 설정
                if (unload.isDone) unload = null;
                // 0.1초마다 상태를 체크
                yield return new WaitForSeconds(0.1f);
            }

            // 한 프레임을 기다림 (씬이 로드되기 전까지 대기)
            yield return new WaitForEndOfFrame();
            //새로 로드된 씬을 활성화
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
            // 화면 틴트 해제
            screenTint.UnTint();

            // 카메라 경계 설정 업데이트
            cameraConfiner.UpdateBounds();
        }

        // 씬 전환을 위한 메서드
        // 새로운 씬을 로드하고, 현재 씬은 비동기적으로 언로드
        public void SwitchScene(string to, Vector3 targetPosition)
        {
            // 새로운 씬을 추가적으로, 비동기적으로 로드 (Additive)
            load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

            // 현재 씬을 비동기적으로 언로드 (메모리에서 제거)
            unload = SceneManager.UnloadSceneAsync(currentScene);

            // currentScene을 새로 로드된 씬으로 업데이트
            currentScene = to;

            // 플레이어의 Transform을 가져옵니다
            // GameManager.Instance.player는 게임 내에서 플레이어를 나타내는 객체이며,
            // 그 객체의 Transform을 가져와 playerTransform에 할당합니다
            Transform playerTransform = GameManager.Instance.player.transform;

            // 현재 활성화된 카메라의 CinemachineBrain을 가져옵니다
            // Camera.main은 메인 카메라를 가르키며, 그 카메라에서 CinemachineBrain 컴포넌트를 가져옵니다
            Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();

            // 현재 카메라의 활성 가상 카메라(Virtual Camera)가 목표로 하는 객체(플레이어)의 위치가 변경되었을 때,
            // OnTargetObjectWarped 메서드를 호출하여 가상 카메라의 위치를 업데이트합니다
            // targetPosition은 플레이어가 이동할 목표 위치이며, playerTransform.position은 플레이어의 현재 위치입니다
            // 두 위치 간의 차이를 계산하여 카메라가 그 차이에 맞춰 움직이도록 합니다
            currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(playerTransform, targetPosition - playerTransform.position);

            // 플레이어를 새로운 타겟 위치로 이동
            GameManager.Instance.player.transform.position = targetPosition;
        }
    }
}
