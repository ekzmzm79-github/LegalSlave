using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect_manager : MonoBehaviour {

    public static Effect_manager instance;
    public GameObject prefabs_Floating_text;
    public Transform[] box_button = new Transform[3];
    Vector3 pos;
    //pos = this.gameObject.transform.position;

    
    void floating_text_effect(int i, bool b)
    {
        Vector3 vec;
        if (i == 1)
        {
            vec = new Vector3(-1.9f, -3.5f, 0.0f);
            //Debug.Log(vec);
        }
        else if(i == 2)
        {
            vec = new Vector3(0.1f, -3.5f, 0.0f);
            //Debug.Log(vec);
        }
        else
        {
            vec = new Vector3(2.0f, -3.5f, 0.0f);
            //Debug.Log(vec);
        }
        GameObject clone = Instantiate(prefabs_Floating_text, vec, Quaternion.identity);
        clone.transform.SetParent(GameObject.Find("Canvas").transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
        clone.transform.position = vec + new Vector3(0, 1, 0);
        if (b == false)
        {
            clone.GetComponent<Floating_text>().text.color = Color.red;
            clone.GetComponent<Outline>().effectColor = new Vector4((float)0.55, (float)0.05, (float)0.05, 1);
            clone.GetComponent<Floating_text>().text.text = "-100";
        }
        else
        {
            clone.GetComponent<Floating_text>().text.color = new Vector4((float)0.04, (float)0.55, (float)0.05, 1);
            clone.GetComponent<Outline>().effectColor = new Vector4((float)0.04, (float)0.55, (float)0.05, 1);
            clone.GetComponent<Floating_text>().text.text = "+100";
        }
        


    }
    
    public void click_move(int num)
    {
        pos = GameManager_2.instance.get_paper_vector();
        for(int i = 0; i<3;i++)
        {
            box_button[i].GetComponent<Button>().interactable = false;
        }
        StartCoroutine("available");
        if (num == 1)
        {
            pos = pos + new Vector3((float)-1.2, (float)-0.6, 0);
            Vector3 txt_pos = GameManager_2.instance.GetComponent<Paper_settings>().get_positon() + new Vector3((float)-1.2, (float)-0.6, 0);
            floating_text_effect(num, GameManager_2.instance.GetComponent<Check_result>().get_result(1));
            GameManager_2.instance.GetComponent<Paper_settings>().chposition(txt_pos);
            
        }
        else if(num == 2)
        {
            pos = pos + new Vector3(0, (float)-0.6, 0);
            Vector3 txt_pos = GameManager_2.instance.GetComponent<Paper_settings>().get_positon() + new Vector3(0, (float)-0.6, 0);
            floating_text_effect(num,GameManager_2.instance.GetComponent<Check_result>().get_result(2));
            GameManager_2.instance.GetComponent<Paper_settings>().chposition(txt_pos);
        }
        else if(num == 3)
        {
            pos = pos + new Vector3((float)1.2, (float)-0.6, 0);
            Vector3 txt_pos = GameManager_2.instance.GetComponent<Paper_settings>().get_positon() + new Vector3((float)1.2, (float)-0.6, 0);
            floating_text_effect(num,GameManager_2.instance.GetComponent<Check_result>().get_result(3));
            GameManager_2.instance.GetComponent<Paper_settings>().chposition(txt_pos);
        }
        GameManager_2.instance.set_paper_vector(6, pos);
        StartCoroutine("del");
        StartCoroutine("move");
    }

    IEnumerator del()
    {
        //Debug.Log("5초 후에");
        yield return new WaitForSeconds(0.2f);
        GameManager_2.instance.del_paper();
        GameManager_2.instance.GetComponent<Paper_settings>().change_text("");
        //Debug.Log("5초 지남");
    }
    IEnumerator move()
    {
        //Debug.Log("5초 후에");
        yield return new WaitForSeconds(0.6f);
        for(int i = 5;i>=0;i--)
        {
            GameManager_2.instance.set_paper_vector(i, pos);
        }
        GameManager_2.instance.GetComponent<Paper_settings>().chposition(GameManager_2.instance.GetComponent<Paper_settings>().get_positon());
        GameManager_2.instance.add_paper();
    }

    IEnumerator available()
    {
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < 3; i++)
        {
            box_button[i].GetComponent<Button>().interactable = true;
        } 
    }

    // Use this for initialization
    void Start () {
        pos = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        if (Effect_manager.instance == null)
        {
            Effect_manager.instance = this;
        }
    }
}
