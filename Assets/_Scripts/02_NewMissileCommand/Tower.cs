using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    // Liner Interpolation선형보간

    [SerializeField][Range(1f, 20f)] private float rotSpeed = 10f;   // 타워 회전속도
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

        float deltaAngle = Mathf.DeltaAngle(myAngle, targetAngle);  

        float t = 0f;
        float s = Mathf.Abs(myAngle - targetAngle) / 360f; // 절대값 구하기

        while (t < 1f)
        {
            // float angle = Mathf.Lerp(myAngle, targetAngle, t);
            float angle = Mathf.LerpAngle(myAngle, myAngle + deltaAngle, t);    // Mathf.LerpAngle 사용
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


        // 공격
        GameObject missile = Instantiate(
            missilePrefab,
            missileSpawnPoint.GetSpawnPoint(),
            missileSpawnPoint.GetRotation()
            );    // 미사일 객체 만들기
        missile.GetComponent<Missile>().Init(_targetPos);

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


}
