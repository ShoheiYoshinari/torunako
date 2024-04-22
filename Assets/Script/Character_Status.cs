using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
�v���C���[�ƓG�̋��ʂ̃X�e�[�^�X
 */
public class Character_Status : MonoBehaviour
{
    [SerializeField] public int MaxHP, NowHP, MaxMP, NowMP, ATK, DEF, SPD; //HP�AMP�A�U���A�h��A�X�s�[�h�̃X�e�[�^�X
    public int ATK_Plus = 0, DEF_Plus = 0;
    public string Character_Name;
    public float ATK_Scale = 1, DEF_Scale = 1;

    //�X�e�[�^�X�o�t�p�̍\����
    struct Buff
    {
        public int Turn;    //�o�t�̎c��^�[��
        public int Grade;   //�o�t�̒i�K
    }
    //��Ԉُ�ɂ������Ă��邩
    int Poison, //��
        Deadly_Poison,  //�ғ�
        Burn,   //�₯��
        Freeze, //����
        Paralysis,  //��� 
        Petrifaction,   //�Ή�
        Sleep,  //����
        Berserk, //����
        Anger,  //�{��
        Dark,   //�����
        Slow,   //�ݑ�
        Quick;  //�g�y

    Buff ATK_Buff, MATK_Buff, DEF_Buff, SPD_Buff, HIT_Buff, AVD_Buff;   //�U���A���@�U���A�h��A�X�s�[�h�A�����A����̃o�t

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�퓬���Ɏg���ŏI�I�ȍU����
    public int Battle_ATK()
    {
        return (int)((ATK + ATK_Plus) * ATK_Scale);
    }


    //�퓬���Ɏg���ŏI�I�Ȗh���
    public int Battle_DEF()
    {
        return (int)((DEF + DEF_Plus) * DEF_Scale);
    }

    //�o�t��t�^����Ƃ��p
    public void make_ATK_Buff(int turn,int grade)
    {
        ATK_Buff.Turn = turn;
        ATK_Buff.Grade = grade;
    }
    public void make_DEF_Buff(int turn, int grade)
    {
        DEF_Buff.Turn = turn;
        DEF_Buff.Grade = grade;
    }
    public void make_DEX_Buff(int turn, int grade)
    {
        SPD_Buff.Turn = turn;
        SPD_Buff.Grade = grade;
    }
}
