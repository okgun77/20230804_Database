using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISignUp : MonoBehaviour
{
    public enum EInputType { ID, PW }
    public delegate void OnClickDelegate(string _id, string _pw);

    private Dictionary<string, TMP_InputField> dicIF = new Dictionary<string, TMP_InputField>();

    private string id = null;
    private OnClickDelegate onClickCallback = null;

    // ������Ƽ
    public OnClickDelegate OnClickCallback
    {
        set { onClickCallback = value; }
    }


    private void Awake()
    {
        TMP_InputField[] inputFields = GetComponentsInChildren<TMP_InputField>();
        string ifText = inputFields[(int)EInputType.ID].text;

        dicIF.Add("ID", inputFields[0]);
        dicIF.Add("PW", inputFields[1]);
        ifText = dicIF["ID"].text;
        // Ű���� ���ڿ��� ��� �ӵ��� ������ ���ɼ��� ����.

        Button btn = GetComponentInChildren<Button>();
        // ���ٽ�
        btn.onClick.AddListener(
            () =>
            {
                //if (onClickCallback != null)
                //    onClickCallback();
                // �κ�ũinvoke ��Ƽ�����忡 �����ϱ� ���ؼ� �����.
                onClickCallback?.Invoke(id, dicIF["PW"].text);
            }
            );
    }

    public void OnChangeIdText(string _id)
    {
        id = _id;
        // Debug.Log(id);
    }
}
