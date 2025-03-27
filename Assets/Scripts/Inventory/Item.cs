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
        public ToolAction onItemUsed; //아이템 사용 시 실행할 아이템 사용 액션
        public Crop crop;           //작물 정보
        public bool iconHighlight;  //월드맵에 아이콘 하이라이트 표시 여부
        public GameObject itemPrefab; //설치아이템 프리팹
    }
}