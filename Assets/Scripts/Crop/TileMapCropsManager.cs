using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


namespace MyStardewValleylikeGame
{
    public class TileMapCropsManager : TimeAgent
    {
        #region Variables
        //밭을 간 상태(갈아엎어진 땅)의 타일 
        [SerializeField] TileBase plowed;
        //씨앗을 심은 상태의 타일
        [SerializeField] TileBase seeded;
        //타일이 배치될 Tilemap 오브젝트
        Tilemap targetTilemap;
        //작물의 스프라이트 이미지를 적용시킬 프리팹
        [SerializeField] GameObject cropsSpritePrefab;
        // 작물 정보를 저장하는 컨테이너
        [SerializeField] CropsContainer cropsContainer;

        #endregion
        private void Start()
        {
            // GameManager에서 CropsManager를 초기화
            GameManager.Instance.GetComponent<CropsManager>().cropsManager = this;
            // Tilemap 컴포넌트를 가져옴
            targetTilemap = GetComponent<Tilemap>();
            // Time Agent의 onTimeTick이 호출될 때마다 Tick 메서드 호출
            onTimeTick += Tick;
            // Time Agent의 초기화 메서드 호출
            Init();
            // 타일맵에 작물 상태를 시각적으로 표시
            VisualizeMap();
        }

        private void VisualizeMap()
        {
            // cropsContainer에 저장된 모든 작물의 상태를 타일맵에 시각적으로 표시
            for (int i = 0; i < cropsContainer.crops.Count; i++)
            {
                VisualizeTile(cropsContainer.crops[i]);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            // 모든 작물에 대해 렌더러를 null로 설정하여 메모리 해제
            for (int i = 0; i < cropsContainer.crops.Count; i++)
            {
                cropsContainer.crops[i].renderer = null;
            }
        }

        //시간 이벤트 발생 주기에 따라 호출되는 메서드
        public void Tick()
        {
            // 타일맵이 null이면 에러 로그를 출력하고 반환
            if (targetTilemap == null)
            {
                Debug.LogError("Target Tilemap is null");
                return;
            }

            //모든 밭을 순회하며 성장 시간을 체크
            foreach (CropTile cropTile in cropsContainer.crops)
            {
                //해당 위치에 심어진 작물이 없다면 다음으로 넘어감
                if (cropTile.crop == null) continue;

                //틱당 데미지 0.02씩 누적
                cropTile.damage += 0.02f;
                //데미지가 1이상되면 작물과 땅을 밭이 갈린상태로 초기화
                if (cropTile.damage >= 1f)
                {
                    // 작물 상태 초기화
                    cropTile.Harvested();
                    // 타일을 갈아엎은 상태로 설정
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

                // 성장 시간이 현재 성장 단계의 지정된 시간에 도달했으면, 
                if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
                {
                    cropTile.renderer.gameObject.SetActive(true);
                    // 해당 성장 단계에 맞는 스프라이트를 적용
                    cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                    // 성장 단계를 하나 증가시켜서 다음 단계로 진행
                    cropTile.growStage++;
                }
            }
        }

        // 특정 위치에 심어진 작물이 있는지 확인하는 메서드
        public bool Check(Vector3Int position)
        {
            // 해당 위치의 CropTile이 존재하면 true 반환, 없으면 false
            return cropsContainer.Get(position) != null;
        }

        // 씨앗이 심어져 있는지 확인하는 메서드
        public bool SeedCheck(Vector3Int postion)
        {
            // 해당 위치의 CropTile이 존재하고 그 위치에 씨앗이 심어져 있으면 true 반환
            return cropsContainer.Get(postion).crop != null;
        }

        // 씨앗을 심을 수 있는지 확인하는 메서드 (현재 이름이 적절한지 고민 중)
        public void Seed(Vector3Int position, Crop toSeed)
        {
            // 해당 위치의 CropTile 객체 가져오기
            CropTile tile = cropsContainer.Get(position);

            // CropTile이 null이면, 즉 해당 위치에 타일이 없다면 아무것도 하지 않음
            if (tile == null) return;

            // 해당 위치에 씨앗을 심은 타일을 배치
            targetTilemap.SetTile(position, seeded);

            // 해당 위치의 CropTile 객체에 씨앗 정보를 저장
            tile.crop = toSeed;
        }

        public void VisualizeTile(CropTile cropTile)
        {
            // 해당 위치의 타일에 대한 시각적 상태 설정
            targetTilemap.SetTile(cropTile.position, cropTile.crop != null ? seeded : plowed);

            // 만약 cropTile에 renderer가 없다면 (즉, 스프라이트 렌더러가 없으면)
            if (cropTile.renderer == null)
            {
                // 새 GameObject를 인스턴스화하여 해당 위치에 배치
                GameObject go = Instantiate(cropsSpritePrefab, transform);
                go.transform.position = targetTilemap.CellToWorld(cropTile.position);  // 타일의 그리드 좌표를 월드 좌표로 변환
                go.SetActive(false);  // 초기 상태에서는 스프라이트가 보이지 않도록 설정
                cropTile.renderer = go.GetComponent<SpriteRenderer>();  // SpriteRenderer 컴포넌트를 해당 GameObject에 할당
            }

            // cropTile이 성장 중인지 확인 (성장 시간이 첫 번째 단계 이상인 경우)
            bool growing = cropTile.crop != null && cropTile.growTimer >= cropTile.crop.growthStageTime[0];

            // 성장 중일 경우에만 렌더러의 게임 오브젝트를 활성화
            cropTile.renderer.gameObject.SetActive(growing);

            // 만약 성장 중이면, 현재 성장 단계에 맞는 스프라이트를 설정
            if (growing)
            {
                // 성장 단계에 맞는 스프라이트를 cropTile.renderer에 적용
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage - 1];
            }
        }

        //특정 위치의 땅을 가는 메서드
        public void Plow(Vector3Int position)
        {
            if (Check(position) == true) return;
            // 해당 위치에 밭을 갈기 위해 CreatePlowedTile 메서드 호출
            CreatePlowedTile(position);
        }

        // 실제로 타일을 변경하는 메서드 (밭을 간 상태로 만듦)
        private void CreatePlowedTile(Vector3Int position)
        {
            // 새 CropTile 객체 생성
            CropTile crop = new CropTile();
            cropsContainer.Add(crop);  // CropTile을 cropsContainer에 추가

            crop.position = position;  // 해당 위치 저장

            VisualizeTile(crop);

            // 해당 위치에 밭을 간 타일을 배치
            targetTilemap.SetTile(position, plowed);
        }


        // 주어진 위치에서 작물을 수확하는 메서드
        public void PickUp(Vector3Int gridPosition)
        {
            // gridPosition을 Vector2Int로 변환하여 타일의 위치를 추출
            Vector2Int position = (Vector2Int)gridPosition;

            // 해당 위치의 CropTile 객체를 가져옴
            CropTile tile = cropsContainer.Get(gridPosition);

            // 해당 위치에 심어진 작물이 없다면 수확을 진행하지 않고 종료
            if (tile == null) return;

            // 작물이 완전히 성장했으면 수확을 진행
            if (tile.Complete)
            {
                // 수확된 아이템을 월드에 생성
                ItemSpawnManager.Instance.SpawnItem(
                    targetTilemap.CellToWorld(gridPosition) + new Vector3(0.5f, 0.5f, 0),  // 월드 좌표로 변환된 타일 위치
                    tile.crop.yield,  // 수확할 아이템 타입
                    tile.crop.yieldAmount  // 수확할 아이템의 수량
                );
                // 수확 후, 밭이 갈려있는 상태로 돌려놓음
                targetTilemap.SetTile(gridPosition, plowed);

                // 수확 후, 작물의 상태 초기화 (작물이 더 이상 존재하지 않게 됨)
                tile.Harvested();
                VisualizeTile(tile);
            }
        }
    }
}

