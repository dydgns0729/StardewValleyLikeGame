using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    [CreateAssetMenu(menuName = "Data/Dialogue/Dialogue")]
    public class DialogueContainer : ScriptableObject
    {
        // NPC가 보여줄 대화내용
        public List<string> line;
        // NPC
        public Actor actor;
    }
}