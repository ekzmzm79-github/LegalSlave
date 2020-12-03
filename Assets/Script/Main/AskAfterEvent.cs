using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class AskAfterEvent : MonoBehaviour {

    void Awake()
    {
        Screen.SetResolution(600, 1024, true);
    }

    // Use this for initialization
    void Start ()
    {
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        save.EventIndex = 4;

        string PutData = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuitClass.GameQuit();
        }
    }


    public void ToShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void ToNightEvent()
    {
        NightEventClass.NightEventTrigger = true;

        SceneManager.LoadScene("Main");
    }
}
