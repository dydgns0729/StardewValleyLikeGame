using UnityEngine;

namespace MyStardewValleylikeGame
{
    // 씬 전환 시에도 삭제되지 않는 싱글톤 클래스
    public class PersistentSingleton<T> : Singleton<T> where T : Singleton<T>
    {
        // Awake(): 오브젝트가 생성될 때 호출됨
        protected override void Awake()
        {
            base.Awake(); // 기본 Singleton<T>의 Awake() 실행

            // 씬이 변경되더라도 삭제되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
    }
}
