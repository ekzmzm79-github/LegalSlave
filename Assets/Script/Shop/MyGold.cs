using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;


public class MyGold : MonoBehaviour {
    public int gold;
    public Text money;
    public int NowGold;
    public int incGold;
    bool GoldTrigger;
	// Use this for initialization
	void Start () {
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");
        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);
        gold = save.Gold;
        money.GetComponent<Text>().text = gold.ToString();
        GoldTrigger = false;
    }

    // Update is called once per frame
    void Update() {
        if(GoldTrigger)
        {
            GoldTrigger = false;
            StartCoroutine("IncGold", incGold);
        }
    }

    public void Click()
    {
        string JsonStr1 = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");
        SaveData save1 = JsonMapper.ToObject<SaveData>(JsonStr1);
        incGold = System.Convert.ToInt32(save1.Gold);//7000
        NowGold = System.Convert.ToInt32(money.GetComponent<Text>().text);//10000
        incGold = incGold - NowGold;//-3000
        Debug.Log(incGold);
        //  money.GetComponent<Text>().text = gold.ToString();
        if (incGold != 0)
        { GoldTrigger = true; }
    }

    IEnumerator IncGold(int incgold)
    {

        if (incgold < 0)
        {
            while (incgold < 0)
            {
                NowGold -= 100;
                incgold += 100;
                money.text = NowGold.ToString();
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (incgold > 0)
            {
                NowGold += 100;
                incgold -= 100;
                money.text = NowGold.ToString();
                yield return new WaitForSeconds(0.01f);
            }

        }
    }
}
