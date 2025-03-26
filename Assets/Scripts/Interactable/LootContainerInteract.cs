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

        //열릴 때 재생할 오디오
        [SerializeField] AudioClip onOpenAudio;
        //닫힐 때 재생할 오디오
        [SerializeField] AudioClip onCloseAudio;
        #endregion

        public override void Interact()
        {
            // 상호작용 시 isOpen을 반전시키고, 상자의 상태를 열린 상태로 변경
            isOpen = !isOpen;

            openedChest.SetActive(isOpen);
            closedChest.SetActive(!isOpen);

            // 상자가 열리거나 닫힐 때 오디오 재생
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