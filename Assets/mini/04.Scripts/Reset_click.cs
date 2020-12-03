using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_click : MonoBehaviour {

    public void OnClick()
    {
        Effect_manager_3.instance.del_icon();
        GameManager_3.instance.reset_material();
        Sound_Manager_2.instance.play_sound(4);
        Effect_manager_3.instance.button_state(false);
        for (int i = 0; i < 3; i++)
        {
            Effect_manager_3.instance.base_state(i, false);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
