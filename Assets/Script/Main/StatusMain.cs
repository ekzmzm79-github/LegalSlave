using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class StatusMain : MonoBehaviour {

    const int StatusMany = 4;

    public GameController gameController;
    public GameObject[] status;
    Slider[] sliders = new Slider[4];
    public Image[] signs = new Image[4];
    double[] heart = new double[4];
    double[] colleague = new double[4];
    double[] workAB = new double[4];
    double[] stress = new double[4];

    bool StatusTrigger, SignTrigger;
    int select_num, sign_num;

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < StatusMany; i++)
        {
            sliders[i] = status[i].GetComponentInChildren<Slider>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(SignTrigger)
        {
            SignTrigger = false;

            if (heart[sign_num] != 0)
                signs[0].color = new Color(255, 255, 255, 255);
            else
                signs[0].color = new Color(255, 255, 255, 0);

            if (colleague[sign_num] != 0)
                signs[1].color = new Color(255, 255, 255, 255);
            else
                signs[1].color = new Color(255, 255, 255, 0);

            if (workAB[sign_num] != 0)
                signs[2].color = new Color(255, 255, 255, 255);
            else
                signs[2].color = new Color(255, 255, 255, 0);

            if (stress[sign_num] != 0)
                signs[3].color = new Color(255, 255, 255, 255);
            else
                signs[3].color = new Color(255, 255, 255, 0);

        }


		if(StatusTrigger)
        {
            StatusTrigger = false;

            //표시등 모두 비표시
            signs[0].color = new Color(255, 255, 255, 0);
            signs[1].color = new Color(255, 255, 255, 0);
            signs[2].color = new Color(255, 255, 255, 0);
            signs[3].color = new Color(255, 255, 255, 0);


            //선택지 선택에 따른 파라미터의 변화
            IncHeart(heart[select_num]);
            IncColleague(colleague[select_num]);
            IncWorkAB(workAB[select_num]);
            IncStress(stress[select_num]);

            //하루 버프로 인한 파라미터의 변화
            IncHeart(DayBuffInfo.Heart);
            IncColleague(DayBuffInfo.Colleague);
            IncWorkAB(DayBuffInfo.WorkAB);
            IncStress(DayBuffInfo.Stress);

            /*
            Debug.Log("하트"+DayBuffInfo.Heart);
            Debug.Log("동료심" + DayBuffInfo.Colleague);
            Debug.Log("업무능력" + DayBuffInfo.WorkAB);
            Debug.Log("스트레스" + DayBuffInfo.Stress);
            */

            //파라미터 세이브 파일에 저장
            SaveParameters();

            

        }
	}

    //변화 트리거
    void StatusTriggerOn(int num)
    {
        select_num = num;

        StatusTrigger = true;
    }

    void SignTriggerOn(int num)
    {
        sign_num = num;

        SignTrigger = true;
    }

    

    void SaveParameters()
    {
        string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

        SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

        save.Parameters[0] += heart[select_num];
        save.Parameters[1] += colleague[select_num];
        save.Parameters[2] += workAB[select_num];
        save.Parameters[3] += stress[select_num];

        string PutData = JsonMapper.ToJson(save);

        File.WriteAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt", PutData);


    }

    void CheckParameters()
    {
        for (int i = 0; i < StatusMany; i++)
        {
            string JsonStr = File.ReadAllText(DataPathStringClass.DataPathString() + "/Save/SaveData.txt");

            SaveData save = JsonMapper.ToObject<SaveData>(JsonStr);

            if (save.Parameters[i] >= 100 || save.Parameters[i] <= 0)
            {
                Debug.Log("파라미터 체크걸림");

                if (save.Parameters[0] >= 100)
                {
                    EndImageClass.SelectEnd = 0;
                }
                else if (save.Parameters[0] <= 0)
                {
                    EndImageClass.SelectEnd = 1;
                }

                else if (save.Parameters[1] >= 100)
                {
                    EndImageClass.SelectEnd = 2;
                }
                else if (save.Parameters[1] <= 0)
                {
                    EndImageClass.SelectEnd = 3;
                }

                else if (save.Parameters[2] >= 100)
                {
                    EndImageClass.SelectEnd = 4;
                }
                else if (save.Parameters[2] <= 0)
                {
                    EndImageClass.SelectEnd = 5;
                }

                else if (save.Parameters[3] >= 100)
                {
                    EndImageClass.SelectEnd = 6;
                }
                else if (save.Parameters[3] <= 0)
                {
                    EndImageClass.SelectEnd = 7;
                }

                gameController.SendMessage("GameOver");
                break;
            }

            

        }
    }


    void SetHeart(StrSet send)
    {
        heart[send.num] = double.Parse(send.str);
    }

    void SetColleague(StrSet send)
    {
        colleague[send.num] = double.Parse(send.str);
    }

    void SetWorkAB(StrSet send)
    {
        workAB[send.num] = double.Parse(send.str);
    }

    void SetStress(StrSet send)
    {
        stress[send.num] = double.Parse(send.str);
    }


    //save 데이터 로딩해서 세팅

    void LoadHeart(double value)
    {
        sliders[0].value =(float) value;
    }

    void LoadColleague(double value)
    {
        sliders[1].value = (float)value;
    }

    void LoadWorkAB(double value)
    {
        sliders[2].value = (float)value;
    }

    void LoadStress(double value)
    {
        sliders[3].value = (float)value;
    }


    
    IEnumerator coroutine;

    //파라미터의 실질적인 증감
    void IncHeart(double value)
    {
        coroutine = Control(value, 0);

        StartCoroutine(coroutine);
    }

    void IncColleague(double value)
    {
        
        coroutine = Control(value, 1);

        StartCoroutine(coroutine);

    }

    void IncWorkAB(double value)
    {

        coroutine = Control(value, 2);

        StartCoroutine(coroutine);
    }

    void IncStress(double value)
    {

        coroutine = Control(value, 3);

        StartCoroutine(coroutine);
    }

    IEnumerator Control(double value, int index)
    {

        float deci = (float)value % 1;
        int integer = (int)value;

        if(value<0)
        {
            while (value < 0)
            {
                sliders[index].value -= 1.0f;
                value++;
                yield return new WaitForSeconds(0.1f);
            }

            sliders[index].value -= deci;
        }
        else
        {
            while(value>0)
            {
                sliders[index].value += 1.0f;
                value--;
                yield return new WaitForSeconds(0.1f);
            }

            sliders[index].value += deci;
        }

        //파라미터 체크
        CheckParameters();

    }

}
