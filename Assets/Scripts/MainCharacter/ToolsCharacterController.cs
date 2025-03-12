using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyStardewValleylikeGame
{
    // 캐릭터가 도구로 상호작용할 수 있도록 하는 컨트롤러 클래스
    public class ToolsCharacterController : MonoBehaviour
    {
        #region Variables
        // 캐릭터 컨트롤러 스크립트를 참조하는 변수
        CharacterController2D character;
        // Rigidbody2D 컴포넌트를 참조하는 변수
        Rigidbody2D rgbd2d;

        // 상호작용할 위치의 오프셋 거리
        [SerializeField] float offsetDistance = 1f;
        // 상호작용 가능한 영역의 크기
        [SerializeField] float sizeOfInteractableArea = 1.2f;
        // 타일선택이 가능한 최대 거리
        [SerializeField] float maxDistance = 1.5f;

        // 마커 매니저를 참조하는 변수
        [SerializeField] MarkerManager markerManager;
        // 타일맵 리드 컨트롤러를 참조하는 변수
        [SerializeField] TileMapReadController tileMapReadController;

        // 타일 선택을 위한 변수
        Vector3Int selectedTilePosition;
        // 선택 가능한 타일인지 여부
        bool selectable;

        // 툴바 컨트롤러를 참조하는 변수
        ToolbarController toolbarController;
        // 애니메이터 컴포넌트를 참조하는 변수
        Animator animator;
        #endregion

        // 컴포넌트가 활성화될 때 호출되는 메서드
        private void Awake()
        {
            // 캐릭터 컨트롤러와 Rigidbody2D 컴포넌트를 가져옴
            character = GetComponent<CharacterController2D>();
            // Rigidbody2D 컴포넌트를 가져옴
            rgbd2d = GetComponent<Rigidbody2D>();
            // 툴바 컨트롤러를 가져옴
            toolbarController = GetComponent<ToolbarController>();
            // 애니메이터 컴포넌트를 가져옴
            animator = GetComponent<Animator>();
        }

        // 매 프레임마다 호출되는 메서드
        private void Update()
        {
            // 타일 위치벡터값 확인 메서드 실행
            SelectTile();

            // 마커표시 여부 확인 메서드 실행
            CanSelectCheck();

            // 마커 메서드 실행
            Marker();

            // 마우스 왼쪽 버튼이 눌렸을 때 도구 사용 메서드 실행
            if (Input.GetMouseButtonDown(0))
            {
                // 도구 사용 메서드 실행
                if (UseToolWorld()) return;
                // 타일맵을 이용한 도구 사용 메서드 실행
                UseToolGrid();
            }
        }

        // 타일의 위치벡터값을 가져옴
        private void SelectTile()
        {
            selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
        }

        // 타일 선택이 가능한지 확인하는 메서드
        void CanSelectCheck()
        {
            // 캐릭터의 위치와 마우스 위치 사이의 거리를 계산
            Vector2 characterPosition = transform.position;
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
            // 마커 매니저에 선택 가능 여부를 전달
            markerManager.Show(selectable);
        }

        // 마커를 표시하는 메서드
        private void Marker()
        {
            markerManager.markedCellPosition = selectedTilePosition;
        }

        // 캐릭터가 도구를 사용하여 상호작용하는 메서드
        private bool UseToolWorld()
        {
            // 캐릭터의 위치에서 오프셋 거리만큼 떨어진 위치를 계산
            Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;

            // 툴바 컨트롤러에서 현재 선택된 아이템을 가져옴
            Item item = toolbarController.GetItem;
            // 아이템이 없다면 false 반환
            if (item == null) return false;
            // 아이템의 사용 메서드가 없다면 false 반환
            if (item.onAction == null) return false;

            // 애니메이션 트리거를 실행
            animator.SetTrigger("act");

            bool complete = item.onAction.OnApply(position);

            if (complete)
            {
                if (item.onItemUsed != null)
                {
                    // 아이템 사용 메서드 실행
                    item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventoryContainer);
                }
            }

            return complete;
        }

        // 타일맵을 이용하여 도구를 사용하는 메서드
        private void UseToolGrid()
        {
            if (selectable)
            {

                // 툴바 컨트롤러에서 현재 선택된 아이템을 가져옴
                Item item = toolbarController.GetItem;
                // 아이템이 없다면 false 반환
                if (item == null) return;
                // 아이템에 타일맵 액션 메서드가 없다면 false 반환
                if (item.onTileMapAction == null) return;

                // 애니메이션 트리거를 실행
                animator.SetTrigger("act");

                // 아이템의 타일맵 액션 메서드를 실행
                bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController, item);
                if (complete)
                {
                    if (item.onItemUsed != null)
                    {
                        // 아이템 사용 메서드 실행
                        item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventoryContainer);
                    }
                }
            }
        }
    }
}
