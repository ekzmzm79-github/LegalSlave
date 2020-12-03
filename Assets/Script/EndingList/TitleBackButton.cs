using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBackButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	public void TitleBack()
    {
        SceneManager.LoadScene("Title");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
