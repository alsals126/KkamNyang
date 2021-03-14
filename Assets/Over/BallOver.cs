using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BallOver : MonoBehaviour
{
    public GameObject scoreText;
    float btime;
    string btime_str;
    public string nickname = LoginManager.name;
    public string phoneNum = LoginManager.phone;

    void Start()
    {
        btime = BTimer.LimitTimeB;
        btime_str = btime.ToString("F2");
        this.scoreText.GetComponent<Text>().text = btime_str + "  초";
        StartCoroutine(GetScore());
    }


    IEnumerator GetScore()
    {
        WWWForm form = new WWWForm();
        form.AddField("Input_nickname", this.nickname);
        form.AddField("Input_PNum", this.phoneNum);
        form.AddField("PiguScore", btime_str);

        using (UnityWebRequest www = UnityWebRequest.Post("http://itshow.dothome.co.kr/GetPiguScore.php", form))
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
