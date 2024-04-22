using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
瓶系アイテムのクラス
 */
public abstract class Bottle : Item
{
    Player_Status Player;
    public Bottle()
    {
        Player = GameObject.Find("player").GetComponent<Player_Status>();
        item_type = Item_Type.bottle;
    }

    public override bool Use()
    {
        Use_Bottle(Player);
        return true;
    }

    public abstract void Use_Bottle(Character_Status character);
}
