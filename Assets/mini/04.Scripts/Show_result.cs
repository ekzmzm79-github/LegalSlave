using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Show_result : MonoBehaviour {

    public void OnClick()
    {
        GameManager_3.instance.set_result_state(true);
        Sound_Manager_2.instance.play_sound(4);
        Effect_manager_3.instance.button_state(false);
        //Debug.Log("111111");
        int[] recipe_1 = GameManager_3.instance.GetComponent<Choice_menu>().out_recipe();
        int[] recipe_2 = GameManager_3.instance.get_use_mat();
        int use_count = GameManager_3.instance.get_use_count();
        //Debug.Log(use_count);
        //Debug.Log(recipe_1);
        //Debug.Log(recipe_1[5]);
        if (recipe_1[5] == use_count)
        {
            //Debug.Log("갯수같음");
            int[] recipe = new int[5];
            for (int i = 0; i < 5; i++)
            {
                recipe[i] = recipe_1[i];
            }
            Array.Sort(recipe);
            Array.Sort(recipe_2);

            bool state = true;
            for (int i = 0; i < 5; i++)
            {
                if (recipe[i] != recipe_2[i])
                {
                    state = false;
                    break;
                }
            }

            if (state == true)
            {
                GameManager_3.instance.GetComponent<Choice_menu>().change_txt("감사합니다.");
                Effect_manager_3.instance.floating_text_effect(true);
                Effect_manager_3.instance.emotion_state(0, true);
                Effect_manager_3.instance.emotion_state(1, true);
                Sound_Manager_2.instance.play_sound(3);
                GameManager_3.instance.add_point();
            }
            else
            {
                GameManager_3.instance.GetComponent<Choice_menu>().change_txt("우웩.");
                Effect_manager_3.instance.floating_text_effect(false);
                Effect_manager_3.instance.emotion_state(2, true);
                Sound_Manager_2.instance.play_sound(5);
                GameManager_3.instance.des_point();
            }
            GameManager_3.instance.del_all();
            //GameObject.Find("GameManager").GetComponent<GameManager>().delete_icon();

        }
        else
        {
            //Debug.Log("다름"+ recipe_1[5]+" "+ use_count);
            GameManager_3.instance.GetComponent<Choice_menu>().change_txt("우웩.");
            Effect_manager_3.instance.floating_text_effect(false);
            Effect_manager_3.instance.emotion_state(2, true);
            Sound_Manager_2.instance.play_sound(5);
            GameManager_3.instance.des_point();
            GameManager_3.instance.del_all();
            /*GameObject.Find("GameManager").GetComponent<GameManager>().des_point();
            GameObject.Find("GameManager").GetComponent<GameManager>().add_complain();
            GameObject.Find("GameManager").GetComponent<GameManager>().delete_icon();*/
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
