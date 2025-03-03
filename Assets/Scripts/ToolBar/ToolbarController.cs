using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ToolbarController : MonoBehaviour
    {
        #region Variables
        [SerializeField] int toolbarSize = 12; // 툴바 크기
        int selectedTool;                      // 선택된 툴의 인덱스
        #endregion

        private void Update()
        {
            float delta = Input.mouseScrollDelta.y; //마우스 휠 입력을 받음
            //위로 이동시 selectedTool을 1 증가, 아래로 이동시 1 감소
            if (delta != 0)
            {
                if (delta > 0)
                {
                    selectedTool += 1;
                    selectedTool = (selectedTool >= toolbarSize) ? 0 : selectedTool;
                }
                else
                {
                    selectedTool -= 1;
                    selectedTool = (selectedTool < 0) ? toolbarSize - 1 : selectedTool;
                }
                Debug.Log(selectedTool);
            }
        }
    }
}