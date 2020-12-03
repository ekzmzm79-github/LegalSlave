using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper_settings : MonoBehaviour {

    public Transform information;

    public Text info;
    Vector3 pos;
    //5개의 부서이름
    string[] department = { "회계", "인사", "영업", "기획","",""};
    //5개의 회사이름
    string[] company_name = { "가람", "가온", "나루", "누리","",""};

    public string get_info()
    {
        string str = info.text;
        string str1 = str.Substring(8,2) + str.Substring(12,2) + str.Substring(0, 2);
        return str1;
    }

    public void change_text(string str)
    {
        info.text = str;
    }

    public Vector3 get_positon()
    {
        return pos;
    }

    public void chposition(Vector3 po)
    {
        information.position = po;
    }

    public void init_info()
    {
        string str1 = GameManager_2.instance.GetComponent<Group_settings>().get_g1();
        string str2 = GameManager_2.instance.GetComponent<Group_settings>().get_g2();
        
        for(int i = 0; i <2;i++)
        {
            department[4 + i]= str1.Substring(2, 2);
            company_name[4 + i] = str2.Substring(2, 2);
        }

        int num1;
        num1 = Random.Range(10, 19);
        int num2, num3;
        num2 = Random.Range(0, 6);
        num3 = Random.Range(0, 6);

        string str;
        if (num2 % 2 == 0)
        {
            str = "수신\n";
        }
        else
        {
            str = "발신\n";
        }

        change_text(company_name[num3] + "회사\n" + str + num1 + "년\n" + department[num2] + "부\n");
    }

    // Use this for initialization
    void Start()
    {
        pos = information.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
