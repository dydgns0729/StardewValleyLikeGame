using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // `PanelGroup`은 UI 패널들을 관리하는 클래스입니다.
    // 여러 개의 패널을 제어하고, 특정 패널을 활성화하거나 비활성화하는 역할을 합니다.
    public class PanelGroup : MonoBehaviour
    {
        #region Variables
        // UI 패널들을 관리하는 리스트입니다. 여러 개의 `GameObject`를 포함하여 패널을 저장합니다.
        // 각 패널은 활성화/비활성화하여 화면에 표시하거나 숨길 수 있습니다.
        public List<GameObject> panels;
        #endregion

        // 지정된 `idPanel`에 해당하는 패널만 활성화하고 나머지 패널들은 비활성화합니다.
        // 패널을 전환할 때 사용됩니다.
        // `idPanel`은 활성화할 패널의 인덱스입니다.
        public void Show(int idPanel)
        {
            // 패널 리스트를 순회하면서 각 패널의 활성화 여부를 설정합니다.
            for (int i = 0; i < panels.Count; i++)
            {
                // 현재 인덱스 `i`가 `idPanel`과 같으면 해당 패널을 활성화하고, 아니면 비활성화합니다.
                panels[i].SetActive(i == idPanel);
            }
        }
    }
}
