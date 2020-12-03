using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floating_text : MonoBehaviour {

    public float moveSpeed;
    public float destroyTime;

    public Text text;

    private Vector3 vertor;

	// Use this for initialization
	void Start () {
        vertor = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        vertor.Set(text.transform.position.x, text.transform.position.y + (moveSpeed * Time.deltaTime), text.transform.position.z);
        text.transform.position = vertor;

        destroyTime = destroyTime - Time.deltaTime;

        if(destroyTime <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
