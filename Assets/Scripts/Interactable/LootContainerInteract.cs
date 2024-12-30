using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class LootContainerInteract : Interactable
    {
        #region Variables
        [SerializeField] GameObject openedChest;
        [SerializeField] GameObject closedChest;
        [SerializeField] bool isOpen;
        #endregion

        public override void Interact()
        {
            if(!isOpen)
            {
                isOpen = true;
                closedChest.SetActive(false);
                openedChest.SetActive(true);
            }
        }
    }
}