using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Vector3 targetPos = Vector3.zero;
    private bool isInit = false;
    private Vector3 moveDir = Vector3.zero;
    private float speed = 20f;

    private Explosion explosion = null;

    private Explosion.HitCallback hitCallback = null;

    private int number = 0;
    private MissileStateDelegate missileStateCallback = null;


    private void Awake()
    {
        explosion = GetComponentInChildren<Explosion>();
    }

    private void OnEnable()
    {
        missileStateCallback?.Invoke(number, false);
    }

    private void OnDisable()
    {
        missileStateCallback?.Invoke(number, true);
    }

    public void Init(
        Vector3 _spawnPos,
        Quaternion _spawnRot,
        Vector3 _targetPos,
        MissileStateDelegate _missileStateCallback,
        Explosion.HitCallback _hitCallback = null)
    {

        transform.position = _spawnPos;
        transform.rotation = _spawnRot;

        targetPos = _targetPos;
        targetPos.y = transform.position.y;
        moveDir = (targetPos - transform.position).normalized;
        isInit = true;

        missileStateCallback = _missileStateCallback;

        hitCallback = _hitCallback;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isInit == false) return;

        // transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.position = transform.position + (moveDir * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            // Destroy(gameObject);
            explosion.Activate(ExplosionFinish, hitCallback);
            // TODO: 상태 변경
            isInit = false;
        }
    }

    public void ExplosionFinish()
    {
        gameObject.SetActive(false);
    }

    public void SetNumber(int _number)
    {
        number = _number;
    }

    public int GetNumber()
    {
        return number;
    }
}