using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ItemDragAndDropController : MonoBehaviour
    {
        [SerializeField] ItemSlot itemSlot;

        private void Start()
        {
            itemSlot = new ItemSlot();
        }
        internal void OnClick(ItemSlot itemSlot)
        {
            if (this.itemSlot.item == null)
            {
                this.itemSlot.Copy(itemSlot);
                itemSlot.Clear();
            }
            else
            { 

            }
        }
    }
}