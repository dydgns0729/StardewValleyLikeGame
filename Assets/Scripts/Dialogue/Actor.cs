using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    [CreateAssetMenu(menuName = "Data/Dialogue/Actor")]
    public class Actor : ScriptableObject
    {
        //NPC 이름
        public new string name;
        //NPC 다이얼로그창 이미지
        public Sprite portrait;
    }
}