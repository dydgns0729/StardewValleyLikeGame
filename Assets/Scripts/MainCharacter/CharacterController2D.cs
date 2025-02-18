using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 스크립트는 2D 캐릭터의 이동과 애니메이션을 처리하는 컨트롤러입니다.
// Rigidbody2D와 Animator를 사용하여 물리 기반의 이동과 애니메이션 동기화를 담당합니다.
namespace MyStardewValleylikeGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    // 이 어트리뷰트는 이 스크립트가 추가된 오브젝트에 반드시 Rigidbody2D가 있어야 함을 명시합니다.
    public class CharacterController2D : MonoBehaviour
    {
        #region Variables

        // 변수 선언
        Rigidbody2D rigidbody2D; // 캐릭터의 물리적 움직임을 담당하는 Rigidbody2D 컴포넌트
        [SerializeField] float speed = 2f; // 캐릭터의 이동 속도를 제어하는 변수, 유니티 에디터에서 설정 가능
        Vector2 motionVector; // 현재 입력에 따른 이동 방향을 저장하는 벡터
        public Vector2 lastMotionVector; // 마지막으로 입력된 이동 방향을 저장하는 벡터
        Animator animator; // 캐릭터의 애니메이션을 관리하는 Animator 컴포넌트
        bool moving; // 캐릭터가 움직이는지 여부를 나타내는 불리언 변수

        #endregion

        // Awake는 스크립트가 활성화될 때 호출되며 초기화를 수행합니다.
        void Awake()
        {
            // Rigidbody2D와 Animator 컴포넌트를 가져옵니다.
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        // Update는 매 프레임마다 호출되며 사용자 입력을 처리합니다.
        private void Update()
        {
            // 입력값을 받습니다.
            float horizontal = Input.GetAxisRaw("Horizontal"); // 왼쪽/오른쪽 방향 입력 (A/D 키 또는 화살표)
            float vertical = Input.GetAxisRaw("Vertical"); // 위/아래 방향 입력 (W/S 키 또는 화살표)

            // 입력에 따른 이동 벡터를 설정합니다.
            motionVector = new Vector2(horizontal, vertical);
            //motionVector = motionVector.normalized * Mathf.Min(motionVector.magnitude, 1f) * speed;
            // 대각선 이동 시 빠른 속도 증가를 막기 위해 입력값을 클램프
            // 속도의 크기를 최대 1로 제한 (정규화 대신 크기 비율을 맞춰줌)
            motionVector = Vector2.ClampMagnitude(motionVector, 1f);

            // 애니메이터의 파라미터를 설정해 입력에 따라 캐릭터 애니메이션을 제어합니다.
            animator.SetFloat("horizontal", horizontal);
            animator.SetFloat("vertical", vertical);

            // 움직임 여부를 판단하고 애니메이션에 전달합니다.
            moving = horizontal != 0 || vertical != 0; // 입력이 있으면 이동 중으로 판단
            animator.SetBool("moving", moving);

            // 만약 캐릭터가 이동 중이라면, 마지막으로 입력된 방향을 저장합니다.
            if (moving)
            {
                lastMotionVector = new Vector2(horizontal, vertical).normalized; // 입력 벡터를 정규화하여 방향만 저장

                // 애니메이터에 마지막으로 입력된 방향을 전달합니다.
                animator.SetFloat("lastHorizontal", horizontal);
                animator.SetFloat("lastVertical", vertical);
            }
        }

        // FixedUpdate는 일정한 시간 간격으로 호출되며 물리 계산을 처리합니다.
        void FixedUpdate()
        {
            Move(); // 캐릭터를 실제로 움직이는 함수 호출
        }

        void Move()
        {
            if (motionVector.magnitude > 0)
            {
                // 이동 중일 때 속도를 설정합니다.
                rigidbody2D.velocity = motionVector * speed;
            }
            else
            {
                // 입력이 없을 때 즉시 속도를 0으로 설정하여 미끄러짐 방지
                rigidbody2D.velocity = Vector2.zero;
            }
        }

    }
}