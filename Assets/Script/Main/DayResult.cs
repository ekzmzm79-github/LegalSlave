using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;
using System.IO;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class DayResult : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
    }

    // 이 스크립트는 정산화면에서 소리와 화면에 글자출력을 담당합니다 Ctrl+F로 "(X)"를 검색해서 볼것

    public TextAsset Save;

    public Text BeforeMoney;
    public Text BeforeMoneyInt;
    public Text Total;
    public Text TotalInt;
    public Text Day;

    public Text[] P = new Text[9];
    public Text[] M = new Text[9];

    int BeforeGoldCalculate = 0;
    int PrintCount;
    int JsonIndex = -1, PMIndex = 0;
    AudioSource PrintSound;

    string JsonPrint, JsonInt;
    string str = "";
    int TotalCalculate, TotalSum, DayInt;

    public bool ResultTrigger;

    void Start()
    {
        ResultTrigger = true;

        if (ResultTrigger)
        {
            //
            string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

            SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

            save.EventIndex = 10;

            
            //

            JsonData SaveData = JsonMapper.ToObject(Save.text);
            str = SaveData["Day"].ToString();
            DayInt = save.Day; // 플레이 날짜를 대입해야 합니다!! (O)

            str = SaveData["PrintCount"].ToString();
            PrintCount = save.PrintCount; 

            PrintCount += 2;

            MoneyCalculate();

            str = SaveData["Gold"].ToString();
            TotalSum = save.Gold - BeforeGoldCalculate; // 현재소지금에서 이전 모든 내역들을 빼서 이전 소지금이 되었습니다

            if (Social.localUser.id.Length != 0)
            {
                if (TotalSum >= 100000)
                {
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement, 100f, null);
                }
            }

            for (int t = 0; t <PrintCount; t++)
            {
                Invoke("PManager", (float)t / 2);
            }
            PrintSound = GetComponent<AudioSource>();
            Day.text = DayInt + "일";


            string PutData = JsonMapper.ToJson(save);

            File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuitClass.GameQuit();
        }


        if (Input.GetButtonDown("Fire1"))//&&(JsonIndex!=Printcount-1)
        {
            CancelInvoke();
            str = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/GoldHistory.txt");
            JsonData HistoryData = JsonMapper.ToObject<JsonData>(str);
            for (; JsonIndex < PrintCount - 2; JsonIndex++)
            {
                JsonPrint = HistoryData[JsonIndex]["type"].ToString();
                JsonInt = HistoryData[JsonIndex]["gold"].ToString();
                StartCoroutine("Printing");
            }
            StartCoroutine("TotalPrinting");
        }
    }

    void MoneyCalculate()
    {
        str = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/GoldHistory.txt");
        JsonData HistoryData = JsonMapper.ToObject<JsonData>(str);
        for (int t = 0; t < PrintCount - 2; t++) // 여기 조건문 임시로 했음 내역갯수가 조건문에 들어가야 됨 변경시 O로 변경 (X)
        {
            str = HistoryData[t]["gold"].ToString();
            Debug.Log(str);
            BeforeGoldCalculate = BeforeGoldCalculate + int.Parse(str);
        }
    }

    void PManager()
    {
        if (JsonIndex == -1)
        {
            JsonInt = TotalSum.ToString();
            str = "이전 소지금";
            StartCoroutine("BeforeMoneyStatePrinting");
        }
        else if (JsonIndex < PrintCount - 2) // 내역 갯수가 들어가야 됩니다. (X) 
        {
            str = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/GoldHistory.txt");
            JsonData HistoryData = JsonMapper.ToObject<JsonData>(str);
            JsonPrint = HistoryData[JsonIndex]["type"].ToString();
            JsonInt = HistoryData[JsonIndex]["gold"].ToString();

            StartCoroutine("Printing");
        }
        else // 계산방식에 따라 합산할 방법이 달라짐
        {
            StartCoroutine("TotalPrinting");
        }
        JsonIndex++;
    }

    IEnumerator BeforeMoneyStatePrinting()
    {
        if (TotalSum < 0)
        {
            str = "<color=red>" + str + "</color>";
        }
        BeforeMoney.text = str;
        str = JsonInt;
        if (TotalSum < 0)
        {
            str = "<color=red>" + TotalSum + "</color>";
        }
        BeforeMoneyInt.text = str;
        PrintSound.Play();
        yield return new WaitForSeconds(0f);
    }

    IEnumerator Printing()
    {
        str = JsonInt;
        TotalCalculate = int.Parse(str);
        TotalSum = TotalSum + TotalCalculate;
        str = JsonPrint;
        if (TotalCalculate < 0)
        {
            str = "<color=red>" + str + "</color>";
        }
        P[PMIndex].text = str;
        str = JsonInt;
        if (TotalCalculate < 0)
        {
            str = "<color=red>" + TotalCalculate + "</color>";
        }
        M[PMIndex].text = str;
        PrintSound.Play();
        PMIndex++;
        yield return new WaitForSeconds(0f);
    }

    IEnumerator TotalPrinting()
    {
        str = "합계";
        if (TotalSum < 0)
        {
            str = "<color=red>" + str + "</color>";
        }
        Total.text = str;
        str = TotalSum.ToString();
        if (TotalSum < 0)
        {
            str = "<color=red>" + TotalSum + "</color>";
        }
        TotalInt.text = str;
        PrintSound.Play();
        yield return new WaitForSeconds(0f);
    }
}
