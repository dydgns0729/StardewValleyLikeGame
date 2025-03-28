using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyStardewValleylikeGame
{
    public class ItemConvertorInteract : Interactable
    {
        #region Variables
        [SerializeField] Item convertableItem;
        [SerializeField] Item producedItem;
        [SerializeField] int produceditemCount = 1;

        Animator animator;

        ItemSlot itemSlot;

        [SerializeField] float timeToProcess = 5f;
        float timer;
        #endregion

        private void Start()
        {
            itemSlot = new ItemSlot();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (itemSlot == null) return;
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    CompleteItemConversion();
                }
            }
        }

        private void CompleteItemConversion()
        {
            animator.SetBool("Working", false);
            itemSlot.Clear();
            itemSlot.Set(producedItem, produceditemCount);
        }

        public override void Interact(Character character)
        {
            if (itemSlot.item == null)
            {
                if (GameManager.Instance.dragAndDropController.Check(convertableItem))
                {
                    StartItemProcessing();
                }
            }

            if (itemSlot.item != null && timer < 0f)
            {
                GameManager.Instance.inventoryContainer.Add(itemSlot.item, itemSlot.count);
                itemSlot.Clear();
            }
        }

        private void StartItemProcessing()
        {
            animator.SetBool("Working", true);

            itemSlot.Copy(GameManager.Instance.dragAndDropController.itemSlot);
            GameManager.Instance.dragAndDropController.RemoveItem();

            timer = timeToProcess;
        }
    }
}

