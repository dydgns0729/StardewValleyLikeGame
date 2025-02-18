using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        // 싱글톤 인스턴스
        public static GameManager Instance { get; private set; }

        // Player 오브젝트를 public으로 선언하여 인스펙터에서 직접 할당 가능
        public GameObject player;
        public ItemContainer inventoryContainer;  // 인벤토리 데이터 (ScriptableObject)
        public ItemDragAndDropController dragAndDropController;  // 드래그 앤 드롭 컨트롤러
        #endregion

        private void Awake()
        {
            // 싱글톤 인스턴스 초기화
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // 씬 전환 시에도 GameManager 유지
            }
            else
            {
                Destroy(gameObject); // 이미 인스턴스가 존재하면 새로운 객체를 삭제
            }
        }
    }
}