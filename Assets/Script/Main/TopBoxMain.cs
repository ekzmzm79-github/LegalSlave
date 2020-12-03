using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopBoxMain : MonoBehaviour {

    public GameController gameController;
    public Text script;
    public ButtonSelectMain[] buttons = new ButtonSelectMain[3];

    string JsonStr;
    string str = "";

    string questionstring;
    string[] resultstring = new string[3];

    bool TextEffect;
    int TextEffectCheck;

    float TextSpeed = 0.02f;
    bool QuestionTrigger, ResultTrigger, EventChange, IsEnd, PrintingTrigger;
    int select_num;

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (PrintingTrigger && Input.GetButtonDown("Fire1"))
        {
            TextSpeed = 0;
            PrintingTrigger = false;
        }


        //이벤트의 시작은 질문
        if (QuestionTrigger)
        {
            QuestionTrigger = false;
            TextSpeed = 0.02f;

            str = "";
            StopCoroutine("Printing");

            JsonStr = questionstring;

            StartCoroutine("Printing");

            for(int i=0; i<3; i++)
            {
                buttons[i].SendMessage("SelectChangeOn");
            }
            

        }

        else if(ResultTrigger)
        {
            ResultTrigger = false;

            str = "";
            StopCoroutine("Printing");

            JsonStr = resultstring[select_num];

            StartCoroutine("Printing");

            gameController.SendMessage("EventChangeTriggerOn");

        }

    }

    public void QuestionTriggerOn()
    {
        QuestionTrigger = true;
    }

    public void ResultTriggerOn(int num)
    {
        select_num = num;
        ResultTrigger = true;

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

    IEnumerator Printing()
    {
        PrintingTrigger = true;

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
                yield return new WaitForSeconds(TextSpeed);
            }



        }
    }




}
