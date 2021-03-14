using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TugOverScript : MonoBehaviour
{
    public GameObject scoreText;
    string nickname = LoginManager.name;
    string phone = LoginManager.phone;
    int round;

    void Start()
    {   
        scoreText.GetComponent<Text>().text = "Round " + HealthBar.Round.ToString();
        round = HealthBar.Round;
        HealthBar.Round = 0;
        StartCoroutine(GetScore());
    }

    IEnumerator GetScore()
    {
        WWWForm form = new WWWForm();
        form.AddField("Input_nickname", nickname);
        form.AddField("Input_PNum", phone);
        form.AddField("TugScore", round);

        UnityEngine.Debug.Log(nickname);
        using (UnityWebRequest www = UnityWebRequest.Post("http://itshow.dothome.co.kr/GetTugScore.php", form))
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
