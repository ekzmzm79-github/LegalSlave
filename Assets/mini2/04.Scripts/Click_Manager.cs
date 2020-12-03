using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Manager : MonoBehaviour {

    public void OnClick()
    {
        if(GameManager_2.instance.get_game_state() == true)
        {
            if (this.tag == "Box_1")
            {
                //Debug.Log(this.tag);
                Effect_manager.instance.GetComponent<Effect_manager>().click_move(1);
            }
            else if (this.tag == "Box_2")
            {
                //Debug.Log(this.tag);
                Effect_manager.instance.GetComponent<Effect_manager>().click_move(3);
            }
            else if (this.tag == "Breaker")
            {
                //Debug.Log(this.tag);
                Effect_manager.instance.GetComponent<Effect_manager>().click_move(2);
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
