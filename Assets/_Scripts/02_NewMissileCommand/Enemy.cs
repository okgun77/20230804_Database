using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Alt + Enter 하면 자동으로 만들어 준다.
public class Enemy : MonoBehaviour, IPoolingObject
{
    private float moveSpeed = 0f;
    private GameObject target = null;

    private EnemyPattern[] patterns = null;
    private Vector3 nonAnchorPos = Vector3.zero;

    private AttackDelegate attackCallback = null;

    public void Init(GameObject _target, AttackDelegate _attackCallback)
    {
        moveSpeed = Random.Range(1f, 3f);
        gameObject.SetActive(true);

        SetTarget(_target);

        patterns = GetComponentsInChildren<EnemyPattern>();
        foreach (EnemyPattern pattern in patterns)
            pattern.Init(target);

        nonAnchorPos = transform.position;

        attackCallback = _attackCallback;
    }

    public void Release()
    {
        gameObject.SetActive(false);
    }

    public bool IsAlive()
    {
        return gameObject.activeSelf;
    }

    public void SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    /* - GPT code
    private void Update()
    {
        if (!target) return;

        LookAtTarget(target);

        Vector3 anchorPos = Vector3.zero;
        nonAnchorPos = Vector3.zero;  // nonAnchorPos 초기화

        foreach (EnemyPattern pattern in patterns)
        {
            if (pattern.IsAnchorType)
                anchorPos += pattern.MovePatternProcess();
            else if (pattern.IsNonAnchorType)
                nonAnchorPos += pattern.MovePatternProcess();
        }

        transform.position += (anchorPos + nonAnchorPos) * Time.deltaTime;  // 위치 업데이트 수정

        if (DistanceWithoutY(target.transform.position, transform.position) < 1f)
        {
            Release();
        }
    }
    */

    
    private void Update()
    {
        if (!target) return;

        

        // 패턴
        Vector3 anchorPos = Vector3.zero;
        
        foreach (EnemyPattern pattern in patterns)
        {
            if (pattern.IsAnchorType)
                anchorPos += pattern.MovePatternProcess();
            else if (pattern.IsNonAnchorType)
                nonAnchorPos += pattern.MovePatternProcess();
            else if (pattern.IsRotationType)
                transform.rotation = pattern.RotatePatternProcess();
        }

        transform.position = anchorPos + nonAnchorPos;


        if (DistanceWithoutY(target.transform.position, transform.position) < 1f)
        {
            // 리셋
            Release();

            attackCallback?.Invoke(1);
        }

    }
    

    private float DistanceWithoutY(Vector3 _lhs, Vector3 _rhs)
    {
        _lhs.y = 0f;
        _rhs.y = 0f;
        return (_lhs - _rhs).magnitude;
    }


}
