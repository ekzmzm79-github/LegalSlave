using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class ItemRandom : MonoBehaviour {

    public Image randomImage;
    public Image infoImage;
    public Sprite s0;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    public Sprite s7;
    public Sprite s8;
    public Sprite s9;
    public Sprite s10;
    public Sprite s11;
    public Sprite[] images;
    public Text ItemPrice;
    public Text ItemName;
    public string price;
    public string name;
    public string dis;
    public string exp;
    public string Itemid;
    public int pprice;
    public int nmoney;
    public int[] random_array;
    public int button_range;
    public GameObject Itembutton;
    public GameObject[] Itembtn;
    public GameObject InfoPanel;
    public Text I_ItemName;
    public Text I_ItemPrice;
    public Text I_ItemDis;
    public Text I_ItemExp;
    public Text I_Itemid;
    public int num_id;
    public int num;
    private AudioSource audio;
    public AudioClip Choose;
    public Canvas InfoPopup;
    public TextAsset jsonData;
    //  List<GoldHistoryForm> GoldList = new List<GoldHistoryForm>();

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        images = new Sprite[12];
        /*   random_array = new int[6];
           button_range = UnityEngine.Random.Range(0, 4);
           for (int i = 0; i <= 3; i++)    //숫자 4개를 뽑기위한 for문
           {
               random_array[i] = UnityEngine.Random.Range(0, 4);
               //1~10숫자중 랜덤으로 하나를 뽑아 a[0]~a[5]에 저장

               for (int j = 0; j < i; j++) //중복제거를 위한 for문 
               {
                   if (random_array[i] == random_array[j])  i--;
               }
           }
           for (int i = 0; i <= 3; i++)    //숫자 4개를 뽑기위한 for문
           {
               Debug.Log(random_array[i]); //1~10숫자중 랜덤으로 하나를 뽑아 a[0]~a[5]에 저장
           }
           */
        //Debug.Log("여기까찌");
        images[0] = s0;
        images[1] = s1;
        images[2] = s2;
        images[3] = s3;
        images[4] = s4;
        images[5] = s5;
        images[6] = s6;
        images[7] = s7;
        images[8] = s8;
        images[9] = s9;
        images[10] = s10;
        images[11] = s11;
        //
        //int num = UnityEngine.Random.Range(0, images.Length);
        num = ButtonManager.ButtonImageDecision(Itembutton);
        if (num < 99)
        {
            randomImage.sprite = images[num];
            num_id = num + 1;
            ParsingJsonItem(num);
            ItemPrice.GetComponent<Text>().text = price;
            ItemName.GetComponent<Text>().text = name;
        }
        else Itembutton.SetActive(false);

    }

    public void ParsingJsonItem(int id)
    {

        JsonData ItemData = JsonMapper.ToObject(jsonData.text);
        price = ItemData[id]["Price"].ToString();
        name = ItemData[id]["Name"].ToString();
        dis = ItemData[id]["Dis"].ToString();
        exp = ItemData[id]["Exp"].ToString();
        Itemid = ItemData[id]["ID"].ToString();
        pprice = Convert.ToInt32(price);
    }


    public void Click()
    {
        //아이템인포 보여줌.
        Iteminfo_Buy.Itembutton = Itembutton;
        infoImage.sprite = images[num];
        I_ItemPrice.GetComponent<Text>().text = price;
        I_ItemName.GetComponent<Text>().text = name;
        I_ItemDis.GetComponent<Text>().text = dis;
        I_ItemExp.GetComponent<Text>().text = exp;
        I_Itemid.GetComponent<Text>().text = Itemid;
        //   InfoPanel.SetActive(true);
        InfoPopup.sortingOrder = 10;
    //    audio.clip = Choose;
        audio.Play();
    }

    public void Buybtn_Click()
    {
        //세이브데이터 불러서 아이템가격만큼 돈깍고 저장함
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");
        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);
        save.Gold -= pprice;
        string PutData = JsonMapper.ToJson(save);
        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);

        GoldHistoryForm newHistory = new GoldHistoryForm();
        newHistory.day = save.Day;
        newHistory.gold = -pprice;
        newHistory.type = "cs23";
        InfoPanel.SetActive(false);
        Itembutton.SetActive(false);
    }

    public void Backbtn_Click()
    {
        InfoPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
    }
    public class GoldHistoryForm
    {
        public int day { get; set; }
        public int gold { get; set; }
        public string type { get; set; }
    }
}
