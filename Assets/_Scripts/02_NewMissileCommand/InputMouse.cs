using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton Pattern 싱글턴 패턴
// 객체는 하나만 존재한다. 어디서든 접근하고 사용할 수 있다.
// 전역변수와 비슷하다.
// 객체지향 개념에 맞지않은 패턴이다.
// 싱글턴이 동작을 할려면 전제가 하나 있어야 한다.
// 객체 생성이 하나만 되어야 한다. 객체 생성이 추가로 안되어야 한다.


public class InputMouse : MonoBehaviour
{
    private static InputMouse instance = null;

    public static InputMouse Instance   // 외부에서 접근하는 통로
    {
        get
        {
            if (instance == null)       // instance 할당이 안되어 있다면...
            {
                GameObject go = new GameObject("[S] Input Mouse");
                instance = go.AddComponent<InputMouse>();
            }
            return instance;            // instance 할당이 되어있으면 바로 반환
        }
    }

    private InputMouse() { }

    public bool Picking(string _tag, ref Vector3 _point)
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag(_tag))
            {
                _point = hit.point;
                return true;
            }
        }

        return false;
    }
}
