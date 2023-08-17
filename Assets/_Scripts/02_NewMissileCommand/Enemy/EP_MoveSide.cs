using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EP_MoveSide : EnemyPattern
{
    [SerializeField][Range(5f, 10f)] private float moveSpeed = 5f;
    [SerializeField][Range(1f, 5f)] private float width = 1f;

    private Vector3 oriPos = Vector3.zero;


    public override void Init(GameObject _target)
    {
        base.Init(_target);

        calcType = ECalcType.Anchor;

        oriPos = transform.position;

    }

    public override Vector3 MovePatternProcess()
    {
        
        if (!IsInit) return Vector3.zero;

        float sin = Mathf.Sin(Time.time * moveSpeed);
        float pos = sin * (width * 0.5f);
        // float pos = sin * width;
        return transform.right * pos;
    }
}
