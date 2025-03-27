using UnityEngine;
using UnityEngine.Tilemaps;

//250326
namespace MyStardewValleylikeGame
{
    // 타일 위에 표시되는 하이라이트 아이콘을 관리하는 클래스
    public class IconHighlight : MonoBehaviour
    {
        #region Variables
        public Vector3Int cellPosition; // 현재 하이라이트가 위치할 셀 좌표
        Vector3 targetPosition; // 셀 좌표를 월드 좌표로 변환한 값

        [SerializeField] Tilemap targetTilemap; // 타일맵 참조
        SpriteRenderer spriteRenderer; // 하이라이트를 그릴 SpriteRenderer

        bool canSelect; // 선택 가능한지 여부
        bool show; // 하이라이트를 보여줄지 여부

        // 선택 가능 여부 설정
        public bool CanSelect
        {
            set
            {
                canSelect = value;
                // 선택 가능하면서 show가 true일 때만 오브젝트 활성화
                gameObject.SetActive(canSelect && show);
            }
        }

        // 하이라이트 표시 여부 설정
        public bool Show
        {
            set
            {
                show = value;
                // show가 true이고 선택 가능할 때만 오브젝트 활성화
                gameObject.SetActive(canSelect && show);
            }
        }
        #endregion

        private void Update()
        {
            // 셀 좌표를 월드 좌표로 변환
            targetPosition = targetTilemap.CellToWorld(cellPosition);

            // 셀의 중앙에 위치하도록 위치 보정
            transform.position = targetPosition + targetTilemap.cellSize / 2;
        }

        // 하이라이트 아이콘 설정
        public void Set(Sprite icon)
        {
            // SpriteRenderer가 할당되어 있지 않으면 가져오기
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

            // 전달받은 아이콘으로 스프라이트 설정
            spriteRenderer.sprite = icon;
        }
    }
}
