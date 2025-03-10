using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyStardewValleylikeGame
{
    public class Crops
    {
        //✔ Crops 클래스는 현재 아무것도 구현되어 있지 않음
        //✔ 추후 농작물의 성장 단계, 종류, 수확 가능 여부 등의 정보를 저장하는 용도로 확장될 가능성 있음

        public bool isPlanted;
    }

    //밭을 가는 상태를 관리하는 클래스
    public class CropsManager : MonoBehaviour
    {
        //밭을 간 상태(갈아엎어진 땅)의 타일
        [SerializeField] TileBase plowed;
        //씨앗을 심은 상태의 타일
        [SerializeField] TileBase seeded;
        //타일이 배치될 Tilemap 오브젝트
        [SerializeField] Tilemap targetTilemap;

        //농작물이 있는 위치와 해당 농작물 데이터를 저장하는 딕셔너리
        [SerializeField]
        Dictionary<Vector2Int, Crops> crops;

        private void Start()
        {
            crops = new Dictionary<Vector2Int, Crops>();
        }

        public bool Check(Vector3Int position)
        {
            return crops.ContainsKey((Vector2Int)position);
        }

        //씨앗을 심을 수 있는지 확인하는 메서드 (메서드 이름을 TryPlantSeed로 변경할까 고민중..)
        public void Seed(Vector3Int position)
        {
            Debug.Log("Ready to Plant Seed");
            //해당 위치에 이미 심어진 씨앗이 있다면 리턴
            if (crops[(Vector2Int)position].isPlanted) return;

            //씨앗을 심는 메서드 호출
            CreateSeedTile(position);
        }

        //특정 위치의 땅을 가는 메서드
        public void Plow(Vector3Int position)
        {
            //TODO :: 현재는 밭을 간 상태로만 변경 추후에 SFX,VFX등 추가
            CreatePlowedTile(position);
        }

        //씨앗을 심는 메서드
        public void CreateSeedTile(Vector3Int position)
        {
            //해당 위치에 씨앗을 심은 타일을 배치
            targetTilemap.SetTile(position, seeded);

            //해당 위치의 Crops 객체의 isPlanted를 true로 변경
            crops[(Vector2Int)position].isPlanted = true;

            Debug.Log("Seed Planted");
        }

        //실제로 타일을 변경하는 메서드
        private void CreatePlowedTile(Vector3Int position)
        {
            //Crops 객체를 새로 생성
            Crops crop = new Crops();
            //딕셔너리에 해당 위치와 Crops 객체를 추가
            crops.Add((Vector2Int)position, crop);

            //해당 위치에 밭을 간 타일을 배치
            targetTilemap.SetTile(position, plowed);
        }
    }
}