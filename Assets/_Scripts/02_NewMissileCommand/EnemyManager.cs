using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject enemyPrefab = null;

    [SerializeField] private int maxEnemyCount = 20;

    // class PoolManager
    // private PoolingObject[] enemies = null;
    // -> Init, Release
    private IPoolingObject[] enemies = null;

    private GameObject target = null;

    private AttackDelegate attackCallback = null;


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

    public void Init(GameObject _target, AttackDelegate _attackCallback)
    {
        target = _target;
        attackCallback = _attackCallback;

        enemies = new IPoolingObject[maxEnemyCount];

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

            enemies[i] = go.GetComponent<IPoolingObject>();
            // ((Enemy)enemies[i]).SetTarget(_target);
            // ((Enemy)enemies[i]).Init(_target);
            ((Enemy)enemies[i]).Release();
        }

        StartCoroutine(RespawnCoroutine());

    }

    private Vector3 GetRandomPosition()
    {
        //Vector2 rnd = Random.insideUnitCircle * 20f;
        //return new Vector3(rnd.x, 0f, rnd.y);

        float angle = Random.Range(0f, 360f);
        float x = Mathf.Cos(angle);
        float z = Mathf.Sin(angle);
        
        return new Vector3(x, 0f, z) * 13f;
    }

    public void SetDamages(List<IPoolingObject> _hitList)
    {
        
        foreach (IPoolingObject enemy in enemies)
        {
            foreach (IPoolingObject target in _hitList)
            {
                if (enemy.Equals(target))
                {
                    // Release
                    // Destroy(enemy.gameObject);
                    enemy.Release();
                    continue;
                }
            }
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        while (true)
        {
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.IsAlive())
                {
                    enemy.SetPosition(GetRandomPosition());
                    enemy.Init(target, attackCallback);
                    break;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
