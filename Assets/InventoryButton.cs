using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyStardewValleylikeGame
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField]
        Image icon;
        [SerializeField]
        TextMeshProUGUI text;

        int myIndex;

        public void SetIndex(int index) => myIndex = index;

        public void SetItem(ItemSlot slot)
        {
            icon.gameObject.SetActive(true);
            icon.sprite = slot.item.icon;
            if (slot.item.stackable)
            {
                text.gameObject.SetActive(true);
                text.text = slot.count.ToString();
            }
            else
            {
                text.gameObject.SetActive(false);
                text.text = "";
            }
        }

        public void Clean()
        {
            icon.sprite = null;
            icon.gameObject.SetActive(false);
            text.text = "";
            text.gameObject.SetActive(false);
        }
    }
}