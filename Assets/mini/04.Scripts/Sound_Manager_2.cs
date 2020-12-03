using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager_2 : MonoBehaviour {

    public static Sound_Manager_2 instance;
    public AudioClip sound_water, sound_ice, sound_powder, sound_success, sound_click, sound_wrong;
    public Transform bgm_manager;

    public void off_bgm()
    {
        bgm_manager.GetComponent<AudioSource>().Stop();
    }

    public void play_sound(int i)
    {
        if (i == 0)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_water);
        }
        else if (i == 1)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_ice);
        }
        else if (i == 2)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_powder);
        }
        else if(i == 3)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_success);
        }
        else if(i == 4)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_click);
        }
        else if(i == 5)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_wrong);
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        if (Sound_Manager_2.instance == null)
        {
            Sound_Manager_2.instance = this;
        }
    }
}
