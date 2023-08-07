using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class SignUp : MonoBehaviour
{
    string singupUri = "http://127.0.0.1/signup.php";

    private void Start()
    {
        StartCoroutine(SignUpCoroutine("test1", "test1"));
    }

    private IEnumerator SignUpCoroutine(string _username, string _password)
    {
        WWWForm form = new WWWForm();
        form.AddField("signupUser", _username);
        form.AddField("signupPass", _password);

        using (UnityWebRequest www = UnityWebRequest.Post(singupUri, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }

    }
}
