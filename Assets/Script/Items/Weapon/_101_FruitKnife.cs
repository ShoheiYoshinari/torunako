using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class _101_FruitKnife : Weapon
{
    public _101_FruitKnife()
    {
        ItemID = 101;   //���̕�������Ƃ��̓A�C�e��ID�ƃN���X���ȊO�R�s�y;

        Item_Detail = "�A�C�e���̂��߂�";
        Item_Image = Resources.Load<Sprite>("Item_Icon/Icon" + ItemID);

        //CSV���畐��̃f�[�^���擾
        TextAsset csvFile;
        List<string[]> csvDatas = new List<string[]>();
        csvFile = Resources.Load("Item/Weapon") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        for (int i = 0; i < ItemID - 99; i++)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        if (int.Parse(csvDatas[csvDatas.Count - 1][0]) != ItemID)
            Debug.LogError("ID���Ⴄ");

        Item_Name = csvDatas[csvDatas.Count - 1][1];
        ATK = int.Parse(csvDatas[csvDatas.Count - 1][2]);
        MGC = int.Parse(csvDatas[csvDatas.Count - 1][3]);
        DEF = int.Parse(csvDatas[csvDatas.Count - 1][4]);
        DEX = int.Parse(csvDatas[csvDatas.Count - 1][5]);
    }
}
