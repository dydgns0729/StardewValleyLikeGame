using UnityEngine;
using UnityEngine.Tilemaps;

//250326
namespace MyStardewValleylikeGame
{
    public class IconHighlight : MonoBehaviour
    {
        #region Variables
        public Vector3Int cellPosition;
        Vector3 targetPosition;
        [SerializeField] Tilemap targetTilemap;
        SpriteRenderer spriteRenderer;

        bool canSelect;
        bool show;

        public bool CanSelect
        {
            set
            {
                canSelect = value;
                gameObject.SetActive(canSelect && show);
            }
        }

        public bool Show
        {
            set
            {
                show = value;
                gameObject.SetActive(canSelect && show);
            }
        }

        #endregion

        private void Update()
        {
            targetPosition = targetTilemap.CellToWorld(cellPosition);
            transform.position = targetPosition + targetTilemap.cellSize / 2;
        }

        public void Set(Sprite icon)
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

            spriteRenderer.sprite = icon;
        }
    }
}

