using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class ItemText : MonoBehaviour{

    public Button ItemTextBox;
    public Text Itemtext;
    public TextAsset jsonData;

    bool ItemBoxTrigger;
    string temp_string;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    { 
        if(ItemBoxTrigger && Input.GetButtonDown("Fire1"))
        {
            ItemBoxTrigger = false;

            ItemTextBox.GetComponent<Image>().enabled = false;
            ItemTextBox.GetComponent<Button>().enabled = false;
            Itemtext.GetComponent<Text>().enabled = false;

        }
        
	}

    public void ItemClick()
    {

        ItemTextBox.GetComponent<Image>().enabled = true;
        ItemTextBox.GetComponent<Button>().enabled = true;

        Itemtext.text = temp_string;
        Itemtext.GetComponent<Text>().enabled = true;


        ItemBoxTrigger = true;

    }


    public void LoadItemText(int ItemID)
    {
        JsonData ItemString = JsonMapper.ToObject(jsonData.text);

        temp_string = ItemString[ItemID]["Dis"].ToString();
    }

}
