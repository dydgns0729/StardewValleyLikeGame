using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class CharacterInteractController : MonoBehaviour
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

        [SerializeReference] HighlightController highlightController; // HighlightController 스크립트 참조 변수

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
            CheckToHighlight(); // 매 프레임마다 하이라이트할 오브젝트를 검사

            // 마우스 오른쪽 버튼이 눌렸을 때 상호작용 메서드 실행
            if (Input.GetMouseButtonDown(1))
            {
                Interact();
            }
        }

        /// <summary>
        /// 캐릭터의 앞에 있는 상호작용 가능한 객체를 찾고, 하이라이팅합니다.
        /// </summary>
        private void CheckToHighlight()
        {
            // 캐릭터의 위치에서 오프셋 거리만큼 떨어진 위치를 계산
            Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;

            // 해당 위치에서 주어진 반경 내에 있는 모든 Collider2D 객체를 감지
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

            // 감지된 각 콜라이더를 순회
            foreach (Collider2D collider in colliders)
            {
                Interactable hit = collider.GetComponent<Interactable>(); // Interactable 컴포넌트를 가진 객체인지 확인

                if (hit != null) // Interactable 객체가 맞으면
                {
                    highlightController.Highlight(hit.gameObject); // 해당 객체를 하이라이팅
                    return; // 첫 번째로 감지된 Interactable 객체만 하이라이팅하고 종료
                }
            }
            //감지된 highlightController가 없으면 Hide() 실행
            highlightController.Hide();
        }

        // 캐릭터가 상호작용하는 메서드
        private void Interact()
        {
            // 캐릭터의 위치에서 오프셋 거리만큼 떨어진 위치를 계산
            Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;

            // 지정된 위치에서 상호작용 가능한 객체들을 감지
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

            // 감지된 객체들 중에서 Interactable 컴포넌트를 가진 객체를 찾음
            foreach (Collider2D collider in colliders)
            {
                Interactable hit = collider.GetComponent<Interactable>();

                // 객체가 Interactable 컴포넌트를 가지고 있다면 Interact 메서드를 호출하고 반복문 종료
                if (hit != null)
                {
                    hit.Interact();
                    break;
                }
            }
        }

    }
}
