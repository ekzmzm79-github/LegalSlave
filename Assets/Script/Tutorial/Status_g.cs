using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status_g : MonoBehaviour {

    const int StatusMany = 4;
    int temp = 100;

    public GameObject[] status;
    Slider[] sliders = new Slider[4];

    void Awake()
    {
        for (int i = 0; i < StatusMany; i++)
        {
            sliders[i] = status[i].GetComponentInChildren<Slider>();
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //save 데이터 세팅
    void SetHeart(float value)
    {
        sliders[0].value = value;
    }

    void SetColleague(float value)
    {
        sliders[1].value = value;
    }

    void SetWorkAB(float value)
    {
        sliders[2].value = value;
    }

    void SetStress(float value)
    {
        sliders[3].value = value;
    }


    
    IEnumerator coroutine;

    //파라미터의 실질적인 증감
    void IncHeart(float value)
    {
        coroutine = Control(value, 0);

        StartCoroutine(coroutine);
    }

    void IncColleague(float value)
    {
        
        coroutine = Control(value, 1);

        StartCoroutine(coroutine);

    }

    void IncWorkAB(float value)
    {

        coroutine = Control(value, 2);

        StartCoroutine(coroutine);
    }

    void IncStress(float value)
    {

        coroutine = Control(value, 3);

        StartCoroutine(coroutine);
    }

    IEnumerator Control(float value, int index)
    {

        float deci = value % 1;
        int integer = (int)value;

        if(value<0)
        {
            while (value < 0)
            {
                sliders[index].value -= 1.0f;
                value++;
                yield return new WaitForSeconds(0.01f);
            }

            sliders[index].value -= deci;
        }
        else
        {
            while(value>0)
            {
                sliders[index].value += 1.0f;
                value--;
                yield return new WaitForSeconds(0.01f);
            }

            sliders[index].value += deci;
        }
        

       
    }

}
