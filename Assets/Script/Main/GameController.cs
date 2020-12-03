using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class GameController : MonoBehaviour {

    public EventChanger eventChanger;
    public DataLoader dataLoader;
    //public GameObject TopBox;
    public DayBlinder dayBlinder;
    public Button[] Buttons = new Button[3];
    public Character character;
    public AudioSource BGM;


    public float delay_time;

    bool EventStartTrigger, EventChangeTrigger, delay;

    static int GameController_Day;

    enum State
    {
        Ready,
        Play,
        GameOver,
        DayEnd
    }

    State state;

    // Use this for initialization
    void Start ()
    {
        Screen.SetResolution(600, 1024, true);

        Ready();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuitClass.GameQuit();
        }


		switch(state)
        {
            case State.Ready:
                Debug.Log("현재 레디 상태입니다.");
                if (GameLoadClass.GameLoadTrigger)//게임로드
                {
                    Debug.Log("1번");
                    GameLoad();
                    BGMstart();
                }
                else if (NightEventClass.NightEventTrigger)//야근 이벤트로 인한 재진입
                {
                    Debug.Log("2번");
                    NightEvent();
                    BGMstart();
                }
                else//로드도 아니고 야근도 아니다.
                {   //초기화 작업
                    Debug.Log("초기화를 실행할께요");
                    Initialized();
                    GameStart();
                }
                break;
            case State.Play:

                //
                string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

                SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

                

                if (save.Day >= 31)
                {
                    if(save.Hidden[0] && save.Hidden[1])
                        EndImageClass.SelectEnd = 10;
                    else
                        EndImageClass.SelectEnd = 9;

                    GameOver();
                }
                else if (save.Gold < 0)
                {
                    EndImageClass.SelectEnd = 8;
                    GameOver();
                }
                //



                if (EventStartTrigger)//이벤트 시작
                {

                    character.SendMessage("CharacterTriggerOn");
                    EventStartTrigger = false;

                    
                }
                else if(EventChangeTrigger)//이벤트 바꾸기
                {
                    EventChangeTrigger = false;

                    if (NightEventClass.NightEventTrigger)//야근이벤트인지 물어봄
                    {
                        save.EventIndex = 5;
                    }
                    else
                    {
                        save.EventIndex++;
                    }

                    


                    string PutData = JsonMapper.ToJson(save);//이벤트 인덱스에서 저장

                    File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);

                    StartCoroutine("EventChage");

                }

                break;
            case State.GameOver:

                break;
            case State.DayEnd:

                break;
        }
	}

    public void BGMstart()
    {
        BGM.Play();
    }

    public void Ready()
    {
        state = State.Ready;

        

        ButtonsOff();

    }

    public void GameStart()
    {
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        state = State.Play;

        dataLoader.SendMessage("LoadTriggerOn", save.Day);


    }

    public void FirstInitialized()
    {
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        save.EventIndex = 0;
        save.PrintCount = 0;

        string PutData = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);
    }


    public void Initialized()
    {
        Debug.Log("초기화진행중");
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        if(FirstStartClass.FirstStartTrigger)
        {
            save.Day = 1;
            save.Gold = 60000;
            for (int i = 0; i < 4; i++)
            {
                save.Items[i] = 0.0;
                save.Parameters[i] = 50.0;
            }
            for (int i = 0; i < 2; i++)
                save.Hidden[i] = false;
        }

        Debug.Log(save.Day +"/"+ save.Gold +"/"+ save.Parameters[0] +"/"+ save.Parameters[1]);
        save.EventIndex = 0;
        save.PrintCount = 0;

        string PutData = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);

    }

    public void NightEvent()
    {
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        state = State.Play;

        dataLoader.SendMessage("LoadTriggerOn", save.Day);
        dayBlinder.SendMessage("BlinderOff");

    }


    public void GameLoad()
    {
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        state = State.Play;

        dataLoader.SendMessage("LoadTriggerOn", save.Day);
        dayBlinder.SendMessage("BlinderOff");
    }



    public void GameOver()
    {
        state = State.GameOver;

        SceneManager.LoadScene("GameEnding");

    }

    public void LoadDay(int day)
    {
        GameController_Day = day;
    }

    public void DayEnd()
    {
        state = State.DayEnd;

        
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        if (save.EventIndex != 10) //이벤트 인덱스가 10? == 이미 정산창을 보고 로드한것이다.
        {
            
            if(save.Day%7==0)
            {
                GoldHistoryForm newHistory3 = new GoldHistoryForm();

                newHistory3.day = GameController_Day;
                newHistory3.gold = 60000;
                newHistory3.type = "주급";
                GoldHistoryList.GoldList.Add(newHistory3);

                save.Gold += 60000;
                save.PrintCount ++;

            }

            if(NightEventClass.NightEventTrigger)
            {
                GoldHistoryForm newHistory4 = new GoldHistoryForm();

                newHistory4.day = GameController_Day;
                newHistory4.gold = 1000;
                newHistory4.type = "야근수당";
                GoldHistoryList.GoldList.Add(newHistory4);

                save.Gold += 1000;
                save.PrintCount++;
            }


            /*
            GoldHistoryForm newHistory1 = new GoldHistoryForm();

            newHistory1.day = GameController_Day;
            newHistory1.gold = -7000;
            newHistory1.type = "식비";
            GoldHistoryList.GoldList.Add(newHistory1);

            save.Gold += -7000;
            */

            GoldHistoryForm newHistory2 = new GoldHistoryForm();

            newHistory2.day = GameController_Day;
            newHistory2.gold = -2400;
            newHistory2.type = "교통비";
            GoldHistoryList.GoldList.Add(newHistory2);


            save.Gold += -2400;

            save.PrintCount += 1;

            File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/GoldHistory.txt", JsonMapper.ToJson(GoldHistoryList.GoldList));


            
        }


        string PutData = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);


        SceneManager.LoadScene("DayResult");
    }

    public void EventStartTriggerOn()
    {
        EventStartTrigger = true;
    }

    public void EventChangeTriggerOn()
    {
        EventChangeTrigger = true;
    }


    public void ButtonsOn()
    {

        for (int i = 0; i < 3; i++)
        {
            Buttons[i].GetComponent<Button>().enabled = true;
            Buttons[i].GetComponent<ButtonSelectMain>().enabled = true;
        }


    }

    public void ButtonsOff()
    {
        

        for (int i = 0; i < 3; i++)
        {
            Buttons[i].GetComponent<Button>().enabled = false;
            Buttons[i].GetComponent<ButtonSelectMain>().enabled = false;
        }

    }



    IEnumerator EventChage()
    {
        ButtonsOff();

        yield return new WaitForSeconds(delay_time);


        eventChanger.SendMessage("EventChangeTriggerOn");
    }
}

public static class FirstStartClass
{
    public static bool FirstStartTrigger;
}


public static class GameLoadClass
{
    public static bool GameLoadTrigger;
}

public static class NightEventClass
{
    public static bool NightEventTrigger;
}
