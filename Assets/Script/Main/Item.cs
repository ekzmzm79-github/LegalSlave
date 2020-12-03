using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class Item : MonoBehaviour {

    int Highest_Rayer = DayBlinder.Highest_Rayer;
    int Lowest_Rayer = DayBlinder.Lowest_Rayer;

    public Sprite[] Items;
    public Canvas[] ItemCanvas;
    public Button[] NowItems;
    

    int NowItemsIndex;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public void LoadItems(double[] items)
    {


        for (int i=0; i<4; i++)
        {
            if (items[i] >= 1.0)
            {

                //아이템의 아이디 == 정수 부분
                ItemInfo.itemID[i] = (int)items[i];


                //아이템의 남은 기간 == 소수 부분
                if (items[i] * 10 % 1 == 0)
                {
                    ItemInfo.itemPeriod[i] = (items[i] - ItemInfo.itemID[i]) * 10;

                }
                else if (items[i] * 100 % 1 == 0)
                {
                    ItemInfo.itemPeriod[i] = (items[i] - ItemInfo.itemID[i]) * 100;
                }
                else
                {
                    ItemInfo.itemPeriod[i] = (items[i] - ItemInfo.itemID[i]) * 1000;
                }

                //남은 기간이 0? == 해당 아이템 사라짐


                if (ItemInfo.itemPeriod[i] == 0.0)
                {
                    NowItems[NowItemsIndex].gameObject.GetComponent<Image>().sprite = Items[0];
                    ItemCanvas[NowItemsIndex].sortingOrder = Lowest_Rayer;
                    continue;
                }



                NowItems[NowItemsIndex].gameObject.GetComponent<Image>().sprite = Items[ItemInfo.itemID[i]];
                ItemCanvas[NowItemsIndex].sortingOrder = Highest_Rayer - 1;

                ItemEffectApply(i);

                NowItems[NowItemsIndex].SendMessage("LoadItemText", ItemInfo.itemID[i]);
                NowItemsIndex++;



            }


        }

        //
    }


    public void ItemEffectApply(int index)
    {

        //아이템 정보
        string JsonStr2 = File.ReadAllText(DataPathStringClass.DataPathString() + "/text/ItemData.json");
        JsonData ItemData = JsonMapper.ToObject(JsonStr2);

        string heart = ItemData[ItemInfo.itemID[index] - 1]["Heart"].ToString();
        string colleague = ItemData[ItemInfo.itemID[index] - 1]["Colleague"].ToString();
        string workAB = ItemData[ItemInfo.itemID[index] - 1]["WorkAB"].ToString();
        string stress = ItemData[ItemInfo.itemID[index] - 1]["Stress"].ToString();

        DayBuffInfo.Heart += int.Parse(heart);
        DayBuffInfo.Colleague += int.Parse(colleague);
        DayBuffInfo.WorkAB += int.Parse(workAB);
        DayBuffInfo.Stress += int.Parse(stress);
    }



}

public static class ItemInfo
{
    public static int[] itemID =new int[4];
    public static double[] itemPeriod = new double[4];
}

public static class DayBuffInfo
{
    //하루에 버프 형식으로 파라미터 적용되는 수치의 합산.

    public static int Heart;
    public static int Colleague;
    public static int WorkAB;
    public static int Stress;

}
