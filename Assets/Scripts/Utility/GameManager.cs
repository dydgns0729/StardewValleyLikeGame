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
        public PlaceableObjectsReferenceManager placeableObjects;   // 배치 가능한 오브젝트들을 관리하는 매니저
        #endregion

        #region 250324 스크립터블 오브젝트가 자동으로 저장되지 않는 문제 해결을 위한 코드 추가
        private void OnApplicationQuit()
        {
            // 게임 종료 시 인벤토리 데이터 저장
            inventoryContainer.SaveInventory();
        }
        #endregion
    }
}