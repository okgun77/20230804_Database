using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    // Liner Interpolation��������

    [SerializeField][Range(1f, 20f)] private float rotSpeed = 10f;   // Ÿ�� ȸ���ӵ�
    [SerializeField] private GameObject missilePrefab = null;

    private MissileSpawnPoint missileSpawnPoint = null;

    private void Awake()
    {
        missileSpawnPoint = GetComponentInChildren<MissileSpawnPoint>();
    }


    public void Attack(Vector3 _targetPos)
    {
        StartCoroutine(AttackCoroutine(_targetPos));
    }

    private IEnumerator AttackCoroutine(Vector3 _targetPos)
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

        float deltaAngle = Mathf.DeltaAngle(myAngle, targetAngle);  

        float t = 0f;
        float s = Mathf.Abs(myAngle - targetAngle) / 360f; // ���밪 ���ϱ�

        while (t < 1f)
        {
            // float angle = Mathf.Lerp(myAngle, targetAngle, t);
            float angle = Mathf.LerpAngle(myAngle, myAngle + deltaAngle, t);    // Mathf.LerpAngle ���
            RotateYaw(angle);


            // t += Time.deltaTime / s;
            t += Time.deltaTime * rotSpeed;
            yield return null;
        }

        RotateYaw(targetAngle);


        // Vector3.Lerp
        // Quaternion.Lerp
        // Color.Lerp
        // Mathf.Lerp


        // ����
        GameObject missile = Instantiate(
            missilePrefab,
            missileSpawnPoint.GetSpawnPoint(),
            missileSpawnPoint.GetRotation()
            );    // �̻��� ��ü �����
        missile.GetComponent<Missile>().Init(_targetPos);

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


}
