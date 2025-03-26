using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class Crafting : MonoBehaviour
    {
        [SerializeField] ItemContainer inventory;  // 플레이어의 인벤토리 (아이템을 담고 있는 컨테이너)

        // 레시피를 기반으로 아이템을 제작하는 메서드
        public void Craft(CraftingRecipe recipe)
        {
            // 인벤토리에 빈 공간이 없다면, 제작을 중단합니다.
            if (inventory.CheckFreeSpace() == false) return;

            // 레시피에 필요한 모든 재료가 인벤토리에 있는지 확인
            for (int i = 0; i < recipe.elements.Count; i++)
            {
                // 필요한 재료가 부족하면, 제작을 중단합니다.
                if (inventory.CheckItem(recipe.elements[i]) == false)
                {
                    return;
                }
            }

            // 필요한 재료를 인벤토리에서 제거합니다.
            for (int i = 0; i < recipe.elements.Count; i++)
            {
                inventory.Remove(recipe.elements[i].item, recipe.elements[i].count);  // 아이템 수량만큼 제거
            }

            // 레시피의 완성 아이템을 인벤토리에 추가합니다.
            inventory.Add(recipe.output.item, recipe.output.count);
        }
    }
}
