using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;   // Network ��Ű��

public class Login : MonoBehaviour
{
    string loginUri = "http://127.0.0.1/login.php";

    private void Start()
    {
        StartCoroutine(LoginCoroutine("640k", "00001111"));

        // Debug.Log(System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
        // Debug.Log(System.DateTime.Now.ToString("yyyy-mm-dd-hh-mm-ss"));
    }

    private IEnumerator LoginCoroutine(string _username, string _password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", _username);  // key, value,   Dictionary ����
        form.AddField("loginPass", _password);

        // using �Լ�, using�Լ��ȿ� ������ ������ �Լ��� ����� gc�� �������ش�.
        // ��Ű�� �ٿ�ε� ���� �� ����ϴ� ���
        using (UnityWebRequest www = UnityWebRequest.Post(loginUri, form))
        {
            yield return www.SendWebRequest();  // POST ������ ������ ���� ���

            // if (www.isNetworkError || www.isHttpError)
            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            { 
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);    // PHP�� ��ȸ�� �� ����� .text�� ���´�.
            }

        }
    }
}
