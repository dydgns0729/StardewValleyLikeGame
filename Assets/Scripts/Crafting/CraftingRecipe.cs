using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // `CraftingRecipe`는 아이템을 만들기 위한 레시피를 나타내는 클래스입니다.
    // 이 클래스는 ScriptableObject로 생성되어 프로젝트에서 데이터 자원으로 사용할 수 있습니다.
    [CreateAssetMenu(menuName = "Data/Recipe")]  // Unity의 메뉴에 'Data/Recipe' 항목으로 생성할 수 있도록 함
    public class CraftingRecipe : ScriptableObject
    {
        #region Variables

        // 여러 개의 `ItemSlot`으로 구성된 리스트. 각각의 항목은 제작에 필요한 아이템과 그 수량을 나타냄.
        public List<ItemSlot> elements;

        // 레시피의 결과로 생성되는 아이템과 그 수량을 나타내는 변수
        public ItemSlot output;

        #endregion
    }
}
