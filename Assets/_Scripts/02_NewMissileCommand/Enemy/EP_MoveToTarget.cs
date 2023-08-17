using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EP_MoveToTarget : EnemyPattern
{
    [SerializeField] private bool randomSpeed = false;
    [SerializeField][Range(1f, 5f)] private float moveSpeed = 1f;

    public override void Init(GameObject _target)
    {
        base.Init(_target);

        calcType = ECalcType.NonAnchor;

        // MoveToTarget √ ±‚»≠
        if (randomSpeed)
            moveSpeed = Random.Range(1f, 5f);
    }

    public override Vector3 MovePatternProcess()
    {
     
        if (!IsInit) return Vector3.zero;

        return transform.forward * moveSpeed * Time.deltaTime;
    }
}
