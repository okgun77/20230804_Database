using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EP_RotateToTarget : EnemyPattern
{
    public override void Init(GameObject _target)
    {
        base.Init(_target);

        calcType = ECalcType.Rotation;
    }

    public override Quaternion RotatePatternProcess()
    {
        Vector3 myPos = transform.position;
        myPos.y = 0f;
        Vector3 targetPos = target.transform.position;
        targetPos.y = 0f;
        Vector3 dir = (targetPos - myPos).normalized;

        return Quaternion.LookRotation(dir);
    }
}
