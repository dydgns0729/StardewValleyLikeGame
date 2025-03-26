using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace MyStardewValleylikeGame
{
    [Serializable]
    public class CropTile
    {
        #region Variables
        public int growTimer;   //성장한 시간
        public int growStage;   //성장한 단계
        public Crop crop;       //작물 정보
        public SpriteRenderer renderer;//작물의 SpriteRenderer를 컨트롤할 변수
        public float damage;    //시간이 지남에따라 작물에 누적될 데미지
        public Vector3Int position; //작물이 심어진 위치값을 저장시킬 변수

        #region 중복체크 (투니드)
        public bool isPlanted;  //작물이 이미 심어졌는지 확인하는 변수
        #endregion

        public bool Complete
        {
            get
            {
                // crop이 null이면 false를 반환
                if (crop == null) { return false; }
                // 성장 시간이 crop의 성장 시간을 초과하거나 같으면 수확이 완료된 것으로 간주
                return growTimer >= crop.timeToGrow;
            }
        }
        #endregion
        public void Harvested()
        {
            // 수확 후 작물의 상태를 초기화하는 메서드
            // 성장 시간과 단계, crop 정보를 초기화하여 타일을 비워줌
            growTimer = 0;  // 성장 시간 초기화
            growStage = 0;  // 성장 단계 초기화
            crop = null;    // 현재 심어진 crop 제거
            isPlanted = false; // 작물이 심어진 상태를 초기화
            damage = 0;     // 데미지 초기화

            renderer.gameObject.SetActive(false);  // 작물의 스프라이트 렌더러를 비활성화하여 화면에서 사라지게 함
        }
    }

    //밭을 가는 상태를 관리하는 클래스
    public class CropsManager : MonoBehaviour
    {
        #region Variables
        // TileMapCropsManager를 참조 (작물 관련 관리)
        public TileMapCropsManager cropsManager;
        #endregion

        // cropsManager가 null인지 확인하는 공통 메서드
        private bool CheckCropsManager()
        {
            if (cropsManager == null)
            {
                Debug.LogError("CropsManager is null");
                return false;
            }
            return true;
        }

        // 수확하기 기능 (주어진 위치에서 작물을 수확)
        public void PickUp(Vector3Int position)
        {
            if (!CheckCropsManager()) return;

            cropsManager.PickUp(position); // cropsManager에서 PickUp 메서드 호출
        }

        // 해당 위치에 씨앗을 심을 수 있는지 확인 (작물이 심어졌는지 확인)
        public bool Check(Vector3Int position)
        {
            // cropsManager가 null이면 에러 메시지 출력 후 false 반환
            if (!CheckCropsManager()) return false;

            return cropsManager.Check(position) && !cropsManager.SeedCheck(position); // cropsManager에서 Check 메서드 호출
        }

        // 씨앗을 심는 기능 (주어진 위치에 씨앗을 심음)
        public void Seed(Vector3Int position, Crop toSeed)
        {
            if (!CheckCropsManager()) return;

            cropsManager.Seed(position, toSeed); // cropsManager에서 Seed 메서드 호출
        }

        // 해당 위치의 밭을 갈아엎는 기능
        public void Plow(Vector3Int position)
        {
            if (!CheckCropsManager()) return;

            cropsManager.Plow(position); // cropsManager에서 Plow 메서드 호출
        }
    }
}