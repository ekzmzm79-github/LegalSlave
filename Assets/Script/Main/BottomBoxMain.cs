using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BottomBoxMain : MonoBehaviour {

    public GameObject Parameter;
    public GameObject TopBox;

    public Text script;
    public Text Bottom_ButtonScript;
    int JsonIndex, StrIndex;
    string JsonStr, JsonValue;
    string str = "";

    string[] selectstring = new string[3];

    bool TextEffect;
    int TextEffectCheck;

    int select_num;
    float TextSpeed = 0.04f;
    bool IsEnd, ParameterControl, ClickTrigger, DragTrigger, PrintingTrigger;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(PrintingTrigger && Input.GetButtonDown("Fire1"))
        {
            TextSpeed = 0;
            PrintingTrigger = false;
        }

        
        if (ClickTrigger)
        {
            ClickTrigger = false;
            TextSpeed = 0.04f;

            str = "";
            StopCoroutine("Printing");

            JsonStr = selectstring[select_num];

            StartCoroutine("Printing");

        }
        else if(DragTrigger)
        {
            DragTrigger = false;

            str = "";
            StopCoroutine("Printing");

            JsonStr = selectstring[select_num];

            script.text = JsonStr;
        }
   

    }


    public void SetSelectString(StrSet send)
    {

        script.text = "";
        Bottom_ButtonScript.text = "";
        selectstring[send.num] = send.str;

    }

    public void ClickTriggerOn(int num)
    {
        select_num = num;
        ClickTrigger = true;

    }

    public void DragTriggerOn(int num)
    {
        select_num = num;
        DragTrigger = true;
        //ParameterControl = true;

    }

    public void End()
    {
        IsEnd = true;
    }

    IEnumerator Printing()
    {
        PrintingTrigger = true;

        for (int i = 0; i < JsonStr.Length; i++)
        {
            if ((i + 1) % 28 == 0)
                str += '\n';

            str += JsonStr[i];

            if (JsonStr[i] == '<')
            {
                TextEffect = true;
                TextEffectCheck++;
            }

            if (JsonStr[i] == '>' || JsonStr[i] == '/')
            {
                TextEffectCheck--;

                if (TextEffectCheck == -1)
                    TextEffect = false;
            }

            if (TextEffect)
                continue;
            else
            {
                script.text = str;
                yield return new WaitForSeconds(TextSpeed);
            }



        }
    }

}
