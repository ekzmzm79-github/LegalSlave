using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice_menu : MonoBehaviour {

    public Text txtRecipe;
    int[,] cho_mat = new int[7, 6];
    bool shot = false;
    int[] recipe = new int[6];
    string str;
    int num;

    // 0:커피  1:설탕  2:프리마  3:녹차  4:물  5:얼음
    /*
    <레시피>
	1. 밀크커피 (커피, 설탕, 프리마, 물)
	2. 블랙커피 (커피, 물)
	3. 설탕커피 (커피, 설탕, 물)
	4. 녹차(녹차티백, 물)
	5. 아이스녹차(녹차티백, 물, 얼음)
	6. 아이스블랙커피(커피, 물, 얼음)
	7.  아이스커피 (커피, 설탕, 프리마, 물, 얼음)
    */

    public int[] out_recipe()
    {
        //Debug.Log("갯수" + recipe[5]);
        return recipe;
    }

    public void change_txt(string str)
    {
        txtRecipe.text = str;
    }

    void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            recipe[i] = -1;
        }
        cho_mat[0, 5] = 4;
        cho_mat[1, 5] = 2;
        cho_mat[2, 5] = 3;
        cho_mat[3, 5] = 2;
        cho_mat[4, 5] = 3;
        cho_mat[5, 5] = 3;
        cho_mat[6, 5] = 5;

        //초기화
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                cho_mat[i, j] = -1;
            }
        }

        //레시피 1
        cho_mat[0, 0] = 0;
        cho_mat[0, 1] = 1;
        cho_mat[0, 2] = 2;
        cho_mat[0, 3] = 4;

        //레시피 2
        cho_mat[1, 0] = 0;
        cho_mat[1, 1] = 4;

        //레시피 3
        cho_mat[2, 0] = 0;
        cho_mat[2, 1] = 1;
        cho_mat[2, 2] = 4;

        //레시피 4
        cho_mat[3, 0] = 3;
        cho_mat[3, 1] = 4;

        //레시피 5
        cho_mat[4, 0] = 3;
        cho_mat[4, 1] = 4;
        cho_mat[4, 2] = 5;

        //레시피 6
        cho_mat[5, 0] = 0;
        cho_mat[5, 1] = 4;
        cho_mat[5, 2] = 5;

        //레시피 7
        cho_mat[6, 0] = 0;
        cho_mat[6, 1] = 1;
        cho_mat[6, 2] = 2;
        cho_mat[6, 3] = 4;
        cho_mat[6, 4] = 5;
    }

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void init()
    {
        num = Random.Range(0, 7);

        if (num != 6 && num != 3 && num != 4)
        {
            int number = Random.Range(0, 2);
            if (number == 0)
            {
                shot = false;
            }
            else
            {
                shot = true;
            }
        }
        else
        {
            shot = false;
        }

        for (int i = 0; i < 6; i++)
        {
            recipe[i] = cho_mat[num, i];
        }
        if (shot == true)
        {
            recipe[recipe[5]] = 0;
            recipe[5]++;
        }
        str_init();
    }

    void str_init()
    {
        str = "";
        if (shot == true)
        {
            str = "진한 ";
        }

        if (num == 0)
        {
            str = str + "밀크커피 부탁해요";
        }
        else if (num == 1)
        {
            str = str + "블랙커피 부탁해요";
        }
        else if (num == 2)
        {
            str = str + "설탕커피 부탁해요";
        }
        else if (num == 3)
        {
            str = str + "녹차 부탁해요";
        }
        else if (num == 4)
        {
            str = str + "녹차 시원하게 부탁해요";
        }
        else if (num == 5)
        {
            str = str + "블랙커피 시원하게 부탁해요";
        }
        else if (num == 6)
        {
            str = str + "아이스커피 부탁해요";
        }

        txtRecipe.text = str;
    }

}
