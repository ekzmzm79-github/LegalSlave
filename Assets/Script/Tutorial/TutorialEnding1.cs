using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialEnding1 : MonoBehaviour {

    public GameObject FadeIn;
    public TextAsset jsonData;
    public Text script;
    AudioSource mobile;

    int JsonIndex, StrIndex;
    string JsonStr;
    string str = "";


    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
    }

    // Use this for initialization
    void Start()
    {
        mobile = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            str = "";
            StopCoroutine("Printing");

            LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

            if(JsonIndex> getData["Script1"].Count)
            {
                return;
            }

            if (JsonIndex == getData["Script1"].Count - 1)
            {
                mobile.Play();
            }


            if (JsonIndex == getData["Script1"].Count)
            {
                script.text = "";
                FadeIn.SendMessage("TriggerOn");
                JsonIndex++;
                return;
            }

            JsonStr = getData["Script1"][JsonIndex++]["String"].ToString();

            StartCoroutine("Printing");


        }

    }

    IEnumerator Printing()
    {
        for (int i = 0; i < JsonStr.Length; i++)
        {
            if ((i + 1) % 16 == 0)
            {
                str += "\n";
            }
            str += JsonStr[i];
            script.text = str;
            yield return new WaitForSeconds(0.08f);
        }
    }
}
