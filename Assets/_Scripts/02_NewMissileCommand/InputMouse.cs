using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton Pattern �̱��� ����
// ��ü�� �ϳ��� �����Ѵ�. ��𼭵� �����ϰ� ����� �� �ִ�.
// ���������� ����ϴ�.
// ��ü���� ���信 �������� �����̴�.
// �̱����� ������ �ҷ��� ������ �ϳ� �־�� �Ѵ�.
// ��ü ������ �ϳ��� �Ǿ�� �Ѵ�. ��ü ������ �߰��� �ȵǾ�� �Ѵ�.


public class InputMouse : MonoBehaviour
{
    private static InputMouse instance = null;

    public static InputMouse Instance   // �ܺο��� �����ϴ� ���
    {
        get
        {
            if (instance == null)       // instance �Ҵ��� �ȵǾ� �ִٸ�...
            {
                GameObject go = new GameObject("[S] Input Mouse");
                instance = go.AddComponent<InputMouse>();
            }
            return instance;            // instance �Ҵ��� �Ǿ������� �ٷ� ��ȯ
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
