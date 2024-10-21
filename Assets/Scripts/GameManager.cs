using UnityEngine;

/// <summary>
/// Singleton<T> Ŭ������ ���׸� Ÿ�� T�� ���� �̱��� �ν��Ͻ��� �����ϴ� �⺻ Ŭ�����Դϴ�.
/// T�� MonoBehaviour�� ��ӹ޴� Ÿ���̾�� �մϴ�.
/// </summary>
/// <typeparam name="T">MonoBehaviour�� ��ӹ޴� ���׸� Ÿ��</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // �̱��� �ν��Ͻ��� ������ ����
    private static T _instance;

    // ������ ����ȭ�� ���� ��� ��ü
    private static readonly object _lock = new object();

    // �̱��� �ν��Ͻ��� �����ϴ��� Ȯ���ϴ� �÷���
    private static bool _applicationIsQuitting = false;

    /// <summary>
    /// �̱��� �ν��Ͻ��� �����ϴ� ������Ƽ
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

            lock (_lock) // ������ �������� �����ϱ� ���� lock ���
            {
                if (_instance == null)
                {
                    // ���� �ν��Ͻ��� ã�ƺ�
                    _instance = FindObjectOfType<T>();

                    // �ν��Ͻ��� �������� ������ ���� ����
                    if (_instance == null)
                    {
                        // �� ���� ������Ʈ�� ����� �� �ȿ� �̱��� �ν��Ͻ��� �߰�
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        // �̱��� ��ü�� �� ��ȯ �� �ı����� �ʵ��� ����
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
    /// ���ø����̼��� ����� �� ȣ��Ǵ� �޼���
    /// </summary>
    protected virtual void OnDestroy()
    {
        _applicationIsQuitting = true;
    }
}


public class GameManager : Singleton<GameManager>
{
    public int score;

    // GameManager���� ��� �߰� ����
    public void AddScore(int value)
    {
        score += value;
    }
}