using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSound : MonoBehaviour {

    AudioSource audio;

    public AudioClip Choose;
    public AudioClip Cancle;
    public AudioClip Buy;


    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
    }
	
    public void BuyClickSound()
    {
        audio.clip = Buy;
        audio.Play();
    }
    public void CancleClickSound()
    {
        audio.clip = Cancle;
        audio.Play();

    }
    public void ChooseClickSound()
    {
        audio.clip = Choose;
        audio.Play();

    }
    // Update is called once per frame
    void Update () {

     //   this.audio.Play();
    }
}
