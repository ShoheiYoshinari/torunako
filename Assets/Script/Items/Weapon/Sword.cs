using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Sword : Weapon
{
    public Sword()
    {
        ItemID = 101;

        Item_Detail = "‚¯‚ñ‚Ì‚¹‚Â‚ß‚¢";
        Item_Image = Resources.Load<Sprite>("Item_Icon/Icon013");
        TextAsset csvFile;
        List<string[]> csvDatas = new List<string[]>();
        csvFile = Resources.Load("Dungeon/Item_Pop") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        for (int i = 0; i < ItemID - 99; i++)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        if (int.Parse(csvDatas[csvDatas.Count][0]) != ItemID)
            Debug.LogError("ID‚ªˆá‚¤");

        Item_Name = csvDatas[csvDatas.Count][1];
        ATK = int.Parse(csvDatas[csvDatas.Count][2]);
        MGC = int.Parse(csvDatas[csvDatas.Count][3]);
        DEF = int.Parse(csvDatas[csvDatas.Count][4]);
        DEX = int.Parse(csvDatas[csvDatas.Count][5]);
    }

}
