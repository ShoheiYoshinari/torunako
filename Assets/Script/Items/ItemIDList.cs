using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
���͂��ꂽ�A�C�e��ID���ƂɃA�C�e����Ԃ�
 */
public class ItemIDList : MonoBehaviour
{
    public Item Creat_Item(int ItemID)
    {
        switch (ItemID)
        {
            case 1:
                return new _004_Medicine();
                break;
            case 2:
                return new Sword();
                break;
            case 101:
                return new _101_FruitKnife();
                break;
        }
        return null;
    }
}
