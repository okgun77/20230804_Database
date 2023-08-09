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

    // 프로퍼티
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
        // 키값이 문자열일 경우 속도가 느려질 가능성이 있음.

        Button btn = GetComponentInChildren<Button>();
        // 람다식
        btn.onClick.AddListener(
            () =>
            {
                //if (onClickCallback != null)
                //    onClickCallback();
                // 인보크invoke 멀티쓰레드에 안전하기 위해서 사용함.
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
