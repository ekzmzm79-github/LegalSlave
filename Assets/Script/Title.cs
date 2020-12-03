using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class Title: MonoBehaviour {

    public Canvas NoData;
    public Canvas NeedLogin;


    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
    }

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuitClass.GameQuit();
        }

    }

    public void ToStart()
    {
        GoldHistoryList.GoldList.Clear();

        StreamWriter GoldHistoryReset = new StreamWriter(DataPathStringClass.DataPathString() + "/Save/GoldHistory.txt", false);
        GoldHistoryReset.WriteLine("");
        GoldHistoryReset.Close();

        FirstStartClass.FirstStartTrigger = true;

        SceneManager.LoadScene("BeforeTutorial");
    }

    public void ToContinue()
    {
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");
        
        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        if(save.Day==0)//로드할 데이터가 없다.
        {
            NoData.sortingOrder = 10;
            return;
        }

        GameLoadClass.GameLoadTrigger = true;

        if (save.EventIndex<4)
        {
            SceneManager.LoadScene("Main");
        }
        else if(save.EventIndex==4)
        {
            SceneManager.LoadScene("AskAfterEvent");
        }
        else if(save.EventIndex==5)
        {
            NightEventClass.NightEventTrigger = true;

            SceneManager.LoadScene("Main");
        }
        else if(save.EventIndex==6)
        {
            SceneManager.LoadScene("Shop");
        }
        else if(save.EventIndex==10)
        {
            SceneManager.LoadScene("DayResult");
        }

    }

    public void NodataClose()
    {
        NoData.sortingOrder = -1;
        NeedLogin.sortingOrder = -1;
    }


    public void ToEndList()
    {

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                SceneManager.LoadScene("EndingCollect");
            }
            else
            {
                //SceneManager.LoadScene("EndingCollect");
                NeedLogin.sortingOrder = 10;
            }
        });
    }

    public void ToSetting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void ToShowAchievementUI()
    {
        //로그인 되지 않은 상태라면 로그인 후 업적 표시 요청 할 것
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    //로그인되어잇으면
                    Social.ShowAchievementsUI();
                    return;
                }
                else
                {
                    //로그인실패
                    NeedLogin.sortingOrder = 10;
                    return;
                }
            });
        }
        Social.ShowAchievementsUI();
    }

    public void End()
    {
        GameQuitClass.GameQuit();
    }

    

}


public static class GameQuitClass
{
    public static void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}

public static class DataPathStringClass
{

    public static string DataPathString()
    {
#if UNITY_EDITOR
        string path = Application.dataPath;
        return path;
#elif UNITY_ANDROID
        
        string path = Application.persistentDataPath;

        if (!Directory.Exists(path + "/Save"))
        {
            Directory.CreateDirectory(path + "/Save");

            FileStream fs = new FileStream(Application.persistentDataPath + "/Save/SaveData.txt", FileMode.Create, FileAccess.ReadWrite);
            byte[] data = new byte[200];
            fs.Write(data, 0, (int)200);
            fs.Close();

            SaveData save = new SaveData();

            string PutData = JsonMapper.ToJson(save);

            File.WriteAllText(path + "/Save/SaveData.txt", PutData);

            FileStream fs2 = new FileStream(Application.persistentDataPath + "/Save/GoldHistory.txt", FileMode.Create, FileAccess.ReadWrite);
            byte[] data2 = new byte[200];
            fs2.Write(data2, 0, (int)200);
            fs2.Close();

        }


        return path;
#endif
    }




}