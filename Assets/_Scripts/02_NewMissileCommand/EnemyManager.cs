using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject enemyPrefab = null;

    [SerializeField] private int maxEnemyCount = 20;

    private void Awake()
    {
        // ���ø� �̿� ���. ���ҽ��� �ҷ��� �� �� ����� ���� ������.
        enemyPrefab = Resources.Load<GameObject>("_Prefabs\\P_Enemy");
        // GameObject[] enemyPrefab = Resources.LoadAll<GameObject>("_Prefabs");   // �迭�� �ҷ����� ���
        
        // �ҷ��°��� ����� ����ȯ. ������ ���ӿ�����Ʈ���� �Ǵ��� ����. ������ �ٲ�. ����� �� ���� ���� �� ����
        enemyPrefab = (GameObject)Resources.Load("_Prefabs\\P_Enemy");
        
        // ����� ����ȯ. �θ� �ڽİ��迡�� �θ��ڽİ��谡 �´��� �˻��Ѵ�. �˻縦 �ؼ� ��ȯ�� �������� ������ null�� ����. Ŭ������ �ȴ�.
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
