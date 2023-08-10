using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Vector3 targetPos = Vector3.zero;
    private bool isInit = false;
    private Vector3 moveDir = Vector3.zero;
    private float speed = 10f;


    public void Init(Vector3 _targetPos)
    {
        targetPos = _targetPos;
        targetPos.y = transform.position.y; 
        moveDir = (targetPos - transform.position).normalized;
        isInit = true;
    }

    private void Update()
    {
        if (isInit == false) return;

        // transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.position = transform.position + (moveDir * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            Destroy(gameObject);
    }

}