using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class PickUpItem : MonoBehaviour
    {
        //따라갈 타겟 (플레이어)오브젝트의 트랜스폼
        Transform player;
        //이동속도
        [SerializeField] float speed = 5f;
        //반응할 범위(캐릭터와의 거리)
        [SerializeField] float pickUpDistance = 1.5f;
        //이 오브젝트가 존재하는 시간
        [SerializeField] float timeToLive = 10f;

        private void Update()
        {
            
        }

    }
}