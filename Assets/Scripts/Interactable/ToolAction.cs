using UnityEngine;

namespace MyStardewValleylikeGame
{
    // ToolAction 클래스는 도구가 수행할 기본적인 액션을 정의하는 추상적인 클래스입니다.
    // 이 클래스는 '도구'가 상호작용할 때 실행될 수 있는 여러 메서드를 정의하며, 
    // 각 메서드는 서브클래스에서 오버라이드되어 실제 동작을 구현해야 합니다.
    public class ToolAction : ScriptableObject
    {
        // 아래 메서드들의 실제 구현은 서브클래스에서 작성해야 합니다. 기본적으로 인게임에서는 보이면 안되는 로그를 출력합니다

        // 월드 좌표에서 상호작용할 때 호출되는 메서드
        public virtual bool OnApply(Vector2 worldPoint)
        {
            Debug.LogError("OnApply is not implemented");  // 구현되지 않은 메서드임을 알림
            return true;
        }

        // 타일맵에서 상호작용할 때 호출되는 메서드
        public virtual bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
        {
            Debug.LogError("OnApplyToTileMap is not implemented");  // 구현되지 않은 메서드임을 알림
            return true;
        }

        // 아이템을 사용했을 때 호출되는 메서드
        public virtual void OnItemUsed(Item item, ItemContainer itemContainer)
        {
            Debug.LogError("OnItemUsed is not implemented");  // 구현되지 않은 메서드임을 알림
        }
    }
}