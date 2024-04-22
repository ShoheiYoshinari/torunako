using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
マップの各区画，または部屋の情報
 */
public class Room
{
    public int right,left, top, bottom, width, height;  //部屋の右端，左端，上端，下端，横幅，縦幅

    //部屋の初期化
    public Room(int R,int L,int T,int B)
    {
        right = R;
        left = L;
        top = T;
        bottom = B;
        width = right - left;
        height = top - bottom;
    }

    //部屋のサイズを作り直す
    public void Set(int R, int L, int T, int B)
    {
        right = R;
        left = L;
        top = T;
        bottom = B;
        width = right - left;
        height = top - bottom;
    }

}
