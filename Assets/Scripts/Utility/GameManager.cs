using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        #region Variables
        public GameObject player;                                   // Player 
        public ItemContainer inventoryContainer;                    // 인벤토리 데이터 (ScriptableObject)
        public ItemDragAndDropController dragAndDropController;     // 드래그 앤 드롭 컨트롤러
        public DayTimeController dayTimeController;                 // 시간 컨트롤러
        public DialogueSystem dialogueSystem;                       // 다이얼로그 시스템
        #endregion
    }
}