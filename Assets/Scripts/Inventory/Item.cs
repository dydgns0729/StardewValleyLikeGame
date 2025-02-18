using UnityEngine;

namespace MyStardewValleylikeGame
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Data/Item")]
    public class Item : ScriptableObject
    {
        public new string name;     //아이템 이름
        public bool stackable;      //쌓을 수 있는지 여부
        public Sprite icon;         //아이템 아이콘 이미지
    }
}