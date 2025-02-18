using System.Collections;
using System.Collections.Generic;
using UnityEditor;  // UnityEditor 관련 기능을 사용하기 위해 필요
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // Unity 에디터에서 `ItemContainer`를 커스텀하기 위한 클래스
    [CustomEditor(typeof(ItemContainer))]  // `ItemContainer`의 Custom Editor임을 지정
    public class ItemContainerEditor : Editor
    {
        // 인스펙터 창에서 UI를 커스텀하는 함수 (기본적으로 Unity에서 제공)
        public override void OnInspectorGUI()
        {
            // 현재 선택된 `ItemContainer` 객체를 가져옴
            ItemContainer container = target as ItemContainer;

            // "Clear Container" 버튼을 생성하고, 클릭 시 모든 슬롯을 초기화
            if (GUILayout.Button("Clear Container"))
            {
                // `slots` 리스트를 순회하며 모든 아이템과 개수를 초기화
                for (int i = 0; i < container.slots.Count; i++)
                {
                    container.slots[i].Clear();
                }
            }
            // 기본적인 인스펙터 UI를 그대로 그려줌 (원래 `ItemContainer`가 가지고 있는 속성 표시)
            DrawDefaultInspector();
        }
    }
}
