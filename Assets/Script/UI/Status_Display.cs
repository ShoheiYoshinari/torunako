using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status_Display : MonoBehaviour
{
    [SerializeField] GameObject Status_Display_UI;

    Player_Status Player;
    [SerializeField] Slider HP_Slider, MP_Slider, SAN_Slider; //HP.MPバーのゲージ
    [SerializeField] Text HP_NowMax, MP_NowMax, SAN_NowMax;  //HP,MP,正気度ゲージ用の数字表示
    [SerializeField] Text HP, MP, ATK, DEF, SPD;     //各ステータスの数字表示
    [SerializeField] Text JOB_Name; //現在のジョブの名前
    [SerializeField] Image Effect_Icon;
    [SerializeField] Text ATK_Buff, MATK_Buff, DEF_Buff, SPD_Buff, HIT_Buff, AVD_Buff;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("player").GetComponent<Player_Status>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open_StatusDisplay()
    {
        Status_Display_UI.SetActive(true);
        Update_Status();
    }

    public void Close_StatusDisplay()
    {
        Status_Display_UI.SetActive(false);
    }

    //プレイヤーのステータスを読み込んで表示
    void Update_Status()
    {
        HP_Slider.value = (float)Player.NowHP / Player.MaxHP;
        HP_NowMax.text = Player.NowHP + " / " + Player.MaxHP;
        MP_Slider.value = (float)Player.NowMP / Player.MaxMP;
        MP_NowMax.text = Player.NowMP + " / " + Player.MaxMP;
        //SAN_Slider.value = (float)Player.NowSAN/Player.MaxSAN;
        //SAN_NowMax.text = Player.NowSAN + " / " + Player.MaxSAN;
        HP.text = "HP　" + Player.MaxHP;
        MP.text = "MP　" + Player.MaxMP;
        ATK.text = "ATK　" + Player.ATK;
        DEF.text = "DEF　" + Player.DEF;
        SPD.text = "SPD　" + Player.SPD;
        JOB_Name.text = Player.JOB.NAME;
    }
}
