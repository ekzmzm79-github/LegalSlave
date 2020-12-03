using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_countdown_4 : MonoBehaviour {

    float countDown = 75.0f;
    bool isCountDown = true;

    public bool get_iscountdown()
    {
        return isCountDown;
    }

    public float get_time()
    {
        return countDown;
    }

    // Use this for initialization
    void Start () {
        isCountDown = true;
        //시간 지정
        countDown = 75.0f;
        //txtCountdown.text = "0" + (int)countDown;
    }
	
	// Update is called once per frame
	void Update () {
        if (isCountDown)
        {
            countDown -= Time.deltaTime;
            //txtCountdown.text = "" + ((int)countDown + 1);
            //progressBar.fillAmount = Mathf.Lerp(0.0f, 1.0f, ((countDown) / 120.0f));
            if (countDown < 0)
            {
                //txtCountdown.text = "" + "0";
                isCountDown = false;
            }
        }
    }
}
