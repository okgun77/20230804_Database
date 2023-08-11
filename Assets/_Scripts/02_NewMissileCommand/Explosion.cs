using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public delegate void FinishCallback();
    public delegate void HitCallback(List<Enemy> _hitList);


    private readonly float maxScale;
    private const float MAX_SCALE = 5f;

    [SerializeField][Range(1f, 10f)] private float speed = 5f;

    private bool isActivate = false;


    private Explosion()
    {
        maxScale = 5f;
    }
    // default parameter 디폴트 매개변수
    // 제일 오른쪽 부터 지정해야 함. 중간에 넣으면 안됨.

    public void DefaultParam(int _a, int _b = 10, int _c = 10) { }
    public void DefaultParam(int _a) { }

    public void Activate(FinishCallback _finishCallback = null, HitCallback _hitCallback = null) 
    {
        if (isActivate == true) return;
        StartCoroutine(ActivateCoroutine(_finishCallback, _hitCallback));
    }

    private IEnumerator ActivateCoroutine(FinishCallback _finishCallback, HitCallback _hitCallback)
    {
        transform.localScale = Vector3.zero;
        float t = 0f;
        Vector3 from = transform.localScale;
        Vector3 to = Vector3.one * maxScale;
        Vector3 velocity = Vector3.zero;


        while (t < 1f)
        {
            // transform.localScale = Vector3.SmoothDamp(from, to, ref velocity, t);
            transform.localScale = Vector3.Lerp(transform.localScale, to, t);
            t += Time.deltaTime * speed;
            yield return null;
        }

        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            MAX_SCALE * 0.5f,
            transform.forward,
            0f
            );

        if (hits.Length > 0)
        {
            List<Enemy> hitList = new List<Enemy>();
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    // Debug.Log(hit.transform.name);
                    hitList.Add(hit.transform.GetComponent<Enemy>());
                }
            }
            _hitCallback?.Invoke(hitList);
        }

        _finishCallback?.Invoke();
        transform.localScale = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, MAX_SCALE * 0.5f);
    }
}
