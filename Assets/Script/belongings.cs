using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
�����A�C�e�����Ǘ�
 */
public class belongings : MonoBehaviour
{
    Item[] Items = new Item[10];  //�����A�C�e���ꗗ
    int Max_Item=10;    //�ő�̃A�C�e��������
    // Start is called before the first frame update
    void Start()
    {
        Items[0] = new _004_Medicine();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�������̐���Ԃ�
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

    //�A�C�e�����l�����ď����i�ɓ����,�Ԃ�l��true�̏ꍇ�͊l���ł���,false�̏ꍇ�́i�A�C�e�����������ς��ȂǂŁj�l���ł��Ȃ�����
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

    //pre�Ԗڂ̃A�C�e����post�Ԗڂɕ��ѕς���
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

    //n�Ԗڂ̃A�C�e���������i����폜
    public void Delete(int n)
    {
        int i = Number_of_Item();
        Items[n] = null;
        Sort(n, i - 1);
    }

    //����̏����i�̃A�C�e������Ԃ�
    public Item Item(int i)
    {
        if (i >= 0 && i < 10)
        {
            return Items[i];
        }
        return null;
    }

    //i�Ԗڂ̕���𑕔�����
    public void EquipWeapon(int i)
    {
        if (i >= 0 && i < Max_Item)
        {
            if (Items[i] != null)
            {
                Sort(i, 0); //������������̓A�C�e�����̐擪��
            }
        }
    }
}
