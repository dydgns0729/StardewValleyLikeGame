using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ToolAction : ScriptableObject
    {
        public virtual bool OnApply(Vector2 worldPoint)
        {
            Debug.Log("OnApply is not implemented");
            return true;
        }
    }
}