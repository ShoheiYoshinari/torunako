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
�S�A�C�e���̐e�ƂȂ钊�ۃN���X
 */
public abstract class Item
{
    
    public Item_Type item_type;  //�A�C�e���̎��
    protected string Item_Name;�@//�A�C�e����
    public string Item_Detail;  //�A�C�e�������̕���
    public Sprite Item_Image;   //�A�C�e���̃A�C�R��
    protected int ItemID;  //�A�C�e����ID
    public Item()
    {
        
    }

    //�A�C�e���g�p���̏���
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
