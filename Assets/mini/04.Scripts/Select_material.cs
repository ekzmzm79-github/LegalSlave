using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_material : MonoBehaviour {

    public void OnClick()
    {
        if (GameManager_3.instance.get_result_state() == false && GameManager_3.instance.get_game_state() == true && GameManager_3.instance.get_people_state() == true)
        {
            if (this.tag == "Button_left")
            {
                Sound_Manager_2.instance.play_sound(4);
                Material_manager.instance.change_image(1);
            }
            else if (this.tag == "Button_right")
            {
                Sound_Manager_2.instance.play_sound(4);
                Material_manager.instance.change_image(2);
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
