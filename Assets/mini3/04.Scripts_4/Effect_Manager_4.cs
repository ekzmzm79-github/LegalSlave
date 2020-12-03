using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect_Manager_4 : MonoBehaviour {

    public static Effect_Manager_4 instance;
    public Text txtSong_name, txtwriter;
    public GameObject[] prefabs_effect = new GameObject[4];
    public Transform bottom_point, Checker;
    public Transform[] quarter_note = new Transform[2];
    public Image progressBar_1, progressBar_2;

    float ratio = 0;
    int bb = 0;
    bool sw = true;

    public void set_Song_Data(string name, string writer)
    {
        txtSong_name.text = name;
        txtwriter.text = writer;
    }

    public void move_bottom_point(int i)
    {
        ratio = 0;
        if (i == 1)
        {
            bottom_point.position = new Vector3(-2.368f, -0.2577f, 0);
            Checker.position = new Vector3(-2.368f, -0.615f, 0);
            bottom_point.gameObject.SetActive(true);
            Checker.gameObject.SetActive(true);
            bb = 1;
        }
        else if(i == 2)
        {
            bottom_point.position = new Vector3(-2.368f, -1.889f, 0);
            Checker.position = new Vector3(-2.368f, -2.2463f, 0);
            bottom_point.gameObject.SetActive(true);
            Checker.gameObject.SetActive(true);
            bb = 2;
        }
        else
        {
            bottom_point.gameObject.SetActive(false);
            Checker.gameObject.SetActive(false);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(bb == 1)
        {
            ratio = Game_Manager_4.instance.get_count_time();
            if (ratio <= 1.0)
            {
                ratio = ratio * 4.686f;
                ratio = ratio -2.368f;
                bottom_point.position = new Vector3(ratio, -0.2577f, 0);
                Checker.position = new Vector3(ratio, -0.615f, 0);
            }
        }
        else if(bb == 2)
        {
            ratio = Game_Manager_4.instance.get_count_time();
            if (ratio <= 1.0)
            {
                ratio = ratio * 4.686f;
                ratio = ratio - 2.368f;
                bottom_point.position = new Vector3(ratio, -1.889f, 90f);
                Checker.position = new Vector3(ratio, -2.2463f, 0);
            }
        }

        if(Game_Manager_4.instance.get_game_state() != false)
        {
            Vector3 vec_0 = quarter_note[0].position;

            if (vec_0.y < 2.69  && sw == true)
            {
                sw = false;
            } 
            else if(vec_0.y > 3.64 && sw == false)
            {
                sw = true;
            }
            else
            {
                if(sw == true)
                {
                    quarter_note[0].position = vec_0 - new Vector3(0, 0.095f/4, 0);
                    quarter_note[1].position = quarter_note[1].position + new Vector3(-0.044f/4, 0.089f/4, 0);
                }
                else
                {
                    quarter_note[0].position = vec_0 + new Vector3(0, 0.095f/4, 0);
                    quarter_note[1].position = quarter_note[1].position + new Vector3(0.044f/4, -0.089f/4, 0);
                }
            }
        }
	}

    void Awake()
    {
        if (Effect_Manager_4.instance == null)
        {
            Effect_Manager_4.instance = this;
        }
        //초기화
    }

    public void floating_text_effect(int i)
    {
        Vector3 vec11 = Checker.position + new Vector3(0.2166f, 0.2166f*2, 0);
        GameObject clone = Instantiate(prefabs_effect[i], vec11, Quaternion.identity);
    }
}
