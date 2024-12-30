using UnityEngine;

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
        #endregion

        // 컴포넌트가 활성화될 때 호출되는 메서드
        private void Awake()
        {
            // 캐릭터 컨트롤러와 Rigidbody2D 컴포넌트를 가져옴
            character = GetComponent<CharacterController2D>();
            rgbd2d = GetComponent<Rigidbody2D>();
        }

        // 매 프레임마다 호출되는 메서드
        private void Update()
        {
            // 마우스 왼쪽 버튼이 눌렸을 때 도구 사용 메서드 실행
            if (Input.GetMouseButtonDown(0))
            {
                UseTool();
            }
        }

        // 캐릭터가 도구를 사용하여 상호작용하는 메서드
        private void UseTool()
        {
            // 캐릭터의 위치에서 오프셋 거리만큼 떨어진 위치를 계산
            Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;

            // 지정된 위치에서 상호작용 가능한 객체들을 감지
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

            // 감지된 객체들 중에서 ToolHit 컴포넌트를 가진 객체를 찾음
            foreach (Collider2D collider in colliders)
            {
                ToolHit hit = collider.GetComponent<ToolHit>();

                // 객체가 ToolHit 컴포넌트를 가지고 있다면 Hit 메서드를 호출하고 반복문 종료
                if (hit != null)
                {
                    hit.Hit();
                    break;
                }
            }
        }
    }
}
