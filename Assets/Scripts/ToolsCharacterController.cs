using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ToolsCharacterController : MonoBehaviour
    {
        #region Variables
        // 캐릭터의 2D 움직임을 담당하는 CharacterController2D를 참조
        CharacterController2D character;

        // 캐릭터의 Rigidbody2D를 참조하여 물리적 위치 및 움직임을 계산
        Rigidbody2D rgbd2d;

        // 캐릭터의 상호작용 위치를 캐릭터의 앞쪽으로 얼마나 떨어뜨릴지 결정하는 거리
        [SerializeField] float offsetDistance = 1f;

        // 상호작용할 수 있는 범위의 크기
        [SerializeField] float sizeOfInteractableArea = 1.2f;
        #endregion

        private void Awake()
        {
            // 캐릭터에 부착된 CharacterController2D와 Rigidbody2D를 가져옵니다
            character = GetComponent<CharacterController2D>();
            rgbd2d = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // 마우스 왼쪽 버튼을 눌렀을 때 도구를 사용합니다
            if (Input.GetMouseButtonDown(0))
            {
                UseTool();
            }
        }

        // 캐릭터가 도구를 사용하여 상호작용하는 메서드
        private void UseTool()
        {
            // 캐릭터의 위치에서 lastMotionVector 방향으로 offsetDistance만큼 떨어진 위치를 계산
            Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;

            // 계산된 위치에서 일정한 반경(sizeOfInteractableArea) 안에 있는 모든 콜라이더를 검색
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

            // 검색된 콜라이더들 중에서 ToolHit 컴포넌트를 가진 오브젝트를 찾음
            foreach (Collider2D collider in colliders)
            {
                ToolHit hit = collider.GetComponent<ToolHit>();

                // ToolHit 컴포넌트를 가진 오브젝트가 있다면, 상호작용을 처리하고 반복을 종료
                if (hit != null)
                {
                    hit.Hit();
                    break;
                }
            }
        }
    }
}