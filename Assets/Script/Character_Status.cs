using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
プレイヤーと敵の共通のステータス
 */
public class Character_Status : MonoBehaviour
{
    [SerializeField] public int MaxHP, NowHP, MaxMP, NowMP, ATK, DEF, SPD; //HP、MP、攻撃、防御、スピードのステータス
    public int ATK_Plus = 0, DEF_Plus = 0;
    public string Character_Name;
    public float ATK_Scale = 1, DEF_Scale = 1;

    //ステータスバフ用の構造体
    struct Buff
    {
        public int Turn;    //バフの残りターン
        public int Grade;   //バフの段階
    }
    //状態異常にかかっているか
    int Poison, //毒
        Deadly_Poison,  //猛毒
        Burn,   //やけど
        Freeze, //凍傷
        Paralysis,  //麻痺 
        Petrifaction,   //石化
        Sleep,  //眠り
        Berserk, //狂乱
        Anger,  //怒り
        Dark,   //くらみ
        Slow,   //鈍足
        Quick;  //身軽

    Buff ATK_Buff, MATK_Buff, DEF_Buff, SPD_Buff, HIT_Buff, AVD_Buff;   //攻撃、魔法攻撃、防御、スピード、命中、回避のバフ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //戦闘時に使う最終的な攻撃力
    public int Battle_ATK()
    {
        return (int)((ATK + ATK_Plus) * ATK_Scale);
    }


    //戦闘時に使う最終的な防御力
    public int Battle_DEF()
    {
        return (int)((DEF + DEF_Plus) * DEF_Scale);
    }

    //バフを付与するとき用
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
