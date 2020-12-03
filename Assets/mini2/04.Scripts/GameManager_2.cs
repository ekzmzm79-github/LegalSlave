using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_2 : MonoBehaviour
{
    public static GameManager_2 instance;

    public GameObject score_window;
    public Transform Paper, Paper2, Paper3;
    public Text txt_score;
    public Text txtmoney, txtmarks;
    public Text[] txt_outline = new Text[4];
    bool game_state = true;
    int score;
    Transform[] Papers = new Transform[7];
    Vector3[] Papers_po = new Vector3[7];


    public bool get_game_state()
    {
        return game_state;
    }

    void upd_score()
    {
        txt_score.text = score + "";
        for(int i = 0;i<4;i++)
        {
            txt_outline[i].text = score + "";
        }
    }

    public void add_score()
    {
        score = score + 100;
        upd_score();
    }

    public void ded_score()
    {
        if(score > 0)
        {
            score = score - 100;
        }
        upd_score();
    }
    
    public int get_score()
    {
        return score;
    }

    public void set_paper_vector(int num, Vector3 vec)
    {
        //Debug.Log("number"+num);
        if (num == 6)
        {
            Papers[num].position = vec;
        }
        else
        {
            Papers[num].position = Papers_po[num + 1];
            Papers[num + 1] = Papers[num];
        }
    }

    public Vector3 get_paper_vector()
    {
        return Papers_po[6];
    }

    public void del_paper()
    {
        Destroy(Papers[6].gameObject);
    }

    public void add_paper()
    {
        int num;
        num = Random.Range(0, 3);
        if (num == 0)
        {
            Papers[0] = Instantiate(Paper, Papers_po[0], Quaternion.identity);
        }
        else if(num == 1)
        {
            Papers[0] = Instantiate(Paper2, Papers_po[0], Quaternion.identity);
        }
        else if(num == 2)
        {
            Papers[0] = Instantiate(Paper3, Papers_po[0], Quaternion.identity);
        }
        for(int i = 6; i >= 0;i--)
        {
            Papers[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = i;
        }
        GameManager_2.instance.GetComponent<Paper_settings>().init_info();
    }

    //가장먼저 실행 게임의 가장 기초적인 세팅
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // 게임을 할 때 화면이 안꺼지도록
        Screen.SetResolution(600, 1024, true); // 비율을 항상 이 해상도에 맞춰줌
        if(GameManager_2.instance == null)
        {
            GameManager_2.instance = this;
        }
    }

    // 오브젝트가 처음 생성될 때 실행
    void Start()
    {
        score_window.SetActive(false);
        GameManager_2.instance.GetComponent<Group_settings>().Text_setting();
        int num;
        for(int i = 0;i<7;i++)
        {
            //Papers_po[i] = new Vector3((float)(0.1+(-1*i)*0.005), (float)(0.48-i*0.27),0);
            Papers_po[i] = new Vector3((float)(0.1 + (-1 * i) * 0.005), (float)(2.0 - i * 0.42), 0);
            //Debug.Log(Papers_po[i]);
            num = Random.Range(0, 3);
            if(num == 0)
            {
                Papers[i] = Instantiate(Paper, Papers_po[i], Quaternion.identity);
            }
            else if(num == 1)
            {
                Papers[i] = Instantiate(Paper2, Papers_po[i], Quaternion.identity);
            }
            else if(num == 2)
            {
                Papers[i] = Instantiate(Paper3, Papers_po[i], Quaternion.identity);
            }
            Papers[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = i;
        }
        GameManager_2.instance.GetComponent<Paper_settings>().init_info();
    }


    // 매 프레임 마다 실행
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuitClass.GameQuit();
        }


        if (game_state == true && GameManager_2.instance.GetComponent<Time_countdown_2>().get_iscountdown() == false)
        {
            Sound_Manager.instance.off_bgm();
            score_window.SetActive(true);
            txtmarks.text = "" + score;
            score = score - 1500;
            if (score > 0)
            {
                txtmoney.text = score + "";
            }
            else
            {
                score = 0;
                txtmoney.text = "0";
            }
            game_state = false;
            //SceneManager.LoadScene("First");
        }
    }
}
