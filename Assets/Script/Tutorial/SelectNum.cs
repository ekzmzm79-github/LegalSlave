using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNum : MonoBehaviour
{

    static int Selnum = -1;

    public static int Select //실제로 외부 코드에서 사용하는 부분
    {
        get
        {
            int ret = Selnum;//리턴할값 지정
            Selnum = -1; //한번 호출된 뒤로는 다시 -1이 된다.
                        //     if (Selnumchoice == ret && ret != 0)
                        //         {
                        //            ret = 0-ret;
                        //            Selnumchoice = 0;
                        //        }
            //Debug.Log("ret=" + ret);
            return ret;//리턴.
        }
        set
        {
            if (value != -1) Selnum = value; //-1이 아닌경우(get에서 다시 set되는거 방지)에만 value를 받아들임.
        }
    }
}