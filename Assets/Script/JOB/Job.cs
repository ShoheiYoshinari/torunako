using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
���ꂼ��̃W���u�Ɍp��������N���X
 */
public abstract class Job : MonoBehaviour
{
    protected string Name;  //�W���u�̖��O
    protected int Level;   //�W���u�̃��׃�
    protected int Exp;  //�W���u�Ŏ擾�������o���l
    protected int Status_Point; //�ێ����Ă���X�e�[�^�X�|�C���g
    protected int S_HP, S_MP, S_ATK, S_DEF, S_SPD;    //�e�X�e�[�^�X�ɐU�����X�e�[�^�X�|�C���g
    protected int J_HP, J_MP, J_ATK, J_DEF, J_SPD;    //�X�e�[�^�X�̃W���u�l
    
    [SerializeField]protected List<Sprite> CharacterTip;    //�L�����N�^�[�̊G

    void Awake()
    {
        Level = 0;
        Exp = 0;
        Status_Point = 0;
    }
  

    //�o���l�l�����̏���
    public void Get_EXP(int exp)
    {
        Exp += exp;
        if (isLevelUP())
        {
            Level++;
            Status_Point += 3;
        }
    }

    //���x���A�b�v���邩
    bool isLevelUP()
    {
        return false;
    }

    //�W���u�̏���ǂݍ���
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
