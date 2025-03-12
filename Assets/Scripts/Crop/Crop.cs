using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    [CreateAssetMenu(menuName = "Data/Crop")]
    public class Crop : ScriptableObject
    {
        public int timeToGrow = 10; // 성장에 필요한 시간
        public Item yield;          // 수확물
        public int yieldAmount = 1; // 수확량

        public List<Sprite> sprites;      // 작물의 성장단계별 이미지 스프라이트 List
        public List<int> growthStageTime; // 각 성장 단계까지 필요한 시간
    }
}