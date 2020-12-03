using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject Select_button;
    int buttonnumber = 0;

    public GameObject BottomBox;

    public AudioClip[] clips;
    AudioSource audio;
    
    bool isEnter, IsEnd, TextEffect, SelectChange;
    
    int JsonIndex, Checker = 0;
    string JsonStr, str;
    public Canvas button_canvas;
    public TextAsset jsonData;
    public Text script;
    string[] select = new string[3];
    int TextEffectCheck;


    void Start()
    {
        SelectChange = true;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(IsEnd)
        {
            Select_button.GetComponent<Button>().enabled = false;
            Select_button.GetComponent<ButtonSelect>().enabled = false;
            BottomBox.SendMessage("End");

            IsEnd = false;

            return;
        }

        if (Checker != 0)
            return;

        if(SelectChange)
        {
            //Debug.Log("dqwdqw");

            SelectChange = false;

            Button btn = GetComponent<Button>(); //현재 객체를 가져온다.
            buttonnumber = Convert.ToInt32(Regex.Replace(btn.name, @"\D", ""));


            str = "";
            StopCoroutine("Printing");

            LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

            if (JsonIndex >= getData["BottomBox"].Count)
            {
                //Debug.Log("string end");
                script.text = "";
                IsEnd = true;
                Checker++;
                return;
            }


            JsonStr = getData["Selection"][JsonIndex]["String" + buttonnumber.ToString()].ToString();

            StartCoroutine("Printing");
            //script.text = JsonStr;
        }




    }

    public void SetSelect(StrSet send)
    {
        select[send.num] = send.str;

    }

    

    public void Change()
    {
        SelectChange = true;
        JsonIndex++;
    }

    public void Click()
    {
        Select_button.GetComponent<Animation>().Play("select_button");
        Button btn = GetComponent<Button>(); //현재 객체를 가져온다.
        buttonnumber = Convert.ToInt32(Regex.Replace(btn.name, @"\D", ""));
        SelectNum.Select = buttonnumber;//객체의 이름에서 숫자만 남긴걸 int변환해 현재 선택된 선택지 번호로서 삼는다.
        //Debug.Log(Regex.Replace(btn.name, @"\D", ""));

        audio.clip = clips[0];
        audio.Play();
    }

    public static Vector2 defaultposition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Button btn = GetComponent<Button>(); //현재 객체를 가져온다.
        buttonnumber = Convert.ToInt32(Regex.Replace(btn.name, @"\D", ""));
        SelectNum.Select = buttonnumber;

        defaultposition = this.transform.position;
        button_canvas.sortingOrder = 2;

        audio.clip = clips[0];
        audio.Play();

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        this.transform.position = currentPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = defaultposition;

        

        button_canvas.sortingOrder = 1;

        if(isEnter)
        {

            audio.clip = clips[1];
            audio.Play();

            BottomBox.SendMessage("Ready");
            

        }


    }
    
    IEnumerator Printing()
    {
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
                yield return new WaitForSeconds(0.07f);
            }



        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {

        if (isEnter)
            return;

        if (collision.gameObject.tag == "Character")
        {
            isEnter = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!isEnter)
            return;

        if (collision.gameObject.tag == "Character")
        {
            isEnter = false;
        }
    }


}