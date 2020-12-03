using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_3 : MonoBehaviour {

    public static GameManager_3 instance;
    public Transform[] people = new Transform[4];
    public Text txtWarning;
    public Text txtscore;
    public Text txtmoney, txtmarks;
    public GameObject score_window;
    Transform peo = null;

    bool game_state = true;
    bool people_state = false;
    bool result_state = false;
    int score = 0;
    float waiting_time = 0;
    int[] use_material = new int[5];
    int use_count = 0;

    public void set_result_state(bool b)
    {
        result_state = b;
    }

    public bool get_result_state()
    {
        return result_state;
    }

    public bool get_game_state()
    {
        return game_state;
    }

    public bool get_people_state()
    {
        return people_state;
    }

    public int[] get_use_mat()
    {
        return use_material;
    }

    public int get_use_count()
    {
        return use_count;
    }

    public void add_point()
    {
        score = score + 100;
    }

    public void des_point()
    {
        if (score >= 100)
        {
            score = score - 100;
        }
    }

    void update_score()
    {
        txtscore.text = "" + score;
    }

    public int get_score()
    {
        return score;
    }

    public void reset_material()
    {
        use_count = 0;
        for (int i = 0; i < 5; i++)
        {
            use_material[i] = -1;
        }
    }

    void Add_people()
    {
        reset_material();
        people_state = true;
        int num = Random.Range(0, 4);
        if (num == 0)
        {
            peo = Instantiate(people[0], new Vector3(0.08f, 2.29f, 0), Quaternion.identity);
        }
        else if (num == 1)
        {
            peo = Instantiate(people[1], new Vector3(0.08f, 2.29f, 0), Quaternion.identity);
        }
        else if(num == 2)
        {
            peo = Instantiate(people[2], new Vector3(0.08f, 2.29f, 0), Quaternion.identity);
        }
        else
        {
            peo = Instantiate(people[3], new Vector3(0.08f, 2.29f, 0), Quaternion.identity);
        }
        this.GetComponent<Choice_menu>().init();
        Effect_manager_3.instance.demand_state(true);
        waiting_time = this.GetComponent<Time_countdown_3>().get_time();
        //choice_ = Instantiate(cho, new Vector3((float)141, (float)-148.552, 0), Quaternion.identity);
    }

    void Del_people()
    {
        people_state = false;
        Destroy(peo.gameObject);
        this.GetComponent<Choice_menu>().change_txt("");
        Effect_manager_3.instance.del_icon();
        for (int i = 0; i < 3; i++)
        {
            Effect_manager_3.instance.base_state(i, false);
        }
    }

    public void del_all()
    {
        Effect_manager_3.instance.button_state(false);
        update_score();
        StartCoroutine("del");
    }

    IEnumerator del()
    {
        yield return new WaitForSeconds(1);
        Effect_manager_3.instance.emotion_state(0, false);
        Effect_manager_3.instance.emotion_state(1, false);
        Effect_manager_3.instance.emotion_state(2, false);
        Effect_manager_3.instance.demand_state(false);
        Del_people();
        result_state = false;
    }

    public void set_material(int i)
    {
        if (use_count == 0)
        {
            Effect_manager_3.instance.button_state(true);
        }
        if (use_count < 5)
        {
            Effect_manager_3.instance.effect(i);
            use_material[use_count] = i;
            use_count++;
            Effect_manager_3.instance.add_icon(i);
        }
        else
        {
            txtWarning.text = "이미 많은 재료를\n넣었습니다!";
            StartCoroutine("reset_text");
        }
    }

    IEnumerator reset_text()
    {
        yield return new WaitForSeconds(1);
        txtWarning.text = "";
    }

    // Use this for initialization
    void Start () {
        score_window.SetActive(false);
        Add_people();
        Effect_manager_3.instance.button_state(false);
        for(int i = 0; i<3;i++)
        {
            Effect_manager_3.instance.emotion_state(i, false);
            Effect_manager_3.instance.base_state(i, false);
        }
    }

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // 게임을 할 때 화면이 안꺼지도록
        Screen.SetResolution(600, 1024, true); // 비율을 항상 이 해상도에 맞춰줌
        if (GameManager_3.instance == null)
        {
            GameManager_3.instance = this;
        }
        //초기화
    }


    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuitClass.GameQuit();
        }



        if (game_state == true)
        {
            if (GameManager_3.instance.GetComponent<Time_countdown_3>().get_iscountdown() == false)
            {
                if(people_state == true)
                {
                    Effect_manager_3.instance.button_state(false);
                    Effect_manager_3.instance.demand_state(false);
                    Del_people();
                }
                Sound_Manager_2.instance.off_bgm();
                score_window.SetActive(true);

                txtmarks.text = "" + score;
                score = score - 1000;
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

            if (people_state == true && waiting_time - this.GetComponent<Time_countdown_3>().get_time() > 15.0 && result_state == false)
            {
                Effect_manager_3.instance.button_state(false);
                Effect_manager_3.instance.demand_state(false);
                Del_people();
            }

            if (waiting_time != 0 && people_state == false && this.GetComponent<Time_countdown_3>().get_time() >= 1.0)
            {
                waiting_time = 0;
                StartCoroutine("order");
            }
        }
    }

    IEnumerator order()
    {
        yield return new WaitForSeconds(1);
        Add_people();
    }
}
