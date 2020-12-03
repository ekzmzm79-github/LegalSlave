using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click_before_game : MonoBehaviour {

    public void OnClick()
    {
        if (this.tag == "Button_left")
        {
            before_manager.instance.play_sound(0);
            before_manager.instance.change_image(1);
        }
        else if (this.tag == "Button_right")
        {
            before_manager.instance.play_sound(0);
            before_manager.instance.change_image(2);
        }
        else if(this.tag == "Button_start")
        {
            before_manager.instance.play_sound(1);
            StartCoroutine("transform_s");
        }
    }

    IEnumerator transform_s()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Coffee");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
