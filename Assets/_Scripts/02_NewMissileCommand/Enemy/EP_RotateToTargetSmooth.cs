using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EP_RotateToTargetSmooth : EnemyPattern
{
    // [SerializeField][Range(1f, 5f)] private float rotSpeed = 3f;
    [SerializeField][Range(0.01f, 1f)] private float rotRatio = 0.3f;

    private float curRotRatio = 0f;

    public override void Init(GameObject _target)
    {
        base.Init(_target);

        calcType = ECalcType.Rotation;

        Vector3 myPos = transform.position;
        myPos.y = 0f;
        Vector3 targetPos = target.transform.position;
        targetPos.y = 0f;
        Vector3 dir = (myPos - targetPos).normalized;
        transform.rotation = Quaternion.LookRotation(dir);

        curRotRatio = rotRatio;
    }

    public override Quaternion RotatePatternProcess()
    {
        Vector3 myPos = transform.position;
        myPos.y = 0f;
        Vector3 targetPos = target.transform.position;
        targetPos.y = 0f;
        Vector3 dir = (targetPos - myPos).normalized;

        Quaternion lerp = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), curRotRatio * 0.01f);
        curRotRatio += 0.0001f;

        return lerp;
    }
}
