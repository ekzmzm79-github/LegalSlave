using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click_before_game_2 : MonoBehaviour {

    public void OnClick()
    {
        if (this.tag == "Button_left")
        {
            before_manager_2.instance.play_sound(0);
            before_manager_2.instance.change_image(1);
        }
        else if (this.tag == "Button_right")
        {
            before_manager_2.instance.play_sound(0);
            before_manager_2.instance.change_image(2);
        }
        else if (this.tag == "Button_start")
        {
            before_manager_2.instance.play_sound(1);
            StartCoroutine("transform_s");
        }
    }

    IEnumerator transform_s()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Document");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
