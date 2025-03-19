using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI targetText;
        [SerializeField] TextMeshProUGUI nameText;

        DialogueContainer currentDialogue;
        int currentTextLine;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PushText();
            }
        }

        private void PushText()
        {
            currentTextLine++;
            if (currentTextLine >= currentDialogue.line.Count)
            {
                Conclude();
            }
            else
            {
                targetText.text = currentDialogue.line[currentTextLine];
            }

        }

        public void Initialize(DialogueContainer dialogueContainer)
        {
            Show(true);
            currentDialogue = dialogueContainer;
            currentTextLine = 0;
            targetText.text = currentDialogue.line[currentTextLine];
        }

        private void Show(bool b)
        {
            gameObject.SetActive(b);
        }

        private void Conclude()
        {
            Debug.Log("The Dialogue has ended");
            Show(false);
        }
    }
}