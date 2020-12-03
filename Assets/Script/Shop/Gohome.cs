using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Gohome : MonoBehaviour {



    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void gohome()
    {

        GameController gameController = new GameController();

        gameController.DayEnd();

    }

}
