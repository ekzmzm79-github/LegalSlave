using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class before_manager_3 : MonoBehaviour {

    public static before_manager_3 instance;
    public AudioClip sound_start;
    // Use this for initialization

    public void play_sound()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(sound_start);
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // 게임을 할 때 화면이 안꺼지도록
        Screen.SetResolution(600, 1024, true); // 비율을 항상 이 해상도에 맞춰줌
        if (before_manager_3.instance == null)
        {
            before_manager_3.instance = this;
        }
    }
}
