using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class Setting : MonoBehaviour
{
    public GameObject SoundOnOff;
    public Sprite SoundOn, SoundOff;

    void Start()
    {
        if (AudioListener.pause == false)
        {
            SoundOnOff.GetComponent<Image>().sprite = SoundOn;
        }
        else
        {
            SoundOnOff.GetComponent<Image>().sprite = SoundOff;
        }
    }

    void Update()
    {

    }

    public void SoundONOff()
    {
        if (AudioListener.pause == false)
        {
            AudioListener.pause = true;
            SoundOnOff.GetComponent<Image>().sprite = SoundOff;
        }
        else
        {
            AudioListener.pause = false;
            SoundOnOff.GetComponent<Image>().sprite = SoundOn;
        }
    }

    public void Helps()
    {
        SceneManager.LoadScene("Helps");
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
