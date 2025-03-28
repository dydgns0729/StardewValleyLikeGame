using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] GameObject inventoryPanel;          //인벤토리 UI
        [SerializeField] GameObject statusPanel;             //상태 UI
        [SerializeField] GameObject toolbarPanel;            //툴바 UI

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            // I 키를 누르면 인벤토리,TopPanel UI를 활성화/비활성화
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
            statusPanel.SetActive(!statusPanel.activeInHierarchy);

            //인벤토리 UI가 활성화되면 툴바 UI를 비활성화
            #region 추후 필요시 활성화
            //toolbarPanel.SetActive(!toolbarPanel.activeInHierarchy);
            #endregion
        }

        public void Open()
        {
            inventoryPanel.SetActive(true);
            statusPanel.SetActive(true);
        }

        public void Close()
        {
            // I 키를 누르면 인벤토리,TopPanel UI를 활성화/비활성화
            inventoryPanel.SetActive(false);
            statusPanel.SetActive(false);

            //인벤토리 UI가 활성화되면 툴바 UI를 비활성화
            #region 추후 필요시 활성화
            //toolbarPanel.SetActive(!toolbarPanel.activeInHierarchy);
            #endregion
        }
    }
}