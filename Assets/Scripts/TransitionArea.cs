using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class TransitionArea : MonoBehaviour
    {
        // 충돌이 발생한 객체가 'Player' 태그를 가지고 있을 때 호출되는 메서드
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 충돌한 객체의 태그가 "Player"인 경우
            if (collision.transform.CompareTag("Player"))
            {
                // 부모 객체에서 Transition 컴포넌트를 찾아 InitiateTransition 메서드를 호출
                // 플레이어의 transform을 인자로 넘겨서 전환을 시작
                transform.parent.GetComponent<Transition>().InitiateTransition(collision.transform);
            }
        }
    }
}