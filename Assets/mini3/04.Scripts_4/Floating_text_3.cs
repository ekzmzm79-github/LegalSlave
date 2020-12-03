using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floating_text_3 : MonoBehaviour {

    public float moveSpeed;
    public float destroyTime;

    public Transform effect;

    private Vector3 vertor;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vertor.Set(effect.position.x, effect.position.y + (moveSpeed * Time.deltaTime), effect.position.z);
        effect.position = vertor;

        destroyTime = destroyTime - Time.deltaTime;

        if (destroyTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
