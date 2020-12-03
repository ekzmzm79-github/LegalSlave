using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Click : MonoBehaviour {

    public void OnClick()
    {
        if(Game_Manager_4.instance.get_game_state() == true && Game_Manager_4.instance.get_start_count_state() == false)
        {
           if(this.tag == "Button_Tambourine")
            {
                Note_Manager.instance.Click_point_check(0);
                Sound_Manager_4.instance.play_sound(0);
            }
            else if(this.tag == "Button_Maracas")
            {
                Note_Manager.instance.Click_point_check(1);
                Sound_Manager_4.instance.play_sound(1);
            }
            else if(this.tag == "Button_Castanets")
            {
                Note_Manager.instance.Click_point_check(2);
                Sound_Manager_4.instance.play_sound(2);
            }
            else if(this.tag == "Button_Clap")
            {
                Note_Manager.instance.Click_point_check(3);
                Sound_Manager_4.instance.play_sound(3);
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
