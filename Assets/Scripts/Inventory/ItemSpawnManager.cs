using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ItemSpawnManager : PersistentSingleton<ItemSpawnManager>
    {
        //아이템을 생성할 프리팹
        [SerializeField] GameObject pickUpItemPrefab;

        //위치, 아이템, 갯수를 받아서 아이템을 생성한다.
        public void SpawnItem(Vector3 position, Item item, int count = 1)
        {
            //prefab을 생성한다.
            GameObject itemGO = Instantiate(pickUpItemPrefab, position, Quaternion.identity);
            //생성된 prefab에 아이템을 설정한다.
            itemGO.GetComponent<PickUpItem>().Set(item, count);
        }
    }
}