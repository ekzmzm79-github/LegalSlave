using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour {

    public AudioClip sound_success, sound_error, sound_break, sound_click;
    public static Sound_Manager instance;
    public Transform bgm_manager;

    void Awake()
    {
        if(Sound_Manager.instance == null)
        {
            Sound_Manager.instance = this;
        }
    }

    public void off_bgm()
    {
        bgm_manager.GetComponent<AudioSource>().Stop();
    }

    public void play_sound(int i)
    {
        if(i == 0)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_success);
        }
        else if(i == 1)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_error);
        }
        else if(i == 2)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_break);
        }
        else if(i == 3)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_click);
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
