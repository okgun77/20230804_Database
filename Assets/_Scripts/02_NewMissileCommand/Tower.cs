using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    // Liner Interpolation��������

    [SerializeField][Range(1f, 10f)] private float rotSpeed = 3f;   // Ÿ�� ȸ���ӵ�
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
            go.SetActive(false);   // ���������� Find�� �ص� ��ã��. �츮�� ������� ������ �ؼ� �������.

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
        // StopCoroutine("AttackCoroutine");    // ������ �� ����°�

        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        attackCoroutine = StartCoroutine(AttackCoroutine(_targetPos, _hitCallback));
    }


    private IEnumerator AttackCoroutine(Vector3 _targetPos, Explosion.HitCallback _hitCallback)
    {
        // ȸ�� Yow, ��������, ���� ���ϱ�(��Ÿ)
        // ��ũź��Ʈ�� �̿��ϸ� ������ ���� �� �ִ�. x,z
        //Vector3 towerPos = transform.position;
        //towerPos.y = 0f; // ���� ��� �� y��(������)�� ����
        //Vector3 targetPos = _targetPos;
        //targetPos.y = 0f;
        //Vector3 dirToTarget = (targetPos - towerPos).normalized;    // Ÿ�� ���⺤�� ���ϱ�
        //float theta = Mathf.Atan2(dirToTarget.z, dirToTarget.x) * Mathf.Rad2Deg;

        float myAngle = AngleToTarget(transform.position, transform.forward);   // Ÿ�� ����
        float targetAngle = AngleToTarget(transform.position, _targetPos);      // Ÿ�� ����

        float deltaAngle = Mathf.DeltaAngle(myAngle, targetAngle);              // s�� ����

        float t = 0f;
        float s = Mathf.Abs(myAngle - targetAngle) / 360f; // ���밪 ���ϱ�
        
        // ���� ���� ����ϴ� ���
        // Quaternion from = transform.rotation;
        // Quaternion to = Quaternion.LookRotation(_targetPos - transform.position);

        // Vector3.MoveTowards      // �Ķ���Ϳ� �ӵ��� �־ �� �ӵ��� ��ӿ�� �Ѵ�.
        // Vector3.SmoothDamp       // ������ õõ��...
        // Vector3.Slerp            // ����� ���������� �ϴ� ���. ���� ����, 3��Ī���� ī�޶� ������ ���
        // Vector3.LerpUnclamped    // 0~1�� ������ ���� �ʴ´�.


        while (t < 1f)
        {
            // float angle = Mathf.Lerp(myAngle, targetAngle, t);
            float angle = Mathf.LerpAngle(myAngle, myAngle + deltaAngle, t);    // Mathf.LerpAngle ���
            RotateYaw(angle);

            // float angle = Mathf.LerpAngle(myAngle, targetAngle, t);
            // RotateYaw(angle);

            // ���� ���� ����ϴ� ���
            // transform.rotation = Quaternion.Lerp(from, to, t);

            // t += Time.deltaTime / s;     // s���� ������ ����� �Ѵ�.
            // t += (Time.deltaTime * rotSpeed) / s;
            t += Time.deltaTime * rotSpeed;
            
            yield return null;
        }

        RotateYaw(targetAngle);


        // Vector3.Lerp
        // Quaternion.Lerp
        // Color.Lerp
        // Mathf.Lerp


        // ����
        /*
        GameObject missile = Instantiate(
            missilePrefab,
            missileSpawnPoint.GetSpawnPoint(),
            missileSpawnPoint.GetRotation()
            );    // �̻��� ��ü �����
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

    // �����Լ��� �̹� �޸𸮿� �ö� �ִ�. ��ü�� �������� �ʰ� �ٷ� ����� �� �ִ�.
    // ���� �ɹ�����
    public static float AngleToTarget(Vector3 _oriPos, Vector3 _targetPos)
    {
        Vector3 oriPos = _oriPos;
        oriPos.y = 0f; // ���� ��� �� y��(������)�� ����
        Vector3 targetPos = _targetPos;
        targetPos.y = 0f;
        Vector3 dirToTarget = (targetPos - oriPos).normalized;    // Ÿ�� ���⺤�� ���ϱ�
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
            transform.rotation = Quaternion.Euler(0f, -theta + 90f, 0f);    // ����Ƽ ������ Atan2������ �������� �޶� �׷�����

            //Vector3 forward = transform.forward;
            //forward.y = 0f;
            //forward.Normalize();
            //float myTheta = Mathf.Atan2(forward.z, forward.x) * Mathf.Rad2Deg;

            // float angle = theta - myTheta;

            // transform.Rotate(0f, angle, 0f); // ���ȸ�� �� ������ŭ ��� ����.
            // transform.Rotate(transform.up, angle);

            //transform.rotation = Quaternion.LookRotation(dirToTarget);    // �ٸ� ���
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
            if (!missile.gameObject.activeSelf) return missile; // ������ missile ��ȯ
        }
        return null;    // ������ null ��ȯ
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
