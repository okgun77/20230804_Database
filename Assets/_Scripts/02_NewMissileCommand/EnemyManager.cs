using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject enemyPrefab = null;

    [SerializeField] private int maxEnemyCount = 20;

    private void Awake()
    {
        // 템플릿 이용 방식. 리소스를 불러올 때 이 방법이 제일 빠르다.
        enemyPrefab = Resources.Load<GameObject>("_Prefabs\\P_Enemy");
        // GameObject[] enemyPrefab = Resources.LoadAll<GameObject>("_Prefabs");   // 배열로 불러오는 방식
        
        // 불러온것을 명시적 형변환. 실제로 게임오브젝트인지 판단을 못함. 강제로 바꿈. 사용할 때 문제 생길 수 있음
        enemyPrefab = (GameObject)Resources.Load("_Prefabs\\P_Enemy");
        
        // 명시적 형변환. 부모 자식관계에서 부모자식관계가 맞는지 검사한다. 검사를 해서 변환이 가능하지 않으면 null이 들어간다. 클래스만 된다.
        enemyPrefab = Resources.Load("_Prefabs\\P_Enemy") as GameObject;
    }

    private void Start()
    {
        for (int i = 0; i < maxEnemyCount; ++i)
        {
            GameObject go =
                Instantiate(
                    enemyPrefab,
                    GetRandomPosition(),
                    Quaternion.identity,
                    transform
                    );
            go.name = "Enemy_" + i;
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 rnd = Random.insideUnitCircle * 20f;
        return new Vector3(rnd.x, 0f, rnd.y);

        //float angle = Random.Range(0f, 360f);
        //float x = Mathf.Cos(angle);
        //float z = Mathf.Sin(angle);
        
        //return new Vector3(x, 0f, z) * 13f;
    }
}
