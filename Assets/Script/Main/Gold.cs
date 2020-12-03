using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using LitJson;



public class Gold : MonoBehaviour {

    public GameController gameController;
    public Text GoldTetxt;
    int[] gold = new int[4];

    int incgold;
    int NowGold;
    int NowDay;
    int PrintCount;
    bool GoldTrigger;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(GoldTrigger)
        {
            GoldTrigger = false;

            StartCoroutine("IncGold", incgold);
        }
	}

    public void LoadGold(StrSet send)
    {
        NowDay = int.Parse(send.str);
        NowGold = send.num;

        GoldTetxt.text = NowGold.ToString();
    }



    public void SetGold(StrSet send)
    {
        gold[send.num] = int.Parse(send.str);
    }

    public void GoldTriggerOn()
    {
        GoldTrigger = true;
    }

    public void ChangeGold(StrSet send)//strset
    {
        if (gold[send.num] != 0)
        {

            GoldHistoryForm newHistory = new GoldHistoryForm();

            string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");
            SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

            

            //newHistory에 증감되는 골드와 어떤 이벤트로 인해서 변화되는지를 기록한다.
            newHistory.day = NowDay;
            newHistory.gold = gold[send.num];

            if (save.EventIndex == 1)
                newHistory.type = "식비";
            else
                newHistory.type = send.str;

            Debug.Log(newHistory.day);
            Debug.Log(newHistory.gold);
            Debug.Log(newHistory.type);

            GoldHistoryList.GoldList.Add(newHistory);

            File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/GoldHistory.txt", JsonMapper.ToJson(GoldHistoryList.GoldList));
            //


            //변화된 골드의 세이브
            
            save.Gold += gold[send.num];
            save.PrintCount += 1; //프린트 카운터를 증가시킨다.

            string PutData = JsonMapper.ToJson(save);

            File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);


            //화면에 표시되는 골드 수치 변경
            incgold = gold[send.num];
            GoldTrigger = true;

            if(save.Gold<0)
            {
                EndImageClass.SelectEnd = 8;
                gameController.SendMessage("GameOver");
            }


        }


    }



    IEnumerator IncGold(int incgold)
    {

        if (incgold < 0)
        {
            while (incgold < 0)
            {
                NowGold-=100;
                incgold+=100;
                GoldTetxt.text = NowGold.ToString();
                yield return new WaitForSeconds(0.0000001f);
            }

        }
        else
        {
            while (incgold > 0)
            {
                NowGold+=100;
                incgold-=100;
                GoldTetxt.text = NowGold.ToString();
                yield return new WaitForSeconds(0.0000001f);
            }

        }



    }

    public void LoadGoldHistory()
    {
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/GoldHistory.txt");

        if (GoldHistoryList.GoldList.Count != 0)
        {
            Debug.Log("ddddd");

            GoldHistoryList.GoldList = JsonMapper.ToObject<List<GoldHistoryForm>>(JsonStr);
        }
            

    }


}


public class GoldHistoryForm
{
    public int day { get; set; }
    public int gold { get; set; }
    public string type { get; set; }
}

public static class GoldHistoryList
{
    public static List<GoldHistoryForm> GoldList = new List<GoldHistoryForm>();


}
