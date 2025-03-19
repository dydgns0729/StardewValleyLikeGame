using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class TalkInteract : Interactable
    {
        [SerializeField] DialogueContainer dialogue;

        public override void Interact()
        {
            GameManager.Instance.dialogueSystem.Initialize(dialogue);
        }
    }
}