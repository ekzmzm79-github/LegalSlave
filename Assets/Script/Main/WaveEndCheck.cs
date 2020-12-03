using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class WaveEndCheck : MonoBehaviour {

    public DayResult dayResult;
    public AudioSource dayResultAudio;
    public Canvas WaveEndCheckCanvas;
    public Text Ment;
    public Text Money;
    public Text Warning;

    // Use this for initialization
    void Start ()
    {
        dayResult.enabled = false;
        Warning.enabled = false;

        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        if(save.Day==10)
        {
            WaveEndCheckCanvas.sortingOrder = 1;

            Ment.text = "여자친구가 기념 선물을 요구했다.";
            Money.text = "(5 만원)";
        }
        else if(save.Day==20)
        {
            WaveEndCheckCanvas.sortingOrder = 1;

            Ment.text = "부모님이 급전을 요구했다.";
            Money.text = "(5 만원)";
        }
        else
        {
            WaveEndCheckCanvas.sortingOrder = -1;

            Ment.text = "";
            Money.text = "";

            dayResult.enabled = true;
            dayResultAudio.Play();
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ClicktoYes()
    {
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        if(save.Gold<50000)
        {
            Warning.enabled = true;
            return;
        }


        GoldHistoryForm newHistory = new GoldHistoryForm();

        newHistory.day = save.Day;
        newHistory.gold = -50000;
        if (save.Day == 10)
            newHistory.type = "기념품";
        else if(save.Day == 20)
            newHistory.type = "급전";

        GoldHistoryList.GoldList.Add(newHistory);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/GoldHistory.txt", JsonMapper.ToJson(GoldHistoryList.GoldList));


        save.Gold += -50000;

        save.PrintCount += 1;

        if (save.Day == 10)
            save.Hidden[0] = true;
        else if (save.Day == 20)
            save.Hidden[1] = true;


        string PutData = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);


        WaveEndCheckCanvas.sortingOrder = -1;

        dayResult.enabled = true;
        dayResultAudio.Play();
    }

    public void ClicktoNo()
    {
        WaveEndCheckCanvas.sortingOrder = -1;

        dayResult.enabled = true;
        dayResultAudio.Play();
    }
}
