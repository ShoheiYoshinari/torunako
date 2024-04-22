using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
����n�A�C�e���̃N���X
 */
public abstract class Weapon : Item
{
    public int ATK, MGC, DEF, DEX;  //�e��X�e�[�^�X
    Player_Status Player;
    public Weapon()
    {
        Player = GameObject.Find("player").GetComponent<Player_Status>();
        item_type = Item_Type.weapon;
    }

    //����𑕔�����
    public override bool Use()
    {
        if (Player.Equip_Weapon == null)
        {
            Player.Equip(this);
            return true;
        }
        if (Player.Equip_Weapon == this)
        {
            Player.Equip(null);
            return false;
        }
        Player.Equip(this);
        return true;
    }
}
