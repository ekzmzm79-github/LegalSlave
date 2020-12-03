using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
    }

    int Highest_Rayer = 10;

    public Image Panel;
    public Canvas Blinder;
    float fades = 0;
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
        if(Trigger)
        {
            //Debug.Log(fades);

            if (fades >= 1.0f)
            {
                SceneManager.LoadScene("TutorialEnding");
                time = 0;
            }


            Blinder.sortingOrder = Highest_Rayer;
            time += Time.deltaTime;

            if (fades < 1.0f && time >= 0.1f)
            {
                fades += 0.08f;
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
