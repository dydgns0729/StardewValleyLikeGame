using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    [CreateAssetMenu(menuName = "Data/Dialogue/Dialogue")]
    public class DialogueContainer : ScriptableObject
    {
        public List<string> line;
        public Actor actor;
    }
}