using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BottomBox : MonoBehaviour {

    public Text Bottom_ButtonScript;
    public GameObject Parameter;
    public GameObject TopBox;
    public GameObject[] Button;
    public TextAsset jsonData;
    public Text script;
    int JsonIndex, StrIndex;
    string JsonStr, JsonValue;
    string str = "";

    string[] selectstring = new string[3];

    bool TextEffect, IsReady, ParameterControl;
    int TextEffectCheck;

    int select_num;

    public bool IsEnd;

    // Use this for initialization
    void Start ()
    {
        IsReady = false;
        IsEnd = false;


    }
	
	// Update is called once per frame
	void Update ()
    {
        if (IsEnd)
        {
            IsEnd = false;

            Debug.Log("end");

            str = "";
            StopCoroutine("Printing");

            JsonStr = "(...그 밖에도 꽤 많은 질문이 오고 갔다.)";
            StartCoroutine("Printing");
            
            TopBox.SendMessage("End");
            SelectNum.Select = -1;

            return;
        }


        select_num = SelectNum.Select;

        if (select_num != -1 && !IsReady && !IsEnd)
        {

            str = "";
            StopCoroutine("Printing");

            LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

            /*
            if (JsonIndex >= getData["BottomBox"].Count)
            {
                Debug.Log("end");
                IsEnd = true;
                
            }
            */

            Bottom_ButtonScript.text= getData["Selection"][JsonIndex]["String" + select_num.ToString()].ToString();
            JsonStr = getData["BottomBox"][JsonIndex]["String" + select_num.ToString()].ToString();

            StartCoroutine("Printing");
        }

        if (IsReady && !IsEnd)
        {
            
            IsReady = false;

            StopCoroutine("Printing");

            JsonIndex++;
            TopBox.SendMessage("Switching");

            Bottom_ButtonScript.text = "";
            str = "";
            script.text = "";

            for (int i = 0; i < 3; i++)
            {
                Button[i].SendMessage("Change");
                Button[i].GetComponent<Button>().enabled = true;
                Button[i].GetComponent<ButtonSelect>().enabled = true;
            }

        }

        if (ParameterControl)
        {
            ParameterControl = false;

            LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

            JsonValue = getData["Value" + select_num.ToString()][JsonIndex]["Heart"].ToString();
            Parameter.SendMessage("IncHeart", int.Parse(JsonValue));

            JsonValue = getData["Value" + select_num.ToString()][JsonIndex]["Colleague"].ToString();
            Parameter.SendMessage("IncColleague", int.Parse(JsonValue));

            JsonValue = getData["Value" + select_num.ToString()][JsonIndex]["WorkAB"].ToString();
            Parameter.SendMessage("IncWorkAB", int.Parse(JsonValue));

            JsonValue = getData["Value" + select_num.ToString()][JsonIndex]["Stress"].ToString();
            Parameter.SendMessage("IncStress", int.Parse(JsonValue));
        }

    }


    public void SetSelectString(StrSet send)
    {
        selectstring[send.num] = send.str;

    }

    public void Ready()
    {
        StopCoroutine("Printing");
        str = "";
        script.text = "";
        StartCoroutine("Printing");

        ParameterControl = true;

        StartCoroutine("ReadyDelay");

        

        //Debug.Log("Ready In");


    }

    public void End()
    {
        StartCoroutine("EndDelay");
    }
    IEnumerator EndDelay()
    {

        yield return new WaitForSeconds(0.5f);

        IsEnd = true;

    }


    IEnumerator ReadyDelay()
    {

        for (int i = 0; i < 3; i++)
        {
            Button[i].GetComponent<Button>().enabled = false;
            Button[i].GetComponent<ButtonSelect>().enabled = false;
        }
        

        yield return new WaitForSeconds(3.5f);

        IsReady = true;
        
    }

    IEnumerator Printing()
    {
        script.text = "";

        for (int i = 0; i < JsonStr.Length; i++)
        {
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
                yield return new WaitForSeconds(0.03f);
            }



        }
    }

}
