using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Material_manager : MonoBehaviour {

    public static Material_manager instance;
    public Sprite[] Material = new Sprite[6]; // 0:커피  1:설탕  2:프리마  3:녹차  4:물  5:얼음
    int Current_status = 0;

    public void OnClick()
    {
        if (GameManager_3.instance.get_result_state() == false && GameManager_3.instance.get_game_state() == true && GameManager_3.instance.get_people_state() == true)
        {
            GameManager_3.instance.set_material(Current_status);
        }
    }

    public void change_image(int i)
    {
        if (i == 1)
        {
            if (Current_status == 0)
            {
                Current_status = 5;
            }
            else
            {
                Current_status--;
            }
        }
        else
        {
            if (Current_status == 5)
            {
                Current_status = 0;
            }
            else
            {
                Current_status++;
            }
        }
        gameObject.GetComponent<Image>().sprite = Material[Current_status];

    }

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Image>().sprite = Material[Current_status];
    }

    void Awake()
    {
        if (Material_manager.instance == null)
        {
            Material_manager.instance = this;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
