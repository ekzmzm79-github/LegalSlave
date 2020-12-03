using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndingList : MonoBehaviour {

    public Canvas SorryBack;
	// Use this for initialization
	void Start () {
		
	}
    public void EndingListClick()
    {
        if (Social.localUser.id == null)
        {
            SorryBack.sortingOrder = 10;
        }
        else
        {
            SceneManager.LoadScene("EndingCollect");
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
