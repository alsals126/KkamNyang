using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoginManager : MonoBehaviour
{

    public GameObject errP;

    public InputField NameField;
    public InputField P_numberField;
    public Dropdown Select_m;

    public static string name;
    public static string phone;
    public static string Major;

    public void LoginBtn()
    {
        StartCoroutine(LoginGo());
    }

    IEnumerator LoginGo()
    {
        name = NameField.text.ToString();
        phone = P_numberField.text.ToString();
        string major = "";
        if (Select_m.value == 0)
            major = "뉴미디어소프트웨어과";
        else if (Select_m.value == 1)
            major = "뉴미디어웹솔루션과";
        else
            major = "뉴미디어디자인과";

        Major = major;

        if (P_numberField.text.Length == 11 && NameField.text.Length != 0)
        {

            WWWForm form = new WWWForm();
            form.AddField("Input_nickname", NameField.text);
            form.AddField("Input_PNum", P_numberField.text);
            form.AddField("Input_major",major);

            using (UnityWebRequest www = UnityWebRequest.Post("http://itshow.dothome.co.kr/CreateAcc_3.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    UnityEngine.Debug.Log(www.error);
                    loginCorrect();
                }
                else
                {
                    UnityEngine.Debug.Log(www.downloadHandler.text);
                    SceneManager.LoadScene("SelectScene");
                }
            }
            yield return null;
        }
        else
        {
            loginCorrect();
        }
        
    }
    void loginCorrect()
    {
        errP.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            errP.SetActive(false);
        }
    }
}
