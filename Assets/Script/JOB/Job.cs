using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
それぞれのジョブに継承させるクラス
 */
public abstract class Job : MonoBehaviour
{
    protected string Name;  //ジョブの名前
    protected int Level;   //ジョブのレべル
    protected int Exp;  //ジョブで取得した総経験値
    protected int Status_Point; //保持しているステータスポイント
    protected int S_HP, S_MP, S_ATK, S_DEF, S_SPD;    //各ステータスに振ったステータスポイント
    protected int J_HP, J_MP, J_ATK, J_DEF, J_SPD;    //ステータスのジョブ値
    
    [SerializeField]protected List<Sprite> CharacterTip;    //キャラクターの絵

    void Awake()
    {
        Level = 0;
        Exp = 0;
        Status_Point = 0;
    }
  

    //経験値獲得時の処理
    public void Get_EXP(int exp)
    {
        Exp += exp;
        if (isLevelUP())
        {
            Level++;
            Status_Point += 3;
        }
    }

    //レベルアップするか
    bool isLevelUP()
    {
        return false;
    }

    //ジョブの情報を読み込む
    protected void Load(string Sprite_Pass,string Csv_Pass)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(Sprite_Pass);
        for (int i = 0; i < 24; i++)
        {
            CharacterTip.Add(sprites[i]);
        }
        TextAsset csvFile;
        List<string[]> csvDatas = new List<string[]>();
        csvFile = Resources.Load(Csv_Pass) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        J_HP = int.Parse(csvDatas[1][1]);
        J_MP = int.Parse(csvDatas[1][2]);
        J_ATK = int.Parse(csvDatas[1][3]);
        J_DEF = int.Parse(csvDatas[1][4]);
        J_SPD = int.Parse(csvDatas[1][5]);
        Name = csvDatas[0][0];
    }

    public string NAME { get { return Name; } }

    public int level { get { return Level; } }

    public int s_HP { get { return S_HP; } }
    public int s_MP { get { return S_MP; } }
    public int s_ATK { get { return S_ATK; } }
    public int s_DEF { get { return S_DEF; } }
    public int s_SPD { get { return S_SPD; } }

    public int j_HP { get { return J_HP; } }
    public int j_MP { get { return J_MP; } }
    public int j_ATK { get { return J_ATK; } }
    public int j_DEF { get { return J_DEF; } }
    public int j_SPD { get { return J_SPD; } }
    public List<Sprite> characterTip { get { return CharacterTip; } }
}
