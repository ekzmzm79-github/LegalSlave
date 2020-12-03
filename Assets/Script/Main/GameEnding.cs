using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class GameEnding : MonoBehaviour {

    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
    }

    public TextAsset jsonData;
    public Text EndText;
    string JsonStr;
    //파라미터 엔딩 8개 + 골드 엔딩 1개 + 일반 엔딩 1개+ 히든 엔딩 1개
    public Image EndImage;
    public Sprite[] EndImages = new Sprite[11];
    public AudioClip[] EndClips = new AudioClip[11];
    AudioSource EndAudio;

    int Lowest_Rayer = DayBlinder.Lowest_Rayer;
    public Image Panel;
    public Canvas Blinder;
    float fades = 1.0f;
    float time = 0;

    bool EndTrigger, BlinderTrigger;
    // Use this for initialization

    IEnumerator UpdateCorutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("IdPost", Social.localUser.id);
        form.AddField("EndingPost", "Ending" + EndImageClass.SelectEnd.ToString());
        WWW www = new WWW("http://dlwlgh301.cafe24.com/wp/UpdateEnding.php", form);
        yield return www;

        if (Social.localUser.id.Length != 0)
        {
            if (EndImageClass.SelectEnd == 9)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_3, 100f, null);
            }
            else if (EndImageClass.SelectEnd == 2)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_5, 100f, null);
            }
            else if (EndImageClass.SelectEnd == 7)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_4, 100f, null);
            }
        }
    }

    void Start ()
    {
        if (NightEventClass.NightEventTrigger)
            NightEventClass.NightEventTrigger = false;

        StartCoroutine(UpdateCorutine());
        BlinderTrigger = true;
        EndAudio = GetComponent<AudioSource>();

        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        save.Day = 0;
        save.EventIndex = 0;
        save.PrintCount = 0;
        for (int i = 0; i < 2; i++)
            save.Hidden[i] = false;

        
        
        EndImage.GetComponent<Image>().sprite = EndImages[EndImageClass.SelectEnd];

        string PutData = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(EndTrigger)
        {
            EndTrigger = false;

            
            EndAudio.clip = EndClips[EndImageClass.SelectEnd];
            EndAudio.Play();

            /*
            JsonData getData = JsonMapper.ToObject(jsonData.text);
            JsonStr = getData["EndScript"][EndImageClass.SelectEnd]["String"].ToString();

            EndText.text = JsonStr;
            */
        }
        else
        {
            if(BlinderTrigger)
            {
                if (fades <= 0)
                {
                    time = 0;
                    Blinder.sortingOrder = Lowest_Rayer;
                    EndTrigger = true;
                    BlinderTrigger = false;
                }

                time += Time.deltaTime;

                if (fades > 0 && time >= 0.1f)
                {
                    fades -= 0.1f;
                    Panel.color = new Color(0, 0, 0, fades);
                    time = 0;
                }
            }

            
        }




	}

    public void Restart()
    {
        

        SceneManager.LoadScene("Title");
    }

}

public static class EndImageClass
{
    public static int SelectEnd;

}