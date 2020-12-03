using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_countdown_2 : MonoBehaviour {

    public Text txtCountdown;
    public Text[] txt_Out = new Text[4];
    float countDown = 60.0f;
    bool isCountDown = true;

    // Use this for initialization
    void Start()
    {

    }

    public bool get_iscountdown()
    {
        return isCountDown;
    }

    public bool get_creatime()
    {
        if ((int)countDown - 1 >= 0)
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountDown)
        {
            countDown -= Time.deltaTime;
            txtCountdown.text = "" + ((int)countDown + 1);
            for(int i = 0; i<4;i++)
            {
                txt_Out[i].text = "" + ((int)countDown + 1);
            }
            if (countDown < 0)
            {
                txtCountdown.text = "" + "0";
                for (int i = 0; i < 4; i++)
                {
                    txt_Out[i].text = "" + "0";
                }
                /*Color color = txtCountdown.color;
                color.a = 0.0f;
                txtCountdown.color = color;*/
                isCountDown = false;
            }
        }
    }

    public void startCountDown()
    {
        isCountDown = true;
        countDown = 60.0f;
        txtCountdown.text = "0" + (int)countDown;
        for (int i = 0; i < 4; i++)
        {
            txt_Out[i].text = "0" + (int)countDown;
        }
        /*Color color = txtCountdown.color;
        color.a = 0.0f;
        txtCountdown.color = color;*/
    }
}
