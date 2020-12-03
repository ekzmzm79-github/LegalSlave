using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Text.RegularExpressions;


public class EndingCollect : MonoBehaviour {

    public GameObject endingimageobj;
    public Sprite[] ei = new Sprite[11];
    public Canvas zoom;
    public GameObject zoom_ei;
    private bool[] ending = new bool[10];


    int endingnum;
 /*   public class EndingData
    {
        public bool[] ending = new bool[10];


        public EndingData(bool[] ending)
        {
            for (int i = 0; i < 10; i++)
            {
                this.ending[i] = ending[i];
            }
        }

        public EndingData()
        {

        }
    }
    */
    IEnumerator EndingLoad_1()
    {
        
        //string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");
        //SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);
        //세이브데이터 다 읽어옴 시작
        WWWForm form2 = new WWWForm();
        form2.AddField("IdPost", Social.localUser.id);
        WWW www2 = new WWW("http://dlwlgh301.cafe24.com/wp/endingoutput.php", form2);
        yield return www2;
   //     EndingData Ed = new EndingData();
        string load = www2.text;

        Debug.Log(load);
        LitJson.JsonData getDa = LitJson.JsonMapper.ToObject(load);
        Debug.Log("디버그로그"+getDa["Ending0"].ToString());

        bool endingg0 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending0"].ToString()));
        ending[0] = endingg0;
        bool endingg1 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending1"].ToString()));
        ending[1] = endingg1;
        bool endingg2 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending2"].ToString()));
        ending[2] = endingg2;
        bool endingg3 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending3"].ToString()));
        ending[3] = endingg3;
        bool endingg4 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending4"].ToString()));
        ending[4] = endingg4;
        bool endingg5 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending5"].ToString()));
        ending[5] = endingg5;
        bool endingg6 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending6"].ToString()));
        ending[6] = endingg6;
        bool endingg7 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending7"].ToString()));
        ending[7] = endingg7;
        bool endingg8 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending8"].ToString()));
        ending[8] = endingg8;
        bool endingg9 = Convert.ToBoolean(Convert.ToInt16(getDa["Ending9"].ToString()));
        ending[9] = endingg9;


        endingnum = Convert.ToInt32(Regex.Replace(endingimageobj.name, @"\D", ""));
        if (ending[endingnum] == true)
        {
            endingimageobj.GetComponent<Image>().sprite = ei[endingnum];
        }
        else
        {
            endingimageobj.GetComponent<Image>().sprite = ei[10];
        }
    }
    // Use this for initialization
    void Start () {
        StartCoroutine(EndingLoad_1());
	}
	
    public void EndingClick()
    {
        if (ending[endingnum] == false)
        {
            zoom_ei.GetComponent<Image>().sprite = ei[10];
            zoom.sortingOrder = 10;
        }


 
        else
        {
            zoom_ei.GetComponent<Image>().sprite = ei[endingnum];
            zoom.sortingOrder = 10;
        }

    }

    public void BackbuttonClick()
    {
        zoom.sortingOrder = 1;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
