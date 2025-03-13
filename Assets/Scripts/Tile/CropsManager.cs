using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace MyStardewValleylikeGame
{
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
    public class CropsManager : TimeAgent
    {
        #region Variables
        //밭을 간 상태(갈아엎어진 땅)의 타일 
        [SerializeField] TileBase plowed;
        //씨앗을 심은 상태의 타일
        [SerializeField] TileBase seeded;
        //타일이 배치될 Tilemap 오브젝트
        [SerializeField] Tilemap targetTilemap;
        //작물의 스프라이트 이미지를 적용시킬 프리팹
        [SerializeField] GameObject cropsSpritePrefab;

        //농작물이 있는 위치와 해당 농작물 데이터를 저장하는 딕셔너리
        [SerializeField]
        Dictionary<Vector2Int, CropTile> cropsTile;
        #endregion
        private void Start()
        {
            //딕셔너리 초기화
            cropsTile = new Dictionary<Vector2Int, CropTile>();
            onTimeTick += Tick;
            Init();
        }

        //시간 이벤트 발생 주기에 따라 호출되는 메서드
        public void Tick()
        {
            //모든 밭을 순회하며 성장 시간을 체크
            foreach (CropTile cropTile in cropsTile.Values)
            {
                //해당 위치에 심어진 작물이 없다면 다음으로 넘어감
                if (cropTile.crop == null) continue;

                //틱당 데미지 0.02씩 누적
                cropTile.damage += 0.02f;
                //데미지가 1이상되면 작물과 땅을 밭이 갈린상태로 초기화
                if (cropTile.damage >= 1f)
                {
                    cropTile.Harvested();
                    targetTilemap.SetTile(cropTile.position, plowed);
                    continue;
                }

                //성장한 시간이 작물의 총 성장가능 시간과 같거나 크다면
                if (cropTile.Complete)
                {
                    //더이상 자라지않게 만듬
                    Debug.Log("Crop is Ready to Harvest");
                    continue;
                }

                //성장한 시간을 1씩 증가
                cropTile.growTimer++;

                #region 디버그 확인용
                //Debug.Log("GrowTimer = " + cropTile.growTimer);
                //Debug.Log("GrowStage = " + cropTile.growStage);
                //Debug.Log("growthStageTime = " + cropTile.crop.growthStageTime[cropTile.growStage]);
                #endregion
                // 성장 시간이 현재 성장 단계의 지정된 시간에 도달했으면, 
                if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
                {
                    // 해당 성장 단계에 맞는 스프라이트를 적용
                    cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                    // 성장 단계를 하나 증가시켜서 다음 단계로 진행
                    cropTile.growStage++;
                }
            }
        }

        public bool Check(Vector3Int position)
        {
            // 해당 위치에 밭이 갈려있는지 확인
            return cropsTile.ContainsKey((Vector2Int)position) && !cropsTile[(Vector2Int)position].isPlanted;
        }

        //씨앗을 심을 수 있는지 확인하는 메서드 (메서드 이름을 TryPlantSeed로 변경할까 고민중..)
        public void Seed(Vector3Int position, Crop toSeed)
        {
            Debug.Log("Ready to Plant Seed");
            //해당 위치에 이미 심어진 씨앗이 있다면 리턴
            if (cropsTile[(Vector2Int)position].isPlanted) return;

            //씨앗을 심는 메서드 호출
            CreateSeedTile(position, toSeed);
        }

        //씨앗을 심는 메서드
        public void CreateSeedTile(Vector3Int position, Crop toSeed)
        {
            //해당 위치에 씨앗을 심은 타일을 배치
            targetTilemap.SetTile(position, seeded);

            //해당 위치의 CropTile 객체에 씨앗정보 저장
            cropsTile[(Vector2Int)position].crop = toSeed;
            //해당 위치의 CropTile 객체의 isPlanted를 true로 변경
            cropsTile[(Vector2Int)position].isPlanted = true;

            // 크롭 스프라이트 프리팹을 생성
            GameObject cropGO = Instantiate(cropsSpritePrefab);
            // 위치를 클릭한 타일에 세팅
            cropGO.transform.position = targetTilemap.CellToWorld(position);
            // 스프라이트 렌더러 컴포넌트를 받아옴
            cropsTile[(Vector2Int)position].renderer = cropGO.GetComponent<SpriteRenderer>();
            cropsTile[(Vector2Int)position].renderer.sprite = cropsTile[(Vector2Int)position]
                .crop.sprites[cropsTile[(Vector2Int)position].growStage];

            cropsTile[(Vector2Int)position].position = position;

            Debug.Log("Seed Planted");
        }

        //특정 위치의 땅을 가는 메서드
        public void Plow(Vector3Int position)
        {
            //TODO :: 현재는 밭을 간 상태로만 변경 추후에 SFX,VFX등 추가
            CreatePlowedTile(position);
        }

        //실제로 타일을 변경하는 메서드
        private void CreatePlowedTile(Vector3Int position)
        {
            //CropTile 객체를 새로 생성
            CropTile crop = new CropTile();
            //딕셔너리에 해당 위치와 CropTile 객체를 추가
            if (!cropsTile.ContainsKey((Vector2Int)position))
            {
                cropsTile.Add((Vector2Int)position, crop);
            }
            else
            {
                cropsTile.Remove((Vector2Int)position);
                cropsTile.Add((Vector2Int)position, crop);
            }

            //해당 위치에 밭을 간 타일을 배치
            targetTilemap.SetTile(position, plowed);
        }

        internal void PickUp(Vector3Int gridPosition)
        {
            // gridPosition을 Vector2Int로 변환하여 타일의 위치를 추출
            Vector2Int position = (Vector2Int)gridPosition;

            // 해당 위치에 심어진 작물이 없다면, 즉 cropsTile에 해당 위치가 없다면 수확을 진행하지 않고 종료
            if (!cropsTile.ContainsKey(position)) return;

            // 해당 위치에 심어진 작물 정보를 가져옴
            CropTile cropTile = cropsTile[position];

            // 작물이 완전히 성장했으면 수확을 진행
            if (cropTile.Complete)
            {
                // 수확된 아이템을 월드에 생성
                ItemSpawnManager.Instance.SpawnItem(
                    targetTilemap.CellToWorld(gridPosition) + new Vector3(0.5f, 0.5f, 0),    // 월드 좌표로 변환된 타일 위치
                    cropTile.crop.yield,                        // 수확할 아이템 타입
                    cropTile.crop.yieldAmount                   // 수확할 아이템의 수량
                );
            }
            // 수확 후, 밭이 갈려있는 상태로 돌려놓음
            targetTilemap.SetTile(gridPosition, plowed);

            // 수확 후, 작물의 상태 초기화 (작물이 더 이상 존재하지 않게 됨)
            cropTile.Harvested();
        }
    }
}