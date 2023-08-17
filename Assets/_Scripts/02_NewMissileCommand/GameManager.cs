using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // manager : mng, mgr
    [SerializeField] private EnemyManager enemyMng= null;
    [SerializeField] private UI_HUD_HP uiHudHp = null;
    [SerializeField] private UI_HUD_Missile uiHudMissile = null;

    private Tower tower = null;
    private InputMouse inputMouse = null;

    private void Awake()
    {
        GameObject towerGo = GameObject.FindWithTag("Tower");   // 하이어라키에 있는 객체중에 태그로 찾기
        tower = towerGo.GetComponent<Tower>();
        inputMouse = InputMouse.Instance;
    }

    private void Start()
    {
        tower.Init(MissileStateCallback);
        enemyMng.Init(tower.gameObject, EnemyAttackCallback);
        uiHudHp.Init(tower.GetHp());
        uiHudMissile.Init(tower.GetMissileCount());
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

    public void HitCallback(List<IPoolingObject> _hitList)
    {
        //foreach (Enemy enemy in _hitList)
        //{
        //    Destroy(enemy.gameObject);
        //}
        enemyMng.SetDamages(_hitList);
    }

    private void EnemyAttackCallback(int _dmg)
    {
        int hp = tower.Damage(_dmg);
        uiHudHp.UpdateHp(hp);

        if(hp == 0)
        {
            // GAME OVER
            return;
        }

    }
    
    private void MissileStateCallback(int _idx, bool _isFill)
    {
        uiHudMissile.UpdateMissileState(_idx, _isFill);
    }

}
