using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    public GameController gameController;
    public GameObject TopBox;
    public float speed;
    public Sprite[] Samples;
    public Image Character_Image;

    bool CharacterTrigger;
    Vector2 Origin;

    // Use this for initialization
    void Start ()
    {
        Origin = transform.position;
        transform.Translate(-236, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(CharacterTrigger)
        {
            if(transform.position.x >=Origin.x)
            {
                CharacterTrigger = false;
                TopBox.SendMessage("QuestionTriggerOn");
                gameController.SendMessage("ButtonsOn");

                return;
            }
            
            transform.Translate(100 * speed * Time.deltaTime, 0, 0);

        }
	}

    public void CharacterTriggerOn()
    {
        CharacterTrigger = true;
    }

    public void SetCharacter(string cha)
    {
        //각 캐릭터별로 배열 인덱스 지정

        transform.Translate(-236, 0, 0); //캐릭터의 위치 조정

        if (cha=="Angry")
        {
            Character_Image.gameObject.GetComponent<Image>().sprite = Samples[0];

        }
        else if(cha== "Politics")
        {
            Character_Image.gameObject.GetComponent<Image>().sprite = Samples[1];
        }
        else if(cha== "Complain")
        {
            Character_Image.gameObject.GetComponent<Image>().sprite = Samples[2];
        }
        else if(cha== "ToughWoman")
        {
            Character_Image.gameObject.GetComponent<Image>().sprite = Samples[3];
        }
        else if(cha=="Feeling")
        {
            Character_Image.gameObject.GetComponent<Image>().sprite = Samples[4];
        }

    }

}
