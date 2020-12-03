using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;


public class ButtonManager : MonoBehaviour {

    public static int buttonnumber;
    public int num;
    public static int[] random_array;//아이템 저장된 배열
    public static int button_range;

    // Use this for initialization
    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);
        random_array = new int[12];
        if (save.EventIndex == 6 && GameLoadClass.GameLoadTrigger)
        {
            for (int i = 0; i <= 5; i++)
            {
                random_array[i] = save.Shoplist[i];
            }
            button_range = save.Shoprange;
        }
        else
        {
            button_range = UnityEngine.Random.Range(1, 6);//상점 아이템 개수 6개
            for (int i = 0; i <= 11; i++)    //숫자 4개를 뽑기위한 for문
            {
                random_array[i] = UnityEngine.Random.Range(0, 12);
                for (int j = 0; j < i; j++) //중복제거를 위한 for문 
                {
                    if (random_array[i] == random_array[j])
                    {
                        i--;
                        break;
                    }
                }
            }
        }
        save.EventIndex = 6;
        for (int i = 0; i <= 5; i++)
        {
            save.Shoplist[i] = random_array[i];
        }
        save.Shoprange = button_range;
        string PutData = JsonMapper.ToJson(save);
        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);
    }

    void Start () {
        
       

    }
	public static int ButtonImageDecision(GameObject btn)
    {
        buttonnumber = Convert.ToInt32(Regex.Replace(btn.name, @"\D", ""));
        if(buttonnumber >= button_range)
        {
            btn.SetActive(false);
        }
        Debug.Log(buttonnumber);
        Debug.Log(random_array[0]+"여기까찌");
        return random_array[buttonnumber];
    }
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuitClass.GameQuit();
        }

    }
}
