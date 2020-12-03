using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_count : MonoBehaviour {

    public Sprite[] count = new Sprite[4];
    public Transform start_count;
    // Use this for initialization

    void Start () {
	}

    void OnEnable()
    {
        int i = 0;
        gameObject.GetComponent<Image>().sprite = count[i];
        i++;
        StartCoroutine("change_img", i);
    }

    IEnumerator change_img(int i)
    {
        yield return new WaitForSeconds(0.5f);
        if(i == 4)
        {
            start_count.gameObject.SetActive(false);
            Effect_Manager_4.instance.set_Song_Data("", "");
            Game_Manager_4.instance.init_point();
            Game_Manager_4.instance.set_start_count_state();
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = count[i];
            i++;
            StartCoroutine("change_img", i);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
