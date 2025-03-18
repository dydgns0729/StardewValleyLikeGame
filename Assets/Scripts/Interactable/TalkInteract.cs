using UnityEngine;

namespace MyStardewValleylikeGame
{
    /// <summary>
    /// NPC와 대화를 상호작용할 수 있도록 하는 클래스
    /// </summary>
    public class TalkInteract : Interactable
    {
        // NPC와의 대화 내용을 담고 있는 대화 컨테이너 (ScriptableObject)
        [SerializeField] DialogueContainer dialogue;

        /// <summary>
        /// 플레이어가 NPC와 상호작용할 때 실행되는 메서드
        /// </summary>
        public override void Interact()
        {
            // GameManager의 대화 시스템을 통해 대화를 시작
            GameManager.Instance.dialogueSystem.Initialize(dialogue);
        }
    }
}