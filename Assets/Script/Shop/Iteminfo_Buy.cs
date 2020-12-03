using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class Iteminfo_Buy : MonoBehaviour {
    public Text ItemPrice;
    public Text ItemExp;
    public Text Itemid;
    public GameObject InfoPanel;
    string price;
    string exp;
    string id;
    public static GameObject Itembutton;
    public int itemstack=0;
    public AudioSource audio;
    public AudioClip Buy;
    public Canvas InfoPopup;
    int buttonnumber;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
   //     audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void Click()
    {
        audio.Play();
        //세이브데이터 불러서 아이템가격만큼 돈깍고 저장함
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");
        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);
        

        price = ItemPrice.GetComponent<Text>().text;


        if (save.Gold >= Convert.ToInt32(price))
        {
            audio.clip = Buy;
            audio.Play();
            exp = ItemExp.GetComponent<Text>().text;
            id = Itemid.GetComponent<Text>().text;
            save.Gold -= Convert.ToInt32(price);

            for (int i = 0; i <= 4; i++)
            {
                if (i == 4)
                {
                    save.Items[itemstack] = Convert.ToInt32(id) + Convert.ToDouble(exp) * 0.1; //0번째 칸에 아이템 넣고 나가기
                    itemstack++;
                    if (itemstack > 4) itemstack = 0;
                    break;
                }
                else if (save.Items[i] == 0.0)
                {
                    save.Items[i] = Convert.ToInt32(id) + Convert.ToDouble(exp) * 0.1;
                    break;
                }
                else continue;
            }

            save.PrintCount++;
            buttonnumber = Convert.ToInt32(Regex.Replace(Itembutton.name, @"\D", ""));
            save.Shoplist[buttonnumber] = 99;
            string PutData = JsonMapper.ToJson(save);
            File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);

            GoldHistoryForm newHistory = new GoldHistoryForm();
            newHistory.day = save.Day;
            newHistory.gold = -Convert.ToInt32(price);
            newHistory.type = "아이템 구매";

            GoldHistoryList.GoldList.Add(newHistory);

            File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/GoldHistory.txt", JsonMapper.ToJson(GoldHistoryList.GoldList));

            // InfoPanel.SetActive(false);
            InfoPopup.sortingOrder = -1;

            Itembutton.SetActive(false);
        }
    }

   
}
