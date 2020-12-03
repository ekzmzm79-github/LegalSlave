using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Group_settings : MonoBehaviour {

    public Text Group_1, Group_2;

    //5개의 부서이름
    string[] department = { "회계", "인사", "영업", "기획" };
    //5개의 회사이름
    string[] company_name = { "가람", "가온", "나루", "누리" };

    /*//9개의 부서이름
    string[] department = {"회계","인사","영업","기획","무역","경영","총무","연구","홍보"};
    //15개의 회사이름
    string[] company_name = { "가람", "가온", "겨슬", "나래", "나루", "누리", "다솜", "도래", "두루", "리라", "마루", "모아", "미나", "보람", "보슬" };*/
    // Use this for initialization,
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string get_g1()
    {
        string str = Group_1.text;
        string str_1 = str.Substring(0, 2) + str.Substring(6, 2);
        //Debug.Log(str_1);
        return str_1;
    }

    public string get_g2()
    {
        string str = Group_2.text;
        string str_1 = str.Substring(0, 2) + str.Substring(6, 2);
        //Debug.Log(str_1);
        return str_1;
    }

    public void Text_setting()
    {
        int num1, num2;
        num1 = Random.Range(13, 16);
        num2 = Random.Range(13, 16);

        while (true)
        {
            if(num1!= num2)
            {
                break;
            }
            else
            {
                num2 = Random.Range(13, 16);
            }
        }

        if (num1 > num2)
        {
            int temp;
            temp = num1;
            num1 = num2;
            num2 = temp;
        }

        int num3, num4;
        num3 = Random.Range(0, 4);
        num4 = Random.Range(0, 4);

        Group_1.text = num1+"년이하 " + department[num3] + "부";
        Group_2.text = num2+"년이상 " + company_name[num4] + "회사";
    }
}
