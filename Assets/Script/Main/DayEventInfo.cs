using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayEventInfo : MonoBehaviour {

    int Highest_Rayer = DayBlinder.Highest_Rayer;
    int Lowest_Rayer = DayBlinder.Lowest_Rayer;

    public GameController GameController;
    public DayBlinder dayBlinder;
    public GameObject EventChanger;
    public Button Close1;
    public Button Close2;

    public Sprite[] Info;
    public Image Panel;
    public Canvas Canvas;

    int DayInfoIndex;
    bool Trigger, Trigger2, Trigger3;

	// Use this for initialization
	void Start ()
    {
        
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        //트리거1은 판넬의 데이 이벤트 배경을 바꿔준다.
        //트리거2는 한번 더 클릭하면 데이 이벤트 판넬이 사라지고 이벤트 체인저 실행.

        

		if(Trigger)
        {
            Trigger = false;
            
            Panel.gameObject.GetComponent<Image>().sprite = Info[DayInfoIndex];

            if(DayInfoIndex == 0)
            {
                Close1.GetComponent<Button>().enabled = true;
                Close1.GetComponent<Image>().enabled = true;

                Close2.GetComponent<Button>().enabled = false;
                Close2.GetComponent<Image>().enabled = false;

            }
            else
            {
                Close1.GetComponent<Button>().enabled = false;
                Close1.GetComponent<Image>().enabled = false;

                Close2.GetComponent<Button>().enabled = true;
                Close2.GetComponent<Image>().enabled = true;
            }
        }
        


	}

    public void DayEventInfoClose()
    {
        if(Trigger3)
        {
            Trigger3 = false;
            Canvas.sortingOrder = Lowest_Rayer;

            return;
        }


        if (Trigger2)
        {

            Trigger2 = false;
            Canvas.sortingOrder = Lowest_Rayer;

            if (!dayBlinder.IsNoEventDay())
            {
                EventChanger.SendMessage("EventChangeTriggerOn");

            }

        }
    }

    public void CaleanderClick(int day)
    {
        Canvas.sortingOrder = Highest_Rayer;

        if (day < 11)
            DayInfoIndex = 0;
        else if (day < 21)
            DayInfoIndex = 1;
        else if (day < 31)
            DayInfoIndex = 2;


        Trigger = true;
        Trigger3 = true;
    }


    public void TriggerOn(int day)
    {
        Canvas.sortingOrder = Highest_Rayer;

        if (day == 1)
            DayInfoIndex = 0;
        else if (day == 11)
            DayInfoIndex = 1;
        else if (day == 21)
            DayInfoIndex = 2;

        Trigger = true;

    }

    public void Trigger2On()
    {
        Trigger2 = true;
    }

}
