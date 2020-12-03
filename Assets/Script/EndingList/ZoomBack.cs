using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomBack : MonoBehaviour {

    public Canvas zoom;

	// Use this for initialization
	void Start () {
		
	}
	
    public void ZoomBackbuttonClick()
    {
        zoom.sortingOrder = -1;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
