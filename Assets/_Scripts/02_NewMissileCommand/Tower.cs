using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    // Liner Interpolation선형보간

    [SerializeField][Range(1f, 10f)] private float rotSpeed = 3f;   // 타워 회전속도
    [SerializeField] private GameObject missilePrefab = null;
    [SerializeField] private int maxMissileCount = 3;

    [SerializeField] private int maxHp = 3;
    private int curHp = 0;

    private MissileSpawnPoint missileSpawnPoint = null;
    private Coroutine attackCoroutine = null;
    private List<Missile> missileList = new List<Missile>();

    private MissileStateDelegate missileStateCallback = null;


    private void Awake()
    {
        missileSpawnPoint = GetComponentInChildren<MissileSpawnPoint>();
    }


    public void Init(MissileStateDelegate _missileStateCallback)
    {
        for (int i = 0; i < maxMissileCount; ++i)
        {
            GameObject go = Instantiate(missilePrefab);
            go.name = "Missile_" + i;
            go.SetActive(false);   // 꺼져있으면 Find로 해도 못찾음. 우리는 목록으로 관리를 해서 상관없음.

            Missile missile = go.GetComponent<Missile>();
            missile.SetNumber(i);
            missileList.Add(missile);
        }
        curHp = maxHp;

        missileStateCallback = _missileStateCallback;
    }

    public void Attack(Vector3 _targetPos, Explosion.HitCallback _hitCallback = null)
    {
        // StartCoroutine("AttackCoroutine", _targetPos);
        // StopCoroutine("AttackCoroutine");    // 무조건 다 세우는것

        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        attackCoroutine = StartCoroutine(AttackCoroutine(_targetPos, _hitCallback));
    }


    private IEnumerator AttackCoroutine(Vector3 _targetPos, Explosion.HitCallback _hitCallback)
    {
        // 회전 Yow, 선형보간, 각도 구하기(세타)
        // 아크탄젠트를 이용하면 각도를 구할 수 있다. x,z
        //Vector3 towerPos = transform.position;
        //towerPos.y = 0f; // 각도 계산 시 y값(높낮이)은 제외
        //Vector3 targetPos = _targetPos;
        //targetPos.y = 0f;
        //Vector3 dirToTarget = (targetPos - towerPos).normalized;    // 타겟 방향벡터 구하기
        //float theta = Mathf.Atan2(dirToTarget.z, dirToTarget.x) * Mathf.Rad2Deg;

        float myAngle = AngleToTarget(transform.position, transform.forward);   // 타워 각도
        float targetAngle = AngleToTarget(transform.position, _targetPos);      // 타겟 각도

        float deltaAngle = Mathf.DeltaAngle(myAngle, targetAngle);              // s값 보정

        float t = 0f;
        float s = Mathf.Abs(myAngle - targetAngle) / 360f; // 절대값 구하기
        
        // 제일 많이 사용하는 방식
        // Quaternion from = transform.rotation;
        // Quaternion to = Quaternion.LookRotation(_targetPos - transform.position);

        // Vector3.MoveTowards      // 파라미터에 속도를 넣어서 그 속도로 등속운동을 한다.
        // Vector3.SmoothDamp       // 갈수록 천천히...
        // Vector3.Slerp            // 곡선으로 선형보간을 하는 방식. 구형 보간, 3인칭시점 카메라 구현에 사용
        // Vector3.LerpUnclamped    // 0~1의 제한을 두지 않는다.


        while (t < 1f)
        {
            // float angle = Mathf.Lerp(myAngle, targetAngle, t);
            float angle = Mathf.LerpAngle(myAngle, myAngle + deltaAngle, t);    // Mathf.LerpAngle 사용
            RotateYaw(angle);

            // float angle = Mathf.LerpAngle(myAngle, targetAngle, t);
            // RotateYaw(angle);

            // 제일 많이 사용하는 방식
            // transform.rotation = Quaternion.Lerp(from, to, t);

            // t += Time.deltaTime / s;     // s값도 보정을 해줘야 한다.
            // t += (Time.deltaTime * rotSpeed) / s;
            t += Time.deltaTime * rotSpeed;
            
            yield return null;
        }

        RotateYaw(targetAngle);


        // Vector3.Lerp
        // Quaternion.Lerp
        // Color.Lerp
        // Mathf.Lerp


        // 공격
        /*
        GameObject missile = Instantiate(
            missilePrefab,
            missileSpawnPoint.GetSpawnPoint(),
            missileSpawnPoint.GetRotation()
            );    // 미사일 객체 만들기
        missile.GetComponent<Missile>().Init(_targetPos);
        */

        Missile missile = GetUsableMissile();
        if (missile)
        {
            missile.Init(
                missileSpawnPoint.GetSpawnPoint(),
                missileSpawnPoint.GetRotation(),
                _targetPos,
                missileStateCallback,
                _hitCallback
                );
        }

    }

    // 정적함수는 이미 메모리에 올라가 있다. 객체를 생성하지 않고 바로 사용할 수 있다.
    // 정적 맴버변수
    public static float AngleToTarget(Vector3 _oriPos, Vector3 _targetPos)
    {
        Vector3 oriPos = _oriPos;
        oriPos.y = 0f; // 각도 계산 시 y값(높낮이)은 제외
        Vector3 targetPos = _targetPos;
        targetPos.y = 0f;
        Vector3 dirToTarget = (targetPos - oriPos).normalized;    // 타겟 방향벡터 구하기
        float theta = Mathf.Atan2(dirToTarget.z, dirToTarget.x) * Mathf.Rad2Deg;

        return theta;
    }


    private void PickingSample()
    {
        // InputMouse im = new InputMouse();
        Vector3 point = Vector3.zero;
        if (InputMouse.Instance.Picking("Stage", ref point))
        {
            float theta = AngleToTarget(transform.position, point);
            Debug.DrawLine(transform.position, point, Color.red);
            transform.rotation = Quaternion.Euler(0f, -theta + 90f, 0f);    // 유니티 각도와 Atan2각도의 기준점이 달라서 그런것임

            //Vector3 forward = transform.forward;
            //forward.y = 0f;
            //forward.Normalize();
            //float myTheta = Mathf.Atan2(forward.z, forward.x) * Mathf.Rad2Deg;

            // float angle = theta - myTheta;

            // transform.Rotate(0f, angle, 0f); // 상대회전 시 각도만큼 계속 돈다.
            // transform.Rotate(transform.up, angle);

            //transform.rotation = Quaternion.LookRotation(dirToTarget);    // 다른 방법
        }
    }

    private void RotateYaw(float _rotY)
    {
        transform.rotation = Quaternion.Euler(0f, -_rotY + 90f, 0f);
    }


    private Missile GetUsableMissile()
    {
        foreach (Missile missile in missileList)
        {
            if (!missile.gameObject.activeSelf) return missile; // 있으면 missile 반환
        }
        return null;    // 없으면 null 반환
    }

    public int GetHp()
    {
        return curHp;
    }

    public int Damage(int _dmg = 1)
    {
        curHp -= _dmg;
        Debug.Log("Tower HP: " + curHp);
        return curHp;
    }

    public int GetMissileCount()
    {
        return maxMissileCount;
    }

}
