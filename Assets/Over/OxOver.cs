using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Diagnostics;

public class OxOver : MonoBehaviour
{
    public GameObject scoreText;
    string nickname=LoginManager.name;
    string phone=LoginManager.phone;
    int score;
    

    void Start()
    {
        score = OXGame.scoreOX;
        StartCoroutine(GetScore());
        this.scoreText.GetComponent<Text>().text = score + "점";
        Sound.aud.Play();
    }

    IEnumerator GetScore()
    {
        WWWForm form = new WWWForm();
        form.AddField("Input_nickname", nickname);
        form.AddField("Input_PNum", phone);
        form.AddField("OXScore", this.score);

        UnityEngine.Debug.Log(nickname);
        using (UnityWebRequest www = UnityWebRequest.Post("http://itshow.dothome.co.kr/GetOXQuizScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                UnityEngine.Debug.Log(www.error);
            }
            else
            {
                UnityEngine.Debug.Log(www.downloadHandler.text);
            }
        }
        yield return null;
    }
}
