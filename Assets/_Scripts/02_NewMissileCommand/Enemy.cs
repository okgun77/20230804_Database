using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Alt + Enter 하면 자동으로 만들어 준다.
public class Enemy : MonoBehaviour, IPoolingObject
{
    private float moveSpeed = 0f;
    private GameObject target = null;


    public void Init()
    {
        moveSpeed = Random.Range(1f, 3f);
        gameObject.SetActive(true);
    }

    public void Release()
    {
        gameObject.SetActive(false);
    }

    public bool IsAlive()
    {
        return gameObject.activeSelf;
    }

    public void SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (!target) return;

        Vector3 myPos = transform.position;
        myPos.y = 0f;
        Vector3 targetPos = target.transform.position;
        targetPos.y = 0f;
        Vector3 dir = (targetPos - myPos).normalized;

        transform.rotation = Quaternion.LookRotation(dir);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if ((targetPos - myPos).magnitude < 1f)
        {
            // 리셋
            Release();
        }

    }
}
