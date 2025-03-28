using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace MyStardewValleylikeGame
{
    public class LootContainerInteract : Interactable
    {
        #region Variables
        // 열린 상자(GameObject)와 닫힌 상자(GameObject) 참조
        [SerializeField] GameObject openedChest;
        [SerializeField] GameObject closedChest;

        // 상자가 열린 상태인지 여부
        [SerializeField] bool isOpen = false;

        // 상자가 열릴 때 재생할 오디오 클립
        [SerializeField] AudioClip onOpenAudio;

        // 상자가 닫힐 때 재생할 오디오 클립
        [SerializeField] AudioClip onCloseAudio;

        // 아이템 컨테이너 참조 (상자에 담긴 아이템들을 관리)
        [SerializeField] ItemContainer itemContainer;
        #endregion

        // 상호작용 메서드, 상자가 열려있지 않으면 열고, 열려있으면 닫는다.
        public override void Interact(Character character)
        {
            // 상자가 열려있지 않으면 열고, 열려있으면 닫는다.
            if (!isOpen)
            {
                Open(character);  // 상자 열기
            }
            else
            {
                Close(character); // 상자 닫기
            }
        }

        // 상자를 여는 메서드
        public void Open(Character character)
        {
            // 상자를 열었으므로 상태를 변경
            isOpen = true;

            // 열린 상자는 활성화하고, 닫힌 상자는 비활성화
            openedChest.SetActive(true);
            closedChest.SetActive(false);

            // 상자가 열릴 때 재생할 오디오 재생
            AudioManager.Instance.Play(onOpenAudio);

            // 캐릭터의 ItemContainerInteractController를 통해 아이템 컨테이너 UI를 연다.
            character.GetComponent<ItemContainerInteractController>().Open(itemContainer, transform);
        }

        // 상자를 닫는 메서드
        public void Close(Character character)
        {
            // 상자를 닫았으므로 상태를 변경
            isOpen = false;

            // 열린 상자는 비활성화하고, 닫힌 상자는 활성화
            openedChest.SetActive(false);
            closedChest.SetActive(true);

            // 상자가 닫힐 때 재생할 오디오 재생
            AudioManager.Instance.Play(onCloseAudio);

            // 캐릭터의 ItemContainerInteractController를 통해 아이템 컨테이너 UI를 닫는다.
            character.GetComponent<ItemContainerInteractController>().Close();
        }
    }
}
