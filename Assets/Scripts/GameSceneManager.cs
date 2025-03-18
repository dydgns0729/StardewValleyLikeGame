using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyStardewValleylikeGame
{
    public class GameSceneManager : PersistentSingleton<GameSceneManager>
    {
        #region Variables
        // 현재 활성화된 씬의 이름을 저장하는 변수
        string currentScene;

        float timer = 5f;
        #endregion

        // 게임 시작 시 현재 씬의 이름을 저장
        void Start()
        {
            // 현재 활성화된 씬의 이름을 가져와 currentScene 변수에 할당
            currentScene = SceneManager.GetActiveScene().name;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 5f;
                Debug.Log("Current Scene: " + SceneManager.GetActiveScene().name);
            }
        }

        // 씬 전환을 위한 메서드
        // 새로운 씬을 로드하고, 현재 씬은 비동기적으로 언로드
        public void SwitchScene(string to, Vector3 targetPosition)
        {
            // 새로운 씬을 추가적으로 로드 (Additive)
            SceneManager.LoadScene(to, LoadSceneMode.Additive);
            // 현재 씬을 비동기적으로 언로드
            SceneManager.UnloadSceneAsync(currentScene);
            // currentScene을 새로 로드된 씬으로 업데이트
            currentScene = to;

            GameManager.Instance.player.transform.position = targetPosition;
        }
    }
}