using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using LitJson;


public class Score_window_3 : MonoBehaviour {

    public void OnClick()
    {
        Sound_Manager_4.instance.play_sound(4);

        //밑의 소스 풀면 계속 반복
        //SceneManager.LoadScene("Before_Karaoke");

        //상여금 받아와서 저장합니다
        int money = Game_Manager_4.instance.get_score();
        Debug.Log(money);
        //그뒤로 돈저장이랑 씬 바꾸기 로드 하시면 됩니다.


        //변화된 골드의 세이브
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);
        save.Gold += money;
        save.PrintCount += 1; //프린트 카운터를 증가시킨다.
        save.EventIndex += 1;

        string PutData = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);

        GoldHistoryForm newHistory = new GoldHistoryForm();

        //newHistory에 증감되는 골드와 어떤 이벤트로 인해서 변화되는지를 기록한다.
        newHistory.day = save.Day;
        newHistory.gold = money;
        newHistory.type = "상여금";
        GoldHistoryList.GoldList.Add(newHistory);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/GoldHistory.txt", JsonMapper.ToJson(GoldHistoryList.GoldList));
        //
        GameLoadClass.GameLoadTrigger = true;

        SceneManager.LoadScene("Main");

    }


}
