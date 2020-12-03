using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm_manager : MonoBehaviour {

    public AudioClip bgm, android_bgm;

    // Use this for initialization
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(android_bgm);
        }
        else
        {
            //this.gameObject.GetComponent<AudioSource>().PlayOneShot(android_bgm);
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(bgm);
        }
    }
}
