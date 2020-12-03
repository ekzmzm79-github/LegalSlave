using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopBox : MonoBehaviour {

    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
    }

    public GameObject BottomBox;
    public GameObject Blinder;
    public TextAsset jsonData;
    public Text script;
    int JsonIndex, StrIndex;

    string JsonStr;
    string str = "", questionstring;
    string[] resultstring = new string[3];

    bool TextEffect;
    int TextEffectCheck;

    public bool IsReady, IsEnd, IsEnd2, FadeOut;

    // Use this for initialization
    void Start()
    {
        FadeOut = false;
        IsReady = false;
        IsEnd = false;
        IsEnd2 = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(FadeOut)
        {
            FadeOut = false;

            Blinder.SendMessage("TriggerOn");
            return;
        }

        if(IsEnd2)
        {
            IsEnd2 = false;

            str = "";
            StopCoroutine("Printing");

            JsonStr = "... 여기까지 하면 될 것 같군요. 합격 여부는 개별적으로 문자를 통해 알려드리겠습니다. 수고하셨습니다.";

            StartCoroutine("Printing");
            StartCoroutine("FadeOutDelay");

            return;
        }

        if (IsEnd)
        {
            script.text = "";
            IsEnd = false;

            StartCoroutine("IsEnd2Delay");

            return;
        }


        if (!IsReady)
        {
            IsReady = true;

            

            str = "";
            StopCoroutine("Printing");

            LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

            
            if (JsonIndex >= getData["TopBox"].Count)
            {
                
                
                return;
            }
            
            JsonStr = getData["TopBox"][JsonIndex]["String"].ToString();

            StartCoroutine("Printing");
            
        }

        

        




    }
    public void SetQuestion(string question)
    {

        questionstring = "";
        questionstring = question;
    }

    public void SetResult(StrSet send)
    {
        resultstring[send.num] = send.str;
    }


    public void Switching()
    {
        JsonIndex++;
        IsReady = false;
    }

    public void End()
    {
        IsEnd = true;
    }

    IEnumerator IsEnd2Delay()
    {

        yield return new WaitForSeconds(2.0f);

        IsEnd2 = true;
    }

    IEnumerator FadeOutDelay()
    {

        yield return new WaitForSeconds(3.0f);

        FadeOut = true;
    }


    IEnumerator Printing()
    {
        for (int i = 0; i < JsonStr.Length; i++)
        {
            if ((i + 1) % 31 == 0)
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
                yield return new WaitForSeconds(0.03f);
            }



        }
    }




}
