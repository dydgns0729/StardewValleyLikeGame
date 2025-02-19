using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        #region Variables
        // Player 오브젝트를 public으로 선언하여 인스펙터에서 직접 할당 가능
        public GameObject player;
        public ItemContainer inventoryContainer;  // 인벤토리 데이터 (ScriptableObject)
        public ItemDragAndDropController dragAndDropController;  // 드래그 앤 드롭 컨트롤러
        #endregion
    }
}