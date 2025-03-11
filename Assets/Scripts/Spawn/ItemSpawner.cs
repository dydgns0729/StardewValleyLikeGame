using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    [RequireComponent(typeof(TimeAgent))]
    // 무엇을 몇개 생성할지 정의하는 클래스
    public class ItemSpawner : MonoBehaviour
    {
        #region Variables
        // 생성할 아이템과 개수
        [SerializeField] Item toSpawn;
        [SerializeField] int count;

        [SerializeField] float spread = 2f; // 아이템이 떨어질 위치의 범위 (spread 설정)

        [SerializeField] float probability = 0.5f;// 생성 확률
        #endregion

        private void Start()
        {
            // 시간이 흐를 때마다 Spawn 함수를 호출합니다.
            TimeAgent timeAgent = GetComponent<TimeAgent>();
            if (timeAgent != null)
            {
                timeAgent.onTimeTick += Spawn;
            }
        }

        void Spawn()
        {
            // 확률에 따라 아이템을 생성합니다.
            if (Random.value < probability)
            {
                // 현재 위치를 기반으로 떨어질 아이템의 위치를 랜덤하게 조정합니다.
                Vector3 position = transform.position;
                position.x += spread * Random.value - spread / 2; // x축에 랜덤 오프셋 추가
                position.y += spread * Random.value - spread / 2; // y축에 랜덤 오프셋 추가

                // 설정된 위치에 아이템 프리팹을 인스턴스화(복제)하여 배치합니다.
                ItemSpawnManager.Instance.SpawnItem(position, toSpawn, count);
            }

        }
    }
}