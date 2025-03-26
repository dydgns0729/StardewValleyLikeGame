using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    // "Crops Container"라는 이름으로 ScriptableObject를 생성할 수 있도록 메뉴에 추가
    [CreateAssetMenu(menuName = "Data/Crops Container")]
    public class CropsContainer : ScriptableObject
    {
        #region Variables
        // CropTile 객체를 저장하는 리스트 (작물 정보 저장)
        public List<CropTile> crops;
        #endregion

        // 주어진 위치에 해당하는 CropTile을 반환하는 메서드
        // 위치에 맞는 CropTile이 없다면 null을 반환
        public CropTile Get(Vector3Int position)
        {
            // crops 리스트에서 위치가 일치하는 첫 번째 CropTile을 반환
            return crops.Find(c => c.position == position);
        }

        // 새로운 CropTile을 crops 리스트에 추가하는 메서드
        public void Add(CropTile crop)
        {
            // 리스트에 새 CropTile 객체를 추가
            crops.Add(crop);
        }
    }
}