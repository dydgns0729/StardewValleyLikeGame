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
        [SerializeField] bool isOpen = false;
        [SerializeField] AudioClip onOpenAudio;
        [SerializeField] AudioClip onCloseAudio;
        #endregion

        public override void Interact()
        {
            isOpen = !isOpen;
            openedChest.SetActive(isOpen);
            closedChest.SetActive(!isOpen);
            if (isOpen)
            {
                AudioManager.Instance.Play(onOpenAudio);
            }
            else
            {
                AudioManager.Instance.Play(onCloseAudio);
            }

        }
    }
}