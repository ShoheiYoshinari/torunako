using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
使用時にHPを回復するアイテム
 */
public class _004_Medicine : Bottle
{
    int Heal_Amount;
    public _004_Medicine()
    {
        Item_Name = "くすり";
        Item_Detail = "HPを10回復する";
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
