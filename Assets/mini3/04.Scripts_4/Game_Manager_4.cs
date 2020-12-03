using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager_4 : MonoBehaviour {

    public static Game_Manager_4 instance;
    public Image progressBar_1, progressBar_2;
    public Text txtpoint_title, txtpoint;
    public GameObject score_window;
    public Text txtmoney, txtmarks;

    bool game_state = true;
    bool start_count_state = true;

    bool p_g_1 = false;
    bool p_g_2 = false;
    int change_p_g = -1;
    int point = 0;
    float difficulty = 3.0f;
    float countDown = 0;

    public int get_score()
    {
        return point;
    }

    public void init_point()
    {
        txtpoint_title.text = "Score";
        txtpoint.text = "0";
    }

    public void update_point()
    {
        txtpoint.text = "" + point;
    }

    public void add_point(int num)
    {
        if(num == 3)
        {
            point = point + 200;
        }
        else if(num == 2)
        {
            point = point + 100;
        }
        else
        {
            point = point + 50;
        }
        update_point();
    }

    public void minus_point()
    {
        if(point >= 100)
        {
            point = point - 100;
            update_point();
        }
        else if(point == 50)
        {
            point = 0;
            update_point();
        }
    }


    public void set_start_count_state()
    {
        start_count_state = false;
        //Debug.Log(75-Game_Manager_4.instance.GetComponent<Time_countdown_4>().get_time());
    }

    public bool get_start_count_state()
    {
        return start_count_state;
    }

    public int get_p_g_state()
    {
        if(p_g_1 == true)
        {
            return 1;
        }
        else if(p_g_2 == true)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    public float get_count_time()
    {
        return (countDown) / difficulty;
    }

    public bool get_game_state()
    {
        return game_state;
    }

    // Use this for initialization
    void Start () {
        score_window.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuitClass.GameQuit();
        }

        if (game_state == true && Game_Manager_4.instance.GetComponent<Time_countdown_4>().get_iscountdown() == false)
        {
            game_state = false;
            Sound_Manager_4.instance.off_bgm();
            score_window.SetActive(true);
            txtmarks.text = "" + point;
            point = point - 12000;
            if (point > 0)
            {
                point = point / 200;
                point = point * 100;
                txtmoney.text = point + "";
            }
            else
            {
                point = 0;
                txtmoney.text = "0";
            }
        }

        if (Note_Manager.instance.get_read_state() && start_count_state == false && p_g_1 == false && p_g_2 == false && game_state == true)
        {
            change_p_g = change_p_g * -1;
            if(change_p_g == 1)
            {
                p_g_1 = true;
                countDown = 0;
                Effect_Manager_4.instance.move_bottom_point(1);
            }
            else
            {
                p_g_2 = true;
                countDown = 0;
                Effect_Manager_4.instance.move_bottom_point(2);
            }
        }

        if((p_g_1 ==true || p_g_2 == true) && game_state == true)
        {
            countDown += Time.deltaTime;
            if (countDown <= difficulty)
            {
                if (p_g_1)
                {
                    progressBar_1.fillAmount = Mathf.Lerp(0.0f, 1.0f, ((countDown) / difficulty));
                }
                else
                {
                    progressBar_2.fillAmount = Mathf.Lerp(0.0f, 1.0f, ((countDown) / difficulty));
                }
            }
            else
            {
                if(change_p_g == 1)
                {
                    p_g_1 = false;
                    progressBar_1.fillAmount = Mathf.Lerp(0.0f, 1.0f, 0f);
                    Note_Manager.instance.init_top_node();
                    if (Note_Manager.instance.get_node_state())
                    {
                        Note_Manager.instance.set_top_node();
                    }
                    Effect_Manager_4.instance.move_bottom_point(0);
                }
                else
                {
                    p_g_2 = false;
                    progressBar_2.fillAmount = Mathf.Lerp(0.0f, 1.0f, 0f);
                    Note_Manager.instance.init_bottom_node();
                    if (Note_Manager.instance.get_node_state())
                    {
                        Note_Manager.instance.set_bottom_node();
                    }
                    Effect_Manager_4.instance.move_bottom_point(0);
                }
                Note_Manager.instance.init_note_cover();
            }
            
            
        }
	}

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // 게임을 할 때 화면이 안꺼지도록
        Screen.SetResolution(600, 1024, true); // 비율을 항상 이 해상도에 맞춰줌
        if (Game_Manager_4.instance == null)
        {
            Game_Manager_4.instance = this;
        }
        //초기화
    }
}
