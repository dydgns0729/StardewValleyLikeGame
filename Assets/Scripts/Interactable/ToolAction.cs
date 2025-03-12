using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ToolAction : ScriptableObject
    {
        public virtual bool OnApply(Vector2 worldPoint)
        {
            Debug.LogError("OnApply is not implemented");
            return true;
        }

        public virtual bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
        {
            Debug.LogError("OnApplyToTileMap is not implemented");
            return true;
        }

        public virtual void OnItemUsed(Item item, ItemContainer itemContainer)
        {
            Debug.LogError("OnItemUsed is not implemented");
        }
    }
}