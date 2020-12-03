using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using LitJson;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class LoginManager : MonoBehaviour
{
    int a = 5;
    [SerializeField]
    //private Text txtLog;
    // Use this for initialization
    private void Awake()
    {
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
    private void Start()
    {
        if (Social.localUser.authenticated)
        {
            //로그인 성공
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_2, 100f, null);
            StartCoroutine(Keyinsert());
            StartCoroutine(EndingLoad_1());
            Debug.Log(Social.localUser.userName);
            //txtLog.text += "id : " + Social.localUser.id + "\n";
        }
        else
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_2, 100f, null);
                    //로그인 성공
                    StartCoroutine(Keyinsert());
                    StartCoroutine(EndingLoad_1());
                    Debug.Log(Social.localUser.userName);
                    //txtLog.text += "id : " + Social.localUser.id + "\n";
                }
                else
                {

                    Debug.Log("로그인 실패");
                    //txtLog.text += "로그인 실패\n";
                }
            });
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Keyinsert()
    {
        WWWForm form = new WWWForm();
        form.AddField("IdPost", Social.localUser.id);
        WWW www = new WWW("http://dlwlgh301.cafe24.com/wp/InputEnding.php", form);
        yield return www;
    }

    IEnumerator EndingLoad_1()
    {
        WWWForm form2 = new WWWForm();
        form2.AddField("IdPost", Social.localUser.id);
        WWW www2 = new WWW("http://dlwlgh301.cafe24.com/wp/endingoutput.php", form2);
        yield return www2;

        string load = www2.text;
        LitJson.JsonData getDa = LitJson.JsonMapper.ToObject(load);

        bool endingg9 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending9"].ToString()));
        if (endingg9 == true)
        {
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_3, 100f, null);
        }

        bool endingg2 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending2"].ToString()));
        if(endingg2 == true)
        {
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_5, 100f, null);
        }

        bool endingg7 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending7"].ToString()));
        if(endingg7 == true)
        {
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_4, 100f, null);
        }
    }
}