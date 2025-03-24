using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class RecipePanel : ItemPanel
    {
        [SerializeField] RecipeList recipeList;  // 레시피 데이터 (ScriptableObject)
        [SerializeField] Crafting crafting;      // 제작이 가능한지 확인을 위한 참조변수

        // 레시피 패널안에 내용을 표시하는 메서드
        public override void Show()
        {
            // inventoryButtons의 갯수만큼 반복하며, 레시피 리스트에서 해당 아이템을 버튼에 설정합니다.
            for (int i = 0; i < inventoryButtons.Count /* 원본 && i < recipeList.recipes.Count*/; i++)
            {
                #region 투니드
                // i가 recipeList.recipes.Count보다 크면, 현재 슬롯을 비웁니다.
                if (i >= recipeList.recipes.Count)
                {
                    inventoryButtons[i].Clean(); // 슬롯을 비우는 메서드 호출
                    continue; // 다음 반복으로 넘어갑니다.
                }
                #endregion

                // 해당 슬롯에 레시피의 출력 아이템을 설정합니다.
                inventoryButtons[i].SetItem(recipeList.recipes[i].output);  // 해당 슬롯의 아이템을 버튼에 설정
            }
        }

        public override void OnClick(int id)
        {
            // 주어진 id가 레시피 리스트의 범위를 초과하면 아무 것도 하지 않고 메서드를 종료합니다.
            if (id >= recipeList.recipes.Count) return;

            // 레시피가 유효하다면, 해당 레시피를 사용하여 아이템을 제작합니다.
            crafting.Craft(recipeList.recipes[id]);
        }
    }
}