using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect_manager_3 : MonoBehaviour {

    public static Effect_manager_3 instance;
    public GameObject[] emotion = new GameObject[3]; // 0 : good 1 : good 2 : bad
    public GameObject[] button = new GameObject[2]; // 0 : mix 1 : reset
    public Transform[] prefab_icon = new Transform[6];
    public GameObject[] base_img = new GameObject[3]; // 0 : water 1 : coffee 2 : green
    public GameObject demand;
    public Transform water_eff, ice_eff, green_eff, coffee_eff, white_eff;
    public GameObject prefabs_Floating_text;
    Vector3[] vec = new Vector3[5];
    Transform[] icon_obj = new Transform[5];
    Transform eff;

    bool[] bases_state = new bool[3];

    public void floating_text_effect(bool b)
    {
        Vector3 vec11 = new Vector3(-1.98f, 1.58f, 0);
        GameObject clone = Instantiate(prefabs_Floating_text, vec11, Quaternion.identity);
        clone.transform.SetParent(GameObject.Find("Canvas").transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
        clone.transform.position = vec11 + new Vector3(0, 1, 0);
        if (b == false)
        {
            clone.GetComponent<Floating_text_2>().text.color = Color.red;
            clone.GetComponent<Outline>().effectColor = new Vector4((float)0.55, (float)0.05, (float)0.05, 1);
            clone.GetComponent<Floating_text_2>().text.text = "-100";
        }
        else
        {
            clone.GetComponent<Floating_text_2>().text.color = new Vector4((float)0.04, (float)0.55, (float)0.05, 1);
            clone.GetComponent<Outline>().effectColor = new Vector4((float)0.04, (float)0.55, (float)0.05, 1);
            clone.GetComponent<Floating_text_2>().text.text = "+100";
        }

    }

    public void effect(int i)
    {
        GameManager_3.instance.set_result_state(true);
        if(i == 0)
        {
            eff = Instantiate(coffee_eff, new Vector3(0.04f, -2.31f, 14.5f), Quaternion.identity);
            Sound_Manager_2.instance.play_sound(2);
            bases_state[1] = true;
            if (bases_state[0] == true)
            {
                base_state(1, true);
            }
        }
        else if(i == 1 || i == 2)
        {
            eff = Instantiate(white_eff, new Vector3(0.04f, -2.31f, 14.5f), Quaternion.identity);
            Sound_Manager_2.instance.play_sound(2);
        }
        else if(i == 3)
        {
            eff = Instantiate(green_eff, new Vector3(0.04f, -2.31f, 14.5f), Quaternion.identity);
            Sound_Manager_2.instance.play_sound(2);
            bases_state[2] = true;
            if (bases_state[1] == false && bases_state[0] == true)
            {
                base_state(2, true);
            }
        }
        else if(i == 4)
        {
            eff = Instantiate(water_eff, new Vector3(0.05f, -2.12f, 14.5f), Quaternion.identity);
            Sound_Manager_2.instance.play_sound(0);
            bases_state[0] = true;
            if(bases_state[1] == true)
            {
               base_state(1, true);
            }
            else if(bases_state[2] == true)
            {
               base_state(2, true);
            }
            else
            {
               base_state(0, true);
            }
        }
        else
        {
            eff = Instantiate(ice_eff, new Vector3(0.06f, -2.07f, 14.5f), Quaternion.identity);
            Sound_Manager_2.instance.play_sound(1);
        }
        
        StartCoroutine("eff_del");
    }
    IEnumerator eff_del()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(eff.gameObject);
        GameManager_3.instance.set_result_state(false);
    }

    public void emotion_state(int i, bool b)
    {
        emotion[i].SetActive(b);
    }

    public void base_state(int i, bool b)
    {
        bases_state[i] = b;
        base_img[i].SetActive(b);
    }

    public void button_state(bool b)
    {
        for(int i = 0; i<2;i++)
        {
            button[i].SetActive(b);
        }
    }

    public void demand_state(bool b)
    {
        demand.SetActive(b);
    }

    public void add_icon(int i)
    {
        int num = GameManager_3.instance.get_use_count() - 1;
        icon_obj[num] = Instantiate(prefab_icon[i], vec[num], Quaternion.identity);
    }

    public void del_icon()
    {
        //Debug.Log(GameManager_3.instance.out_use_count());
        for(int i = 0; i < GameManager_3.instance.get_use_count(); i++)
        {
            Destroy(icon_obj[i].gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        vec[0] = new Vector3(0.04f, -2.44f, 0f);
        vec[1] = new Vector3(-0.54f, -2.74f, 0f);
        vec[2] = new Vector3(0.54f, -2.74f, 0f);
        vec[3] = new Vector3(-0.28f, -3.16f, 0f);
        vec[4] = new Vector3(0.28f, -3.16f, 0f);

        for(int i = 0; i<3;i++)
        {
            base_state(i, false);
        }
        //초기화
        for(int i = 0;i<5;i++)
        {
            icon_obj[i] = null;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        if (Effect_manager_3.instance == null)
        {
            Effect_manager_3.instance = this;
        }
    }
}
