using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
所持アイテムを管理
 */
public class belongings : MonoBehaviour
{
    Item[] Items = new Item[10];  //所持アイテム一覧
    int Max_Item=10;    //最大のアイテム所持数
    // Start is called before the first frame update
    void Start()
    {
        Items[0] = new _004_Medicine();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //持ち物の数を返す
    public int Number_of_Item()
    {
        int count = 0;
        for(int i = 0; i < Max_Item; i++)
        {
            if (Items[i] != null)
            {
                count++;
            }
            else
            {
                return count;
            }
        }
        return count;
    }

    //アイテムを獲得して所持品に入れる,返り値がtrueの場合は獲得できた,falseの場合は（アイテム欄がいっぱいなどで）獲得できなかった
    public bool Item_Get(Item item)
    {
        for (int i = 0; i < Max_Item; i++)
        {
            if (Items[i] == null)
            {
                Items[i] = item;
                return true;
            }
        }
        return false;
    }

    //pre番目のアイテムをpost番目に並び変える
    public void Sort(int pre,int post)
    {
        Item temp = Items[pre];
        if (pre > post)
        {
            for (int i = pre; i > post; i--)
            {
                Items[i] = Items[i - 1];
            }
            
        }
        else
        {
            for (int i = pre; i < post; i++)
            {
                Items[i] = Items[i + 1];
            }
            
        }
        Items[post] = temp;
    }

    //n番目のアイテムを所持品から削除
    public void Delete(int n)
    {
        int i = Number_of_Item();
        Items[n] = null;
        Sort(n, i - 1);
    }

    //特定の所持品のアイテム情報を返す
    public Item Item(int i)
    {
        if (i >= 0 && i < 10)
        {
            return Items[i];
        }
        return null;
    }

    //i番目の武器を装備する
    public void EquipWeapon(int i)
    {
        if (i >= 0 && i < Max_Item)
        {
            if (Items[i] != null)
            {
                Sort(i, 0); //装備した武器はアイテム欄の先頭へ
            }
        }
    }
}
