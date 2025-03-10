using UnityEngine;

namespace MyStardewValleylikeGame
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Data/Item")]
    public class Item : ScriptableObject
    {
        public new string name;     //아이템 이름
        public bool stackable;      //쌓을 수 있는지 여부
        public Sprite icon;         //아이템 아이콘 이미지
        public ToolAction onAction; //아이템 사용 시 실행할 도구 액션
        public ToolAction onTileMapAction; //아이템 사용 시 실행할 타일맵 도구 액션
    }
}