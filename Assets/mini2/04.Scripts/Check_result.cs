using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Check_result : MonoBehaviour {

    public bool get_result(int i)
    {
        if (i == 1)
        {
            string str_g1 = GameManager_2.instance.GetComponent<Group_settings>().get_g1();
            string str_info = GameManager_2.instance.GetComponent<Paper_settings>().get_info();
            //Debug.Log(str_g1);
            //Debug.Log(str_info);
            if (str_g1.Substring(2, 2).Equals(str_info.Substring(2, 2)) && (int.Parse(str_g1.Substring(0, 2)) >= int.Parse(str_info.Substring(0, 2))))
            {
                GameManager_2.instance.add_score();
                Sound_Manager.instance.play_sound(0);
                return true;
            }
            else
            {
                GameManager_2.instance.ded_score();
                Sound_Manager.instance.play_sound(1);
                return false;
            }
        }
        else if (i == 2)
        {
            string str_g1 = GameManager_2.instance.GetComponent<Group_settings>().get_g1();
            string str_g2 = GameManager_2.instance.GetComponent<Group_settings>().get_g2();
            string str_info = GameManager_2.instance.GetComponent<Paper_settings>().get_info();
            if (str_g1.Substring(2, 2).Equals(str_info.Substring(2, 2)) && (int.Parse(str_g1.Substring(0, 2)) >= int.Parse(str_info.Substring(0, 2))))
            {
                GameManager_2.instance.ded_score();
                Sound_Manager.instance.play_sound(2);
                return false;
            }
            else if (str_g2.Substring(2, 2).Equals(str_info.Substring(4, 2)) && (int.Parse(str_g2.Substring(0, 2)) <= int.Parse(str_info.Substring(0, 2))))
            {
                GameManager_2.instance.ded_score();
                Sound_Manager.instance.play_sound(2);
                return false;
            }
            else
            {
                GameManager_2.instance.add_score();
                Sound_Manager.instance.play_sound(2);
                return true;
            }
        }
        else
        {
            string str_g2 = GameManager_2.instance.GetComponent<Group_settings>().get_g2();
            string str_info = GameManager_2.instance.GetComponent<Paper_settings>().get_info();
            if (str_g2.Substring(2, 2).Equals(str_info.Substring(4, 2)) && (int.Parse(str_g2.Substring(0, 2)) <= int.Parse(str_info.Substring(0, 2))))
            {
                GameManager_2.instance.add_score();
                Sound_Manager.instance.play_sound(0);
                return true;
            }
            else
            {
                GameManager_2.instance.ded_score();
                Sound_Manager.instance.play_sound(1);
                return false;
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
