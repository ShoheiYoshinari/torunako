using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
�g�p����HP���񕜂���A�C�e��
 */
public class _004_Medicine : Bottle
{
    int Heal_Amount;
    public _004_Medicine()
    {
        Item_Name = "������";
        Item_Detail = "HP��10�񕜂���";
        Item_Image = Resources.Load<Sprite>("Item_Icon/Icon004");
        ItemID = 1;
        Heal_Amount = 10;
    }

    public override void Use_Bottle(Character_Status character)
    {
        character.NowHP += Heal_Amount;
        if (character.MaxHP < character.NowHP)
        {
            character.NowHP = character.MaxHP;
        }
    }
}
