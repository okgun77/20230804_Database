using UnityEngine;

public interface IPoolingObject
{
    public void Init(GameObject _target, AttackDelegate _attackCallback);
    public void Release();
}