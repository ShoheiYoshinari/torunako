using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
プレイヤーと敵の共通の挙動
 */

//方向を表す
public enum Direction
{
    right,
    left,
    up,
    down,
    right_up,
    right_down,
    left_up,
    left_down,
    no,
}
public abstract class Character_Move : MonoBehaviour
{
    protected GameMode GameMode;
    [SerializeField] protected Animator Animator;
    protected Distance Distance;
    protected Dungeon_Create Dungeon_Create;
    protected Move_Controller Move_Controller;
    protected Log Log;

    public List<Sprite> CharacterTip;

    float MoveX=0, MoveY=0; //1フレームごとの移動距離
    int GoalX = 0, GoalY = 0;   //移動時の目標到達地点
    int Move_interval = 5;  //1フレームごとの移動間隔
    int move_count = 0; //移動時のフレームカウント　移動終了の判断に使う
    
    
    protected bool action = false;  //行動できるかを表す
    [SerializeField] protected int PosX, PosY;
    [SerializeField] protected Character_Status Status;
    
    void Awake()
    {
        GameMode = GameObject.Find("GameController").GetComponent<GameMode>();
        Distance = GameObject.Find("GameController").GetComponent<Distance>();
        Dungeon_Create = GameObject.Find("GameController").GetComponent<Dungeon_Create>();
        Move_Controller = GameObject.Find("GameController").GetComponent<Move_Controller>();
        Log = GameObject.Find("Log").GetComponent<Log>();
        PosX = (int)transform.position.x;
        PosY = (int)transform.position.y;
    }
    

    protected Direction direction;  //キャラが向いてる方向を表す

    //特定座標に移動しようとする
    void MovePos(int x, int y)
    {
        Move_Controller.Move_MOBs.Add(this);
        Dungeon_Create.All_MOB[PosX, PosY] = null;
        PosX += x;
        PosY += y;
        Dungeon_Create.All_MOB[PosX, PosY] = this.Status;
        MoveX = (float)x / Move_interval;
        MoveY = (float)y / Move_interval;
        GoalX = PosX;
        GoalY = PosY;
        move_count = 0;
    }

    //キャラの向きを変更
    protected void DirectionChange(Direction dir)
    {
        transform.position = new Vector2((int)Math.Round(transform.position.x), (int)Math.Round(transform.position.y));
        direction = dir;
        switch (direction)
        {
            case Direction.right:
                Animator.SetFloat("X", 1);
                Animator.SetFloat("Y", 0);
                break;
            case Direction.left:
                Animator.SetFloat("X", -1);
                Animator.SetFloat("Y", 0);
                break;
            case Direction.up:
                Animator.SetFloat("X", 0);
                Animator.SetFloat("Y", 1);
                break;
            case Direction.down:
                Animator.SetFloat("X", 0);
                Animator.SetFloat("Y", -1);
                break;
            case Direction.right_up:
                Animator.SetFloat("X", 1);
                Animator.SetFloat("Y", 1);
                break;
            case Direction.right_down:
                Animator.SetFloat("X", 1);
                Animator.SetFloat("Y", -1);
                break;
            case Direction.left_up:
                Animator.SetFloat("X", -1);
                Animator.SetFloat("Y", 1);
                break;
            case Direction.left_down:
                Animator.SetFloat("X", -1);
                Animator.SetFloat("Y", -1);
                break;
        }

    }

    //移動中
    protected bool Moving()
    {
        transform.Translate(MoveX, MoveY, 0);
        move_count++;
        if (move_count >= Move_interval)
        {
            Move_Controller.Move_MOBs.Remove(this);
            transform.position = new Vector2(GoalX, GoalY);
            MoveX = 0;
            MoveY = 0;
            Animator.SetBool("MOVE", false);
            Animator.SetBool("RETURN", true);
            return true;
        }
        return false;
    }

    //攻撃時に呼び出される
    protected void Attack(bool player=true)
    {
        Animator.SetTrigger("ATTACK");
        GameObject enemy = null;
        switch (direction)
        {
            case Direction.right:
                enemy = isAttack(1, 0, player);
                break;
            case Direction.left:
                enemy = isAttack(-1, 0, player);
                break;
            case Direction.up:
                enemy = isAttack(0, 1, player);
                break;
            case Direction.down:
                enemy = isAttack(0, -1,  player);
                break;
            case Direction.right_up:
                enemy = isAttack(1, 1, player);
                break;
            case Direction.right_down:
                enemy = isAttack(1, -1,  player);
                break;
            case Direction.left_up:
                enemy = isAttack(-1, 1, player);
                break;
            case Direction.left_down:
                enemy = isAttack(-1, -1, player);
                break;
        }
        if (enemy != null)
        {
            Attack_Keisan(enemy);
        }
    }

    //攻撃先に攻撃できる相手がいるか確認
    protected GameObject isAttack(int x, int y,bool player)
    {
        Character_Status character_Status = Dungeon_Create.All_MOB[PosX + x, PosY + y];
        if (character_Status == null)
            return null;
        if (player)
        {
            if (character_Status.GetType() == typeof(Enemy_Status))
                return character_Status.gameObject;
        }
        else
        {
            if (character_Status.GetType() == typeof(Player_Status))
                return character_Status.gameObject;
        }
        return null;
    }

    //攻撃時のダメージ計算
    void Attack_Keisan(GameObject enemy)
    {
        double dam;
        dam = Math.Pow(this.Status.Battle_ATK(),2) / (3 * enemy.GetComponent<Character_Status>().Battle_DEF());
        if (dam < 0)
            dam = 0;
        int damage = (int)Math.Ceiling(dam);
        //回避判定
        if (UnityEngine.Random.value < 1.0 - (enemy.GetComponent<Character_Status>().SPD - Status.SPD) / 100.0)
        {
            Log.Add_Log(Status.Character_Name + "は" + enemy.GetComponent<Character_Status>().Character_Name + "に" + damage + "のダメージ");
            enemy.GetComponent<Character_Status>().NowHP -= damage;
        }
        else
        {
            Log.Add_Log(enemy.GetComponent<Character_Status>().Character_Name + "は" + Status.Character_Name + "の攻撃をよけた");
        }
            
    }

    //特定方向に移動可能か，オプションで攻撃可能か
    protected bool isMove_Dir(Direction direction, bool attack = false)
    {
        switch (direction)
        {
            case Direction.right:
                if (!Dungeon_Create.WallCheck(PosX + 1, PosY))
                {
                    if (attack)
                        return true;
                    else
                    {
                        if (!Dungeon_Create.Exist_MOB(PosX + 1, PosY))
                            return true;
                    }
                }
                break;
            case Direction.left:
                if (!Dungeon_Create.WallCheck(PosX - 1, PosY))
                {
                    if (attack)
                        return true;
                    else
                    {
                        if (!Dungeon_Create.Exist_MOB(PosX - 1, PosY))
                            return true;
                    }
                }
                break;
            case Direction.up:
                if (!Dungeon_Create.WallCheck(PosX, PosY + 1))
                {
                    if (attack)
                        return true;
                    else
                    {
                        if (!Dungeon_Create.Exist_MOB(PosX, PosY + 1))
                            return true;
                    }
                }
                break;
            case Direction.down:
                if (!Dungeon_Create.WallCheck(PosX, PosY - 1))
                {
                    if (attack)
                        return true;
                    else
                    {
                        if (!Dungeon_Create.Exist_MOB(PosX, PosY - 1))
                            return true;
                    }
                }
                break;
            case Direction.right_up:
                if (!Dungeon_Create.WallCheck(PosX + 1, PosY) && !Dungeon_Create.WallCheck(PosX, PosY + 1) && !Dungeon_Create.WallCheck(PosX + 1, PosY + 1))
                {
                    if (attack)
                        return true;
                    else
                    {
                        if (!Dungeon_Create.Exist_MOB(PosX + 1, PosY + 1))
                            return true;
                    }
                }
                break;
            case Direction.right_down:
                if (!Dungeon_Create.WallCheck(PosX + 1, PosY) && !Dungeon_Create.WallCheck(PosX, PosY - 1) && !Dungeon_Create.WallCheck(PosX + 1, PosY - 1))
                {
                    if (attack)
                        return true;
                    else
                    {
                        if (!Dungeon_Create.Exist_MOB(PosX + 1, PosY - 1))
                            return true;
                    }
                }
                break;
            case Direction.left_up:
                if (!Dungeon_Create.WallCheck(PosX - 1, PosY) && !Dungeon_Create.WallCheck(PosX, PosY + 1) && !Dungeon_Create.WallCheck(PosX - 1, PosY + 1))
                {
                    if (attack)
                        return true;
                    else
                    {
                        if (!Dungeon_Create.Exist_MOB(PosX - 1, PosY + 1))
                            return true;
                    }
                }
                break;
            case Direction.left_down:
                if (!Dungeon_Create.WallCheck(PosX - 1, PosY) && !Dungeon_Create.WallCheck(PosX, PosY - 1) && !Dungeon_Create.WallCheck(PosX - 1, PosY - 1))
                {
                    if (attack)
                        return true;
                    else
                    {
                        if (!Dungeon_Create.Exist_MOB(PosX - 1, PosY - 1))
                            return true;
                    }
                }
                break;
        }
        return false;
    }

    

    //引数の方向に移動させる
    protected void Move_Dir(Direction dir)
    {
        switch (dir)
        {
            case Direction.right:
                MovePos(1, 0);
                break;
            case Direction.left:
                MovePos(-1, 0);
                break;
            case Direction.up:
                MovePos(0, 1);
                break;
            case Direction.down:
                MovePos(0, -1);
                break;
            case Direction.right_up:
                MovePos(1, 1);
                break;
            case Direction.right_down:
                MovePos(1, -1);
                break;
            case Direction.left_up:
                MovePos(-1, 1);
                break;
            case Direction.left_down:
                MovePos(-1, -1);
                break;
        }
        Animator.SetBool("MOVE", true);
        Animator.SetBool("RETURN", false);
    }

    //何も行動しない(その場で足踏み)
    protected void No_Action()
    {
        MovePos(0, 0);
        Animator.SetBool("MOVE", true);
        Animator.SetBool("RETURN", false);
    }

    //行動開始時の処理
    public abstract void ActionStart();

    //行動終了時の処理
    public abstract void ActionEnd();

    //変数上の座標をオブジェクトの座標に更新
    public void TransformUpdate()
    {
        PosX = (int)transform.position.x;
        PosY = (int)transform.position.y;
    }

    public int X
    {
        get { return PosX; }
    }
    public int Y
    {
        get { return PosY; }
    }

    public Direction Direction
    {
        get { return direction; }
    }
    public void SpriteChange(int SpriteNo)
    {
        GetComponent<SpriteRenderer>().sprite = CharacterTip[SpriteNo];
    }
}
