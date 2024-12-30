using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // TreeCuttable 클래스는 ToolHit 클래스에서 상속받아, 나무를 베는 동작을 정의합니다.
    public class TreeCuttable : ToolHit
    {
        #region Variables
        [SerializeField] GameObject dropItem; // 떨어질 아이템의 프리팹, 인스펙터에서 설정 가능
        [SerializeField] int dropCount; // 떨어질 아이템의 개수를 결정하는 변수
        [SerializeField] float spread = 0.7f; // 아이템이 떨어질 위치의 범위 (spread 설정)
        #endregion

        // Hit 메서드는 나무가 베어질 때 실행되는 함수입니다.
        public override void Hit()
        {
            // 떨어질 아이템 개수를 2에서 6 사이의 랜덤 값으로 설정합니다.
            dropCount = Random.Range(2, 6);

            // dropCount가 0이 될 때까지 반복하여 아이템을 생성합니다.
            while (dropCount > 0)
            {
                dropCount--; // 남은 아이템 개수를 하나씩 줄입니다.

                // 현재 위치를 기반으로 떨어질 아이템의 위치를 랜덤하게 조정합니다.
                Vector3 position = transform.position;
                position.x += spread * Random.value - spread / 2; // x축에 랜덤 오프셋 추가
                position.y += spread * Random.value - spread / 2; // y축에 랜덤 오프셋 추가

                // 설정된 위치에 아이템 프리팹을 인스턴스화(복제)하여 배치합니다.
                GameObject itemGo = Instantiate(dropItem);
                itemGo.transform.position = position;
            }

            // 나무 오브젝트를 삭제합니다.
            Destroy(gameObject);
        }
    }
}
