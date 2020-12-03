using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class before_manager_2 : MonoBehaviour {

    public static before_manager_2 instance;
    public Sprite[] img_game = new Sprite[2];
    public Button[] button = new Button[2];
    public AudioClip sound_click, sound_start;
    int Current_status = 0;

    public void play_sound(int i)
    {
        if (i == 0)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_click);
        }
        else if (i == 1)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_start);
        }
    }

    public void change_image(int i)
    {
        if (i == 1)
        {
            /*if (Current_status == 0)
            {
                Current_status = 2;
            }
            else
            {*/
            Current_status--;
            //}
        }
        else
        {
            /*if (Current_status == 2)
            {
                Current_status = 0;
            }
            else
            {*/
            Current_status++;
            //}
        }

        if (Current_status == 0)
        {
            button[0].gameObject.SetActive(false);
            button[1].gameObject.SetActive(true);
        }
        else
        {
            button[0].gameObject.SetActive(true);
            button[1].gameObject.SetActive(false);
        }
        gameObject.GetComponent<Image>().sprite = img_game[Current_status];
    }

    // Use this for initialization
    void Start () {
        button[0].gameObject.SetActive(false);
        button[1].gameObject.SetActive(true);
        gameObject.GetComponent<Image>().sprite = img_game[Current_status];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // 게임을 할 때 화면이 안꺼지도록
        Screen.SetResolution(600, 1024, true); // 비율을 항상 이 해상도에 맞춰줌
        if (before_manager_2.instance == null)
        {
            before_manager_2.instance = this;
        }
    }
}
