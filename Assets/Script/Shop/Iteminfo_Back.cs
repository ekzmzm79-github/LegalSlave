using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Iteminfo_Back : MonoBehaviour {
    public Canvas InfoPopup;
    public AudioSource audio;
    public AudioClip Cancle;
    // Use this for initialization
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void Click()
    {
        audio.Play();
        InfoPopup.sortingOrder = -1;
        //  ItemPanel.SetActive(false);
    }
}
