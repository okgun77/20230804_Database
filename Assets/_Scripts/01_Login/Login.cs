using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;   // Network 패키지

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
        form.AddField("loginUser", _username);  // key, value,   Dictionary 구조
        form.AddField("loginPass", _password);

        // using 함수, using함수안에 생성된 변수가 함수를 벗어나면 gc가 정리해준다.
        // 패키지 다운로드 받을 때 사용하는 방식
        using (UnityWebRequest www = UnityWebRequest.Post(loginUri, form))
        {
            yield return www.SendWebRequest();  // POST 보낸것 받을때 까지 대기

            // if (www.isNetworkError || www.isHttpError)
            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            { 
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);    // PHP가 조회를 한 결과가 .text로 들어온다.
            }

        }
    }
}
