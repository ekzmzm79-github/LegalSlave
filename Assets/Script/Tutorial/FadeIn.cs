using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour {

    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
    }

    public GameObject BGMchange;
    public GameObject TutorialEnding2;

    int Lowest_Rayer = 0;

    public Image Panel;
    public Canvas Blinder;
    float fades = 1.0f;
    float time = 0;

    public bool Trigger;

    // Use this for initialization
    void Start ()
    {
        Trigger = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Trigger)
        {
            //Debug.Log(fades);

            if (fades <= 0)
            {
                BGMchange.SendMessage("Change");
                TutorialEnding2.SendMessage("IsChange");

                time = 0;
                Blinder.sortingOrder = Lowest_Rayer;
                Trigger = false;
            }


            
            time += Time.deltaTime;

            if (fades > 0 && time >= 0.1f)
            {
                fades -= 0.08f;
                Panel.color = new Color(0, 0, 0, fades);
                time = 0;
            }




        }
    }

    public void TriggerOn()
    {
        Trigger = true;
    }


}
