using UnityEngine;

namespace MyStardewValleylikeGame
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Data/Item")]
    public class Item : ScriptableObject
    {
        public new string name;     
        public bool stackable;
        public Sprite icon;
    }
}