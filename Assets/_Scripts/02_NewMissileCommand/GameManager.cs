using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Tower tower = null;
    private InputMouse inputMouse = null;

    private void Awake()
    {
        GameObject towerGo = GameObject.FindWithTag("Tower");   // ���̾��Ű�� �ִ� ��ü�߿� �±׷� ã��
        tower = towerGo.GetComponent<Tower>();
        inputMouse = InputMouse.Instance;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Vector3.zero;
            if (inputMouse.Picking("Stage", ref point))
            {
                tower.Attack(point, HitCallback);
            }
        }
    }

    public void HitCallback(List<Enemy> _hitList)
    {
        foreach (Enemy enemy in _hitList)
        {
            Destroy(enemy.gameObject);
        }
    }

}
