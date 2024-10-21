using UnityEngine;

/// <summary>
/// Singleton<T> 클래스는 제네릭 타입 T에 대해 싱글톤 인스턴스를 제공하는 기본 클래스입니다.
/// T는 MonoBehaviour를 상속받는 타입이어야 합니다.
/// </summary>
/// <typeparam name="T">MonoBehaviour를 상속받는 제네릭 타입</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // 싱글톤 인스턴스를 저장할 변수
    private static T _instance;

    // 스레드 동기화를 위한 잠금 객체
    private static readonly object _lock = new object();

    // 싱글톤 인스턴스가 존재하는지 확인하는 플래그
    private static bool _applicationIsQuitting = false;

    /// <summary>
    /// 싱글톤 인스턴스에 접근하는 프로퍼티
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Application is quitting. Returning null.");
                return null;
            }

            lock (_lock) // 스레드 안전성을 보장하기 위해 lock 사용
            {
                if (_instance == null)
                {
                    // 먼저 인스턴스를 찾아봄
                    _instance = FindObjectOfType<T>();

                    // 인스턴스가 존재하지 않으면 새로 생성
                    if (_instance == null)
                    {
                        // 새 게임 오브젝트를 만들고 그 안에 싱글톤 인스턴스를 추가
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        // 싱글톤 객체가 씬 전환 시 파괴되지 않도록 설정
                        DontDestroyOnLoad(singletonObject);

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so '" + singletonObject +
                            "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " + _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }
    /// <summary>
    /// 어플리케이션이 종료될 때 호출되는 메서드
    /// </summary>
    protected virtual void OnDestroy()
    {
        _applicationIsQuitting = true;
    }
}


public class GameManager : Singleton<GameManager>
{
    public int score;

    // GameManager만의 기능 추가 가능
    public void AddScore(int value)
    {
        score += value;
    }
}