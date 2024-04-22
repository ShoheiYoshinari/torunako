using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item_Type
{
    weapon,
    accessory,
    bottle,
    scroll,
    staff,
    other,
}

/*
全アイテムの親となる抽象クラス
 */
public abstract class Item
{
    
    public Item_Type item_type;  //アイテムの種類
    protected string Item_Name;　//アイテム名
    public string Item_Detail;  //アイテム説明の文章
    public Sprite Item_Image;   //アイテムのアイコン
    protected int ItemID;  //アイテムのID
    public Item()
    {
        
    }

    //アイテム使用時の処理
    public abstract bool Use();


    public string Name
    {
        get { return Item_Name; }
    }
    public int id
    {
        get { return ItemID; }
    }
}
