using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
ゲームが今どういう状態かを表す変数Gamemodeを管理する
 */
public class GameMode : MonoBehaviour
{
    public enum Game_Mode
    {
        operabale,          //プレイヤーが移動，攻撃の操作ができる
        only_player_moving, //プレイヤーだけが移動中
        moving,             //プレイヤーもしくは敵の移動中
        enemy,              //敵の行動中
        menu,               //メニューを開いている
        attack,              //プレイヤーもしくは敵の攻撃中
        enemy_controller,   //敵AIに命令中
        enemy_attack_controller,    //敵への攻撃命令中
        throwing,           //アイテムを投げてる
        game_over,          //ゲームオーバーになっている
        game_clear,         //ゲームクリアしている
        stairs, //階段チェック
    }

    [SerializeField] Game_Mode gamemode = Game_Mode.operabale;
    Game_Mode temp;
    bool isChange=false;

    
    public Game_Mode Gamemode
    {
        get { return gamemode; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange)
        {
            isChange = false;
            gamemode = temp;
        }
        
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    //ゲームモードを変える
    public void Mode_Change(Game_Mode gameMode)
    {
        if (temp == Game_Mode.game_clear || temp == Game_Mode.game_over)
            return;
        temp = gameMode;
        isChange = true;
    }

}
