using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public enum ResourceNodeType
    {
        Undefined = -1,
        Tree,
        Ore
    }

    [CreateAssetMenu(menuName = "Data/Tool Action/Gather Resource Node")]
    public class GatherResourceNode : ToolAction
    {
        // 상호작용 가능한 객체를 감지할 범위
        [SerializeField] float sizeOfInteractableArea = 1f;
        // 상호작용 가능한 객체의 종류
        [SerializeField] List<ResourceNodeType> canHitNodesOfType;

        public override bool OnApply(Vector2 worldPoint)
        {
            // 지정된 위치에서 상호작용 가능한 객체들을 감지
            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);

            // 감지된 객체들 중에서 ToolHit 컴포넌트를 가진 객체를 찾음
            foreach (Collider2D collider in colliders)
            {
                ToolHit hit = collider.GetComponent<ToolHit>();

                // 객체가 ToolHit 컴포넌트를 가지고 있다면 Hit 메서드를 호출하고 반복문 종료
                if (hit != null)
                {
                    if (hit.CanBeHit(canHitNodesOfType))
                    {
                        hit.Hit();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}