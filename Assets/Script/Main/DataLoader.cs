using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;
using System.IO;

    public class DataLoader : MonoBehaviour {

    public GameController gameController;
    public GameObject Parameter;
    public GameObject Blinder;
    public GameObject Calendar;
    public GameObject Gold;
    public GameObject Item;
    public EventChanger eventChanger;
    public TextAsset jsonData;

    int Day;
    double[] parameter = new double[4];
    int gold;

    int JsonIndex, StrIndex;
    string JsonStr;
    string str = "";

    bool LoadTrigger;


    // Use this for initialization
    void Start ()
    { 

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (LoadTrigger)
        {

            LoadTrigger = false;

            JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

            SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

            if(GameLoadClass.GameLoadTrigger)//게임로드
            {

                gameController.SendMessage("LoadDay", save.Day);

                Calendar.SendMessage("LoadDay", save.Day);

                Item.SendMessage("LoadItems", save.Items);

                Gold.SendMessage("LoadGold", new StrSet(save.Day.ToString(), save.Gold));
                Gold.SendMessage("LoadGoldHistory");

                Parameter.SendMessage("LoadHeart", save.Parameters[0]);

                Parameter.SendMessage("LoadColleague", save.Parameters[1]);

                Parameter.SendMessage("LoadWorkAB", save.Parameters[2]);

                Parameter.SendMessage("LoadStress", save.Parameters[3]);

                eventChanger.SendMessage("LoadEvent", save.EventIndex);




            }
            else if(NightEventClass.NightEventTrigger)//야근 이벤트
            {
                gameController.SendMessage("LoadDay", save.Day);

                Calendar.SendMessage("LoadDay", save.Day);

                Item.SendMessage("LoadItems", save.Items);

                Gold.SendMessage("LoadGold", new StrSet(save.Day.ToString(), save.Gold));
                Gold.SendMessage("LoadGoldHistory");

                Parameter.SendMessage("LoadHeart", save.Parameters[0]);

                Parameter.SendMessage("LoadColleague", save.Parameters[1]);

                Parameter.SendMessage("LoadWorkAB", save.Parameters[2]);

                Parameter.SendMessage("LoadStress", save.Parameters[3]);

                eventChanger.SendMessage("EventChangeTriggerOn");

            }
            else//게임 로드가 아님
            {
                

                //데이터를 각 객체에 로드시킨다.
                gameController.SendMessage("LoadDay", save.Day);
                Blinder.SendMessage("TriggerOn", save.Day);
                Calendar.SendMessage("LoadDay", save.Day);

                Item.SendMessage("LoadItems", save.Items);

                Gold.SendMessage("LoadGold", new StrSet(save.Day.ToString(), save.Gold)); //골드에는 골드와 날짜를 동시에 전송

                Parameter.SendMessage("LoadHeart", save.Parameters[0]);

                Parameter.SendMessage("LoadColleague", save.Parameters[1]);

                Parameter.SendMessage("LoadWorkAB", save.Parameters[2]);

                Parameter.SendMessage("LoadStress", save.Parameters[3]);

                eventChanger.SendMessage("CreateEvent", save.Day);
            }

            
            if(Day >= 11 && Day < 21)
            {
                DayBuffInfo.Stress += 5;
                Debug.Log("웨이브2 버프 적용 호출");
            }
            else if(Day >= 21)
            {
                DayBuffInfo.Colleague += 5;
                DayBuffInfo.WorkAB -= 5;

                Debug.Log("웨이브3 버프 적용 호출");
            }
            
        }


    }


    public void LoadTriggerOn(int day)
    {
        LoadTrigger = true;
        Day = day;
    }
}



public class SaveData
{

    public int Day;
    public int Gold;
    public double[] Items = new double[4];
    public double[] Parameters = new double[4];
    public int EventIndex;
    public int[] Shoplist = new int[6];
    public int Shoprange;
    public string Character;
    public int[] RandomBox = new int[5];
    public int PrintCount;
    public bool[] Hidden = new bool[2];

    public SaveData(int Day, int Gold, double[] Parameters, int EventIndex, int[] Shoplist, int Shoprange, string Character, int[] RandomBox, int PrintCount, bool[] Hidden)
    {
        this.Day = Day;
        this.Gold = Gold;

        for (int i = 0; i < 4; i++)
        {
            this.Parameters[i] = Parameters[i];
        }

        this.EventIndex = EventIndex;
        this.Character = Character;


        for (int i = 0; i < 5; i++)
        {
            this.RandomBox[i] = RandomBox[i];
        }
        for (int i = 0; i < 6; i++)
        {
            this.Shoplist[i] = Shoplist[i];
        }

        this.Shoprange = Shoprange;
        this.PrintCount = PrintCount;

        for(int i=0; i<2; i++)
        {
            this.Hidden[i] = Hidden[i];
        }
    }

    public SaveData()
    {

    }

}