using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    [CreateAssetMenu(menuName = "Data/Dialogue/Actor")]
    public class Actor : ScriptableObject
    {
        public string name;
        public Sprite portrait;
    }
}