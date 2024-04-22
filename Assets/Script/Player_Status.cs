using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
プレイヤーのステータス
 */
public class Player_Status : Character_Status
{
    [SerializeField]int B_HP, B_MP, B_ATK, B_DEF, B_SPD;    //ステータスの基礎値
    [SerializeField] Job Job;   //現在のジョブ
    public Weapon Equip_Weapon; //装備しているアイテム

    
    [SerializeField]List<Job> All_Job;
    public void Job_Change(Job job)
    {
        Job = job;
        MaxHP = B_HP + Job.j_HP;
        if (MaxHP < NowHP)
            NowHP = MaxHP;
        MaxMP = B_MP + Job.j_MP;
        if (MaxMP < NowMP)
            NowMP = MaxMP;
        ATK = B_ATK + Job.j_ATK;
        DEF = B_DEF + Job.j_DEF;
        SPD = B_SPD + Job.j_SPD;

        GetComponent<Player_Move>().CharacterTip = job.characterTip;
    }
    //武器を装備する
    public void Equip(Weapon weapon)
    {
        if (weapon != null)
        {
            Equip_Weapon = weapon;
            ATK_Plus = weapon.ATK;
        }
        else
        {
            Equip_Weapon = null;
            ATK_Plus = 0;
        }
    }
    private void Awake()
    {
        
        Job_Change(All_Job[0]);
        NowHP = MaxHP;
        NowMP = MaxMP;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Job_Change(All_Job[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Job_Change(All_Job[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Job_Change(All_Job[2]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Job_Change(All_Job[3]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Job_Change(All_Job[4]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Job_Change(All_Job[5]);
        }
    }

    public Job JOB { get { return Job; } }
}
