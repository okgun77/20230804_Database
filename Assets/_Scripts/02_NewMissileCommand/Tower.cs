using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private void Update()
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

    private IEnumerator AttackCoroutine(Vector3 _targetPos)
    {
        // ȸ�� Yow, ��������, ���� ���ϱ�(��Ÿ)
        // ��ũź��Ʈ�� �̿��ϸ� ������ ���� �� �ִ�. x,z
        Vector3 towerPos = transform.position;
        towerPos.y = 0f; // ���� ��� �� y��(������)�� ����
        Vector3 targetPos = _targetPos;
        targetPos.y = 0f;
        Vector3 dirToTarget = (targetPos - towerPos).normalized;    // Ÿ�� ���⺤�� ���ϱ�
        float theta = Mathf.Atan2(dirToTarget.z, dirToTarget.x) * Mathf.Rad2Deg;

        yield return null;


        // ����

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

    
}
