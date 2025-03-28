using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ItemContainerInteractController : MonoBehaviour
    {
        #region Variables
        // 대상 아이템 컨테이너 (아이템이 저장된 장소)
        ItemContainer targetItemContainer;

        // 인벤토리 컨트롤러, 인벤토리 열기/닫기 등 관리
        InventoryController inventoryController;

        // 아이템 컨테이너 패널, UI의 인벤토리 패널
        [SerializeField] ItemContainerPanel itemContainerPanel;

        // 열려있는 상자에 대한 참조
        Transform openedChest;

        // 상호작용 가능한 최대 거리 (상자와의 거리)
        [SerializeField] float maxDistance = 0.8f;
        #endregion

        // Awake는 이 컴포넌트가 활성화될 때 호출된다. 초기화 작업을 여기서 한다.
        private void Awake()
        {
            // 인벤토리 컨트롤러 컴포넌트를 찾는다.
            inventoryController = GetComponent<InventoryController>();
        }

        // Update는 매 프레임마다 호출된다. 여기서 상자와의 거리를 체크한다.
        void Update()
        {
            // 열려있는 상자가 있을 경우에만 거리 체크를 한다.
            if (openedChest != null)
            {
                // Collider2D 컴포넌트를 통해 상자와의 가장 가까운 점을 계산한다.
                Vector3 closestPoint = openedChest.GetComponent<Collider2D>().ClosestPoint(transform.position);

                // 상자와의 거리 계산
                float distance = Vector3.Distance(closestPoint, transform.position);

                // 상자와의 거리가 최대 거리보다 멀어졌다면, 상자를 닫는다.
                if (distance > maxDistance)
                {
                    // LootContainerInteract 컴포넌트의 Close 메서드를 호출하여 상자를 닫는다.
                    openedChest.GetComponent<LootContainerInteract>().Close(GetComponent<Character>());
                }
            }
        }

        // 상자를 열 때 호출되는 함수, 인벤토리와 상자 패널을 활성화한다.
        public void Open(ItemContainer itemContainer, Transform _openedChest)
        {
            // 전달받은 아이템 컨테이너를 설정한다.
            targetItemContainer = itemContainer;

            // UI 패널에 해당 아이템 컨테이너를 할당한다.
            itemContainerPanel.inventory = targetItemContainer;

            // 인벤토리 컨트롤러를 열어준다.
            inventoryController.Open();

            // 아이템 컨테이너 패널을 활성화한다.
            itemContainerPanel.gameObject.SetActive(true);

            // 열린 상자에 대한 참조를 저장한다.
            openedChest = _openedChest;
        }

        // 상자를 닫을 때 호출되는 함수, 인벤토리와 패널을 비활성화한다.
        public void Close()
        {
            // 인벤토리 컨트롤러를 닫는다.
            inventoryController.Close();

            // 아이템 컨테이너 패널을 비활성화한다.
            itemContainerPanel.gameObject.SetActive(false);

            // 열린 상자에 대한 참조를 null로 설정한다.
            openedChest = null;
        }
    }
}
