using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyStardewValleylikeGame
{
    public class CropTile
    {
        //✔ CropTile 클래스는 현재 아무것도 구현되어 있지 않음
        //✔ 추후 농작물의 성장 단계, 종류, 수확 가능 여부 등의 정보를 저장하는 용도로 확장될 가능성 있음

        public int growTimer;   //성장한 시간
        public int growStage;   //성장한 단계
        public Crop crop;       //작물 정보
        public SpriteRenderer renderer;//작물의 SpriteRenderer를 컨트롤할 변수

        #region 중복체크 (투니드)
        public bool isPlanted;  //작물이 이미 심어졌는지 확인하는 변수
        #endregion
    }

    //밭을 가는 상태를 관리하는 클래스
    public class CropsManager : TimeAgent
    {
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

                //성장한 시간을 1씩 증가
                cropTile.growTimer++;

                Debug.Log("GrowTimer = " + cropTile.growTimer);
                Debug.Log("GrowStage = " + cropTile.crop.growthStageTime[cropTile.growStage]);
                if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
                {
                    //cropTile.renderer.gameObject.SetActive(true);
                    cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                    cropTile.growStage++;
                }
                //성장한 시간이 작물의 성장 시간과 같거나 크다면
                if (cropTile.growTimer >= cropTile.crop.timeToGrow)
                {
                    //더이상 자라지않게 만듬
                    Debug.Log("Crop is Ready to Harvest");
                    cropTile.crop = null;
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
            cropsTile[(Vector2Int)position].renderer.sprite = cropsTile[(Vector2Int)position].crop.sprites[cropsTile[(Vector2Int)position].growStage];
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
    }
}