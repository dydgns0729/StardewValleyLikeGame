using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // `RecipeList`는 여러 가지 `CraftingRecipe`를 저장하는 리스트를 관리하는 ScriptableObject입니다.
    // 레시피 목록을 정의하고 이를 게임 데이터로 저장하고 관리하는 데 사용됩니다.
    [CreateAssetMenu(menuName = "Data/RecipeList")]  // Unity 에디터에서 "Data/RecipeList" 항목으로 이 객체를 생성할 수 있도록 함
    public class RecipeList : ScriptableObject
    {
        #region Variables
        // `CraftingRecipe` 객체의 리스트입니다.
        // 여러 개의 `CraftingRecipe`를 저장하고 관리합니다.
        // 각 레시피는 특정 아이템을 만들기 위한 재료와 결과를 포함하고 있습니다.
        public List<CraftingRecipe> recipes;
        #endregion
    }
}
