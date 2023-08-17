using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern : MonoBehaviour
{
    public enum ECalcType { Anchor, NonAnchor, Rotation }

    protected GameObject target = null;
    protected ECalcType calcType = ECalcType.Anchor;

    private bool isInit = false;

    public ECalcType CalcType { get { return calcType; } }
    public bool IsAnchorType { get { return calcType == ECalcType.Anchor; } }
    public bool IsNonAnchorType { get { return calcType == ECalcType.NonAnchor; } }
    public bool IsRotationType { get { return calcType == ECalcType.Rotation; } }

    protected bool IsInit { get { return isInit; } }

    private void OnEnable()
    {
        if (IsRotationType)
        {
            EnemyPattern[] patterns = GetComponents<EnemyPattern>();
            foreach (EnemyPattern pattern in patterns)
                if (pattern != this && pattern.IsRotationType)
                {
                    Destroy(this);
                    return;
                }
        }
    }

    public virtual void Init(GameObject _target)
    {
        target = _target;

        isInit = true;
    }

    public virtual Vector3 MovePatternProcess()
    {
        return Vector3.zero;
    }

    public virtual Quaternion RotatePatternProcess()
    {
        return Quaternion.identity;
    }
}
