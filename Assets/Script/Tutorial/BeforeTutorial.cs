using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeforeTutorial : MonoBehaviour
{
    
    public TextAsset jsonData;
    public Text script;

    int JsonIndex, StrIndex;
    string JsonStr;
    string str = "";

    // Use this for initialization
    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
    }

    void Start()
    {
        
        script.text = "...";
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            str = "";
            StopCoroutine("Printing");

            LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

            if (JsonIndex == getData["Script"].Count)
            {
                SceneManager.LoadScene("Tutorial");
                return;
            }

            JsonStr = getData["Script"][JsonIndex++]["string"].ToString();

            StartCoroutine("Printing");
           
            
        }

    }

    IEnumerator Printing()
    {
        for (int i = 0; i < JsonStr.Length; i++)
        {
            str += JsonStr[i];
            script.text = str;
            yield return new WaitForSeconds(0.12f);
        }
    }

    public void ToTitle()
    {
        
    }



}
