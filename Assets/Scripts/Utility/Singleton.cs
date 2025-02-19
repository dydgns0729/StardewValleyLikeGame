using UnityEngine;

namespace MyStardewValleylikeGame
{
    // 제네릭 싱글톤 클래스: MonoBehaviour를 상속받아 특정 타입의 싱글톤을 생성
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance; // 싱글톤 인스턴스를 저장하는 정적 변수

        // 싱글톤 인스턴스를 외부에서 접근할 수 있도록 하는 프로퍼티
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new System.NullReferenceException($"Singleton instance of {typeof(T)} is not initialized.");
                }
                return instance;
            }
        }

        // 싱글톤 인스턴스가 존재하는지 여부를 반환하는 프로퍼티
        public static bool InstancExist
        {
            get { return instance != null; }
        }

        // Awake(): 오브젝트가 생성될 때 호출됨
        protected virtual void Awake()
        {
            // 이미 인스턴스가 존재하면 중복 생성을 방지하기 위해 현재 오브젝트를 제거
            if (InstancExist)
            {
                Destroy(this.gameObject);
                return;
            }
            // 현재 오브젝트를 싱글톤 인스턴스로 설정
            instance = (T)this;
        }

        // OnDestroy(): 오브젝트가 파괴될 때 호출됨
        protected virtual void OnDestroy()
        {
            // 현재 인스턴스가 자신이라면 정적 변수 instance를 null로 설정
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}
