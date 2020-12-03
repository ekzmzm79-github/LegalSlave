using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;
using System.IO;

public struct StrSet
{
    public string str;
    public int num;

    public StrSet(string str, int num)
    {
        this.str = str;
        this.num = num;
    }

}

public class EventChanger : MonoBehaviour {

    public GameController gameController;
    public GameObject Character;
    public GameObject TopBox;
    public GameObject BottomBox;
    public GameObject[] Buttons = new GameObject[3];
    public GameObject StatusBox;
    public GameObject Gold;
    public TextAsset jsonData;

    public int TotalEventMany = 20;
    int DayEventMany = 5;
    int EventIndex, SelectIndex;
    int JsonIndex;
    string JsonStr, EventKinds;
    
    

    bool EventChangeTrigger; //임시


    // Use this for initialization
    void Start ()
    {

        EventChangeTrigger = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        

        if (EventChangeTrigger)
        {
            EventChangeTrigger = false;

            JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

            SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);



            if (NightEventClass.NightEventTrigger)
            {

                if (save.EventIndex == 5)
                {
                    //야근끝났으니 정산창으로

                    gameController.SendMessage("DayEnd");

                    return;
                }

                EventKinds = "Night";
                JsonIndex = Random.Range(0, 6); //야근 이벤트 스크립트 범위

            }
            else
            {
                if (save.Day <= 10)//10일 이하의 이벤트
                {
                    
                    EventKinds = "EventFirst";
                }
                else if(save.Day<=20)//10일이상, 20일 이하의 이벤트
                {
                    EventKinds = "EventSecond";
                }
                else//21일 이상의 이벤트
                {
                    EventKinds = "EventLast";
                }
                

                if (save.EventIndex == 4)
                {
                    //야근을 할지 말지를 묻는 씬으로

                    SceneManager.LoadScene("AskAfterEvent");
                    Debug.Log("셀렉트 인덱스가 데이 이벤트 매니 -1" + save.EventIndex);
                    return;
                }

                if (save.EventIndex == 1)
                {
                    EventKinds = "Lunch";
                    JsonIndex = Random.Range(0, 1); //점심 이벤트 스크립트 범위
                }
                else
                {
                    //JsonIndex 어떤 이벤트를 불러올지 결정하는 인덱스
                    JsonIndex = RandomBoxClass.RandomBox[save.EventIndex];
                }

                string PutData = JsonMapper.ToJson(save);

                File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);

                if (JsonIndex >= 1000) //미니 게임
                {

                    if (JsonIndex == 1000)
                    {
                        SceneManager.LoadScene("Before_Coffee");

                        return;
                    }
                    else if (JsonIndex == 1001)
                    {
                        SceneManager.LoadScene("Before_Document");

                        return;
                    }
                    else if (JsonIndex == 1002)
                    {
                        SceneManager.LoadScene("Before_Karaoke");

                        return;
                    }

                }
            }


            JsonData getData = JsonMapper.ToObject(jsonData.text);

            //캐릭터 이름에 따라서 캐릭터 체인지
            JsonStr = getData[EventKinds][JsonIndex]["Character"].ToString();
            Character.SendMessage("SetCharacter", JsonStr);

            //이벤트 시작 질문 세팅
            JsonStr = getData[EventKinds][JsonIndex]["QuestionString"].ToString();
            TopBox.SendMessage("SetQuestion", JsonStr);

            for (int i = 0; i <= 2; i++)
            {
                //버튼에 적힐 문구 세팅
                JsonStr = getData[EventKinds][JsonIndex]["Select" + i.ToString()].ToString();
                Buttons[i].SendMessage("SetSelect", new StrSet(JsonStr, i));

                //버튼을 클릭했을 때, 출력되는 대사 세팅
                JsonStr = getData[EventKinds][JsonIndex]["SelectString" + i.ToString()].ToString();
                BottomBox.SendMessage("SetSelectString", new StrSet(JsonStr, i));

                //각 버튼을 선택했을 때의 결과 대사 세팅
                JsonStr = getData[EventKinds][JsonIndex]["ResultString" + i.ToString()].ToString();
                TopBox.SendMessage("SetResult", new StrSet(JsonStr, i));

                //각 버튼을 선택했을 때의 파라미터 값 세팅
                JsonStr = getData[EventKinds][JsonIndex]["Heart" + i.ToString()].ToString();
                StatusBox.SendMessage("SetHeart", new StrSet(JsonStr, i));

                JsonStr = getData[EventKinds][JsonIndex]["Colleague" + i.ToString()].ToString();
                StatusBox.SendMessage("SetColleague", new StrSet(JsonStr, i));

                JsonStr = getData[EventKinds][JsonIndex]["WorkAB" + i.ToString()].ToString();
                StatusBox.SendMessage("SetWorkAB", new StrSet(JsonStr, i));

                JsonStr = getData[EventKinds][JsonIndex]["Stress" + i.ToString()].ToString();
                StatusBox.SendMessage("SetStress", new StrSet(JsonStr, i));

                //골드 세팅
                JsonStr = getData[EventKinds][JsonIndex]["Gold" + i.ToString()].ToString();
                Gold.SendMessage("SetGold", new StrSet(JsonStr, i));
                //


            }

            gameController.SendMessage("EventStartTriggerOn");

        }

        

    }

    public void EventChangeTriggerOn()
    {
        EventChangeTrigger = true;
    }

    public void CreateEvent(int day)
    {
        EventIndex = 0;

        for (int i = EventIndex; i < DayEventMany; i++)
        {
            RandomBoxClass.RandomBox[i] = Random.Range(0, TotalEventMany);

            if (i != 0)
            {
                for (int j = EventIndex; j < i; j++)
                {
                    if(RandomBoxClass.RandomBox[i]== RandomBoxClass.RandomBox[j])
                    {
                        i--;
                        break;
                    }
                }
            }

        }

        //미니게임 이벤트 추가
        if (day % 3 == 0)
        {
            int temp= Random.Range(0, 2);

            //미니게임은 3일을 주기로 랜덤 박스 0, 2, 3 중에서 하나만 나온다.
            if(temp==0)
            {

                RandomBoxClass.RandomBox[0] = Random.Range(1000, 1003);

            }
            else if(temp==1)
            {

                RandomBoxClass.RandomBox[2] = Random.Range(1000, 1003);
            }
            else
            {

                RandomBoxClass.RandomBox[3] = Random.Range(1000, 1003);
            }
        }


        //랜덤 박스 생성 후 세이브 (하루단위)

        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        for (int i = 0; i < 5; i++)
        {
            save.RandomBox[i] = RandomBoxClass.RandomBox[i];
        }

        string PutData = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);

    }

    public void LoadEvent(int eventindex)
    {
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        for (int i = 0; i < 5; i++)
        {
            RandomBoxClass.RandomBox[i] = save.RandomBox[i];
        }

        save.EventIndex = eventindex;

        string PutData = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);

        //이벤트를 로드한 뒤에는 바로 이벤트 체인지 트리거를 On
        EventChangeTriggerOn();


    }


}


public static class RandomBoxClass
{
    public static int[] RandomBox = new int[5];
}
 
