using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DayBlinder : MonoBehaviour {

    public static int Highest_Rayer = 10;
    public static int Lowest_Rayer = -1;

    public GameController gameController;
    public GameObject Calendar;
    public GameObject DayEventInfo;
    public GameObject EventChanger;

    public Text DayText;
    public Image Panel;
    public Canvas Blinder;
    float fades = 1.0f;
    float time = 0;

    int Day;
    bool Trigger1, Trigger2, TextTrigger, noEventDay;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {

        //1. 게임 시작과 동시에 텍스트 트리거 켜짐--> 블라인더 + 날짜 표시 
        //2. 클릭 한 번 더하면 페이드 아웃 효과 시작
        //3. 특정 날짜를 판단해서 DayEventInfo와 EventChanger를 적절히 실행시킴.

        if (TextTrigger)
        {
            Trigger1 = true;
            TextTrigger = false;
            DayText.text = "Day" + Day.ToString();
        }

        if (Trigger1 && Input.GetButtonDown("Fire1"))
        {
            Trigger1 = false;
            Trigger2 = true;
            noEventDay = false;

            //특정 날짜를 감지해서 데이 이벤트 이미지를 준비한다. 
            if (Day == 1)
            {
                DayEventInfo.SendMessage("TriggerOn", Day);

            }
            else if (Day == 11)
            {
                DayEventInfo.SendMessage("TriggerOn", Day);
            }
            else if(Day == 21)
            {
                DayEventInfo.SendMessage("TriggerOn", Day);
            }
            else //특정 날짜가 아니다 == noEventDay
            {
                noEventDay = true;
            }

        }


        if (Trigger2)
        {

            if (fades<=0.9)
            {
                //텍스트는 미리 사라진다
                DayText.text = "";
            }

            if (fades <= 0)
            {
                //배경음악을 켠다
                gameController.SendMessage("BGMstart");

                //

                time = 0;
                Blinder.sortingOrder = Lowest_Rayer;
                Trigger2 = false;

                //페이드 아웃이 완료되었다는 신호를 보낸다.
                DayEventInfo.SendMessage("Trigger2On");

                if(noEventDay)
                {
                    //만약 이벤트 날짜가 아니라면 바로 이벤트 체인지로 신호를 보낸다.
                    
                    EventChanger.SendMessage("EventChangeTriggerOn");
                }

                

            }

            time += Time.deltaTime;

            if (fades > 0 && time >= 0.1f)
            {
               
                fades -= 0.05f;
                Panel.color = new Color(0, 0, 0, fades);
                time = 0;
            }
        }

        
    }

    public void BlinderOff()
    {
        Blinder.sortingOrder = Lowest_Rayer;
    }

    public void TriggerOn(int day)
    {
        Day = day;
        Blinder.sortingOrder = Highest_Rayer;

        TextTrigger = true;
    }

    public bool IsNoEventDay()
    {

        return noEventDay;
    }

}
