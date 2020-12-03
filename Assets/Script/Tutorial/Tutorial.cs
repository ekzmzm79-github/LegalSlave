using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public Canvas TempletCanvas, TempletCanvas2;
    public TextAsset jsonData;
    public Text TopBox, BottomBox;
    int JsonIndex, StrIndex;
    string JsonStr;
    string str = "";

    bool TempletChangeTrigger;
    bool TextEffect;
    int TextEffectCheck;

    // Use this for initialization
    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
    }


    void Start()
    {
        BottomBox.text = "(방 안에 들어서자 면접관으로 보이는 사람이 보였다.)";
    }


    // Update is called once per frame
    void Update()
    {



        if (Input.GetButtonDown("Fire1"))
        {
            if(TempletChangeTrigger)
            {
                TempletChangeTrigger = false;
                TempletCanvas2.sortingOrder = 3;
                return;
            }


            BottomBox.text = "";
            str = "";
            StopCoroutine("Printing");

            LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

            

            if (JsonIndex == getData["TopBox"].Count)
            {
                TempletCanvas.sortingOrder = 2;
                TempletChangeTrigger = true;

                return;
            }
                

            JsonStr = getData["TopBox"][JsonIndex++]["String"].ToString();
            
            StartCoroutine("Printing");

        }

    }

    public void TempletClose()
    {

        SceneManager.LoadScene("AfterTutorial");
    }

    IEnumerator Printing()
    {
        for (int i = 0; i < JsonStr.Length; i++)
        {
            if ((i + 1) % 24 == 0)
                str += '\n';


            str += JsonStr[i];

            if (JsonStr[i] == '<')
            {
                TextEffect = true;
                TextEffectCheck++;
            }
                
            if (JsonStr[i] == '>'|| JsonStr[i]=='/')
            {
                TextEffectCheck--;

                if(TextEffectCheck==-1)
                    TextEffect = false;
            }
                
            if (TextEffect)
                continue;
            else
            {
                TopBox.text = str;
                yield return new WaitForSeconds(0.03f);
            }
            

            
        }
    }




}
