using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CalendarMain : MonoBehaviour {

    public Text Day;
    public DayEventInfo dayEventInfo;
    int D;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void callDayEventInfo()
    {
        dayEventInfo.SendMessage("CaleanderClick", D);
    }


    void LoadDay(int day)
    {
        Day.text = day.ToString();
        D = day;
    }


    void SetDay(int day)
    {
        Day.text = day.ToString();
        D = day;
    }

}
