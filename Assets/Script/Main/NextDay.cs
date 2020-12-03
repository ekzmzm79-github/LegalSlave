using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;

public class NextDay : MonoBehaviour {

    public SaveData saveData = new SaveData();

    string JsonStr;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextDay()
    {
        

        JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        save.Day++;

        save.PrintCount = 0;
        save.EventIndex = 0;

        for (int i=0; i<4; i++)
        {
            if(ItemInfo.itemPeriod[i]>0)
            {
                save.Items[i] -= 0.1;
            }
            
        }


        


        if (GameLoadClass.GameLoadTrigger)
            GameLoadClass.GameLoadTrigger = false;

        if (NightEventClass.NightEventTrigger)
            NightEventClass.NightEventTrigger = false;

        if (FirstStartClass.FirstStartTrigger)
            FirstStartClass.FirstStartTrigger = false;

        //하루 버프 변수에 대한 초기화
        DayBuffInfo.Heart = 0;
        DayBuffInfo.Colleague = 0;
        DayBuffInfo.WorkAB = 0;
        DayBuffInfo.Stress = 0;

        GoldHistoryList.GoldList.Clear();

        StreamWriter GoldHistoryReset = new StreamWriter(DataPathStringClass.DataPathString() + "/Save/GoldHistory.txt", false);
        GoldHistoryReset.WriteLine("");
        GoldHistoryReset.Close();

        string PutData = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);

        SceneManager.LoadScene("Main");
    }
    
}
