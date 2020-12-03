using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMchange : MonoBehaviour {

    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
    }


    public AudioClip[] clips;
    AudioSource audio;

    bool IsChange;
    int audio_Index;

	// Use this for initialization
	void Start ()
    {
        IsChange = false;
        audio = GetComponent<AudioSource>();
        audio.clip = clips[audio_Index];
        audio.Play();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (IsChange)
        {
            audio.clip = clips[audio_Index];
            audio.Play();

            IsChange = false;
        }
	}

    public void Change()
    {
        IsChange = true;
        audio_Index++;
    }

}
