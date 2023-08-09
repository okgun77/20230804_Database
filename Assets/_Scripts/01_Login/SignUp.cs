using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SignUp : MonoBehaviour
{
    [SerializeField] private UISignUp uiSignUp = null;

    private readonly string uri = "http://127.0.0.1/signup.php";


    private void Awake()
    {
        uiSignUp.OnClickCallback = OnClickSignUp;
    }

    public void OnClickSignUp(string _id, string _pw)
    {
        Debug.Log(_id + " / " + _pw);
        StartCoroutine(SignUpCoroutine(_id, _pw));
    }

    private IEnumerator SignUpCoroutine(string _id, string _pw)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _id);
        form.AddField("PW", _pw);

        // WWW www
        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log("Sign Up Success!!");
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
