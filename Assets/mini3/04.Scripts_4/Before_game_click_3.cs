using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Before_game_click_3 : MonoBehaviour {

    public void OnClick()
    {
        if (this.tag == "Button_start")
        {
            before_manager_3.instance.play_sound();
            StartCoroutine("transform_s");
        }
    }

    IEnumerator transform_s()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Karaoke");
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
