using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager_4 : MonoBehaviour {

    public static Sound_Manager_4 instance;
    public AudioClip sound_tambourine, sound_maracas, sound_castanets, sound_clap, sound_click;
    public Transform bgm_manager;

    public void off_bgm()
    {
        bgm_manager.gameObject.GetComponent<AudioSource>().Stop();
    }

    public void play_sound(int i)
    {
        if (i == 0)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_tambourine);
        }
        else if (i == 1)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_maracas);
        }
        else if (i == 2)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_castanets);
        }
        else if (i == 3)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_clap);
        }
        else if (i == 4)
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

    void Awake()
    {
        if (Sound_Manager_4.instance == null)
        {
            Sound_Manager_4.instance = this;
        }
        
    }

}
