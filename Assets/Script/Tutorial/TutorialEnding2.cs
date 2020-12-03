using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialEnding2 : MonoBehaviour {

    public TextAsset jsonData;
    public Text script;

    bool isChange;
    int JsonIndex, StrIndex;
    static string JsonStr;
    string str = "";

    // Use this for initialization
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isChange)
        {
            str = "";
            StopCoroutine("Printing");

            LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

            if (JsonIndex > getData["Script2"].Count)
            {

                SceneManager.LoadScene("Main");
                return;
            }

            if (JsonIndex == getData["Script2"].Count)
            {
                JsonIndex++;
                return;
            }

            JsonStr = getData["Script2"][JsonIndex++]["String"].ToString();

            StartCoroutine("Printing");


        }

    }

    IEnumerator Printing()
    {
        for (int i = 0; i < JsonStr.Length; i++)
        {
            str += JsonStr[i];
            script.text = str;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void IsChange()
    {
        isChange = true;
    }

}
