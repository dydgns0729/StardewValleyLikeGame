using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        GameObject inventoryPanel;          //인벤토리 UI

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                // I 키를 누르면 인벤토리 UI를 활성화/비활성화
                inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
            }
        }
    }
}