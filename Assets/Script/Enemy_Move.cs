using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
敵の挙動
 */
public class Enemy_Move : Character_Move
{
    Player_Move Player;
    EnemyController EnemyController;
    Transform CameraPos;
    [SerializeField] int Visible_Distance;  //敵がプレイヤーに気づく距離

    // Start is called before the first frame update
    void Start()
    {
        CameraPos = GameObject.Find("Main Camera").transform;
        EnemyController = GameObject.Find("EnemyController").GetComponent<EnemyController>();
        Player = GameObject.Find("player").GetComponent<Player_Move>();
        EnemyController.Enemies.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Status.NowHP <= 0)
        {
            EnemyController.Enemy_Dead(this);
            Dungeon_Create.All_MOB[PosX, PosY] = null;
            Destroy(this.gameObject);
        }
        if (action)
        {
            if (GameMode.Gamemode == GameMode.Game_Mode.moving)
            {
                if (Moving())
                {
                    ActionEnd();
                }
            }
            else if (GameMode.Gamemode == GameMode.Game_Mode.attack)
            {
                Attack(false);
                ActionEnd();
            }
        }
        
    }

    public bool Action()
    {
        //Debug.Log(gameObject.name);
        //Debug.Log(Distance_to_Player());
        //プレイヤーに攻撃可能な場合
        if (Distance_to_Player() == 1)
        {
            DirectionChange(Search_Attack());
            return true;
        }
        //プレイヤーと離れている場合
        else if (Distance_to_Player() < Visible_Distance)
        {
            Direction dir = Search_Move();
            if (dir == Direction.no)
                No_Action();
            else
                Move_Enemy(dir);
        }
        else
        {
            Random_Move();
        }
        return false;
    }

    //プレイヤーに近づく方向を調べる
    Direction Search_Move()
    {
        Direction dir = Direction.no;
        int min = Distance.Get_distances(PosX, PosY);
        float dis = 10000;
        int tempm;
        float tempd;
        tempm = Distance.Get_distances(PosX + 1, PosY);
        tempd = player_dis(new Vector2(PosX + 1, PosY));
        if (tempm < min)
        {
            if (isMove_Dir(Direction.right))
            {
                dir = Direction.right;
                min = tempm;
                dis = tempd;
            }
        }
        else if (tempm == min)
        {
            if (isMove_Dir(Direction.right) && dis >= tempd)
            {
                dir = Direction.right;
                min = tempm;
                dis = tempd;
            }
        }

        tempm = Distance.Get_distances(PosX - 1, PosY);
        tempd = player_dis(new Vector2(PosX - 1, PosY));
        if (tempm < min)
        {
            if (isMove_Dir(Direction.left))
            {
                dir = Direction.left;
                min = tempm;
                dis = tempd;
            }
        }
        else if (tempm == min)
        {
            if (isMove_Dir(Direction.left) && dis >= tempd)
            {
                dir = Direction.left;
                min = tempm;
                dis = tempd;
            }
        }

        tempm = Distance.Get_distances(PosX, PosY + 1);
        tempd = player_dis(new Vector2(PosX, PosY + 1));
        if (tempm < min)
        {
            if (isMove_Dir(Direction.up))
            {
                dir = Direction.up;
                min = tempm;
                dis = tempd;
            }
        }
        else if (tempm == min)
        {
            if (isMove_Dir(Direction.up) && dis >= tempd)
            {
                dir = Direction.up;
                min = tempm;
                dis = tempd;
            }
        }

        tempd = player_dis(new Vector2(PosX, PosY - 1));
        tempm = Distance.Get_distances(PosX, PosY - 1);
        if (tempm < min)
        {
            if (isMove_Dir(Direction.down))
            {
                dir = Direction.down;
                min = tempm;
                dis = tempd;
            }
        }
        else if (tempm == min)
        {
            if (isMove_Dir(Direction.down) && dis >= tempd)
            {
                dir = Direction.down;
                min = tempm;
                dis = tempd;
            }
        }

        tempd = player_dis(new Vector2(PosX + 1, PosY + 1));
        tempm = Distance.Get_distances(PosX + 1, PosY + 1);
        if (tempm < min)
        {
            if (isMove_Dir(Direction.right_up))
            {
                dir = Direction.right_up;
                min = tempm;
                dis = tempd;
            }
        }
        else if (tempm == min)
        {
            if (isMove_Dir(Direction.right_up) && dis >= tempd)
            {
                dir = Direction.right_up;
                min = tempm;
                dis = tempd;
            }
        }

        tempd = player_dis(new Vector2(PosX + 1, PosY - 1));
        tempm = Distance.Get_distances(PosX + 1, PosY - 1);
        if (tempm < min)
        {
            if (isMove_Dir(Direction.right_down))
            {
                dir = Direction.right_down;
                min = tempm;
                dis = tempd;
            }
        }
        else if (tempm == min)
        {
            if (isMove_Dir(Direction.right_down) && dis >= tempd)
            {
                dir = Direction.right_down;
                min = tempm;
                dis = tempd;
            }
        }

        tempd = player_dis(new Vector2(PosX - 1, PosY + 1));
        tempm = Distance.Get_distances(PosX - 1, PosY + 1);
        if (tempm < min)
        {
            if (isMove_Dir(Direction.left_up))
            {
                dir = Direction.left_up;
                min = tempm;
                dis = tempd;
            }
        }
        else if (tempm == min)
        {
            if (isMove_Dir(Direction.left_up) && dis >= tempd)
            {
                dir = Direction.left_up;
                min = tempm;
                dis = tempd;
            }
        }

        tempd = player_dis(new Vector2(PosX - 1, PosY - 1));
        tempm = Distance.Get_distances(PosX - 1, PosY - 1);
        if (tempm < min)
        {
            if (isMove_Dir(Direction.left_down))
            {
                dir = Direction.left_down;
                min = tempm;
                dis = tempd;
            }
        }
        else if (tempm == min)
        {
            if (isMove_Dir(Direction.left_down) && dis >= tempd)
            {
                dir = Direction.left_down;
                min = tempm;
                dis = tempd;
            }
        }
        return dir;
    }

    float player_dis(Vector2 pos)
    {
        float dis = Vector2.Distance(pos, new Vector2(Player.X, Player.Y));
        return dis;
    }

    //攻撃方向を調べる
    Direction Search_Attack()
    {
        if (Distance.Get_distances(PosX + 1, PosY) == 0)
        {
            return Direction.right;
        }
        if (Distance.Get_distances(PosX - 1, PosY) == 0)
        {
            return Direction.left;
        }
        if (Distance.Get_distances(PosX, PosY + 1) == 0)
        {
            return Direction.up;
        }
        if (Distance.Get_distances(PosX, PosY - 1) == 0)
        {
            return Direction.down;
        }
        if (Distance.Get_distances(PosX + 1, PosY + 1) == 0)
        {
            return Direction.right_up;
        }
        if (Distance.Get_distances(PosX + 1, PosY - 1) == 0)
        {
            return Direction.right_down;
        }
        if (Distance.Get_distances(PosX - 1, PosY + 1) == 0)
        {
            return Direction.left_up;
        }
        if (Distance.Get_distances(PosX - 1, PosY - 1) == 0)
        {
            return Direction.left_down;
        }
        return 0;
    }
    
    //敵の移動時の処理
    void Move_Enemy(Direction dir)
    {
        DirectionChange(dir);
        Move_Dir(dir);
    }

    //ランダムな方向に移動する
    void Random_Move()
    {
        int i = UnityEngine.Random.Range(1, 9);
        switch (i)
        {
            case 1:
                if (isMove_Dir(Direction.right))
                {
                    Move_Enemy(Direction.right);
                }
                break;
            case 2:
                if (isMove_Dir(Direction.left))
                {
                    Move_Enemy(Direction.left);
                }
                break;
            case 3:
                if (isMove_Dir(Direction.up))
                {
                    Move_Enemy(Direction.up);
                }
                break;
            case 4:
                if (isMove_Dir(Direction.down))
                {
                    Move_Enemy(Direction.down);
                }
                break;
            case 5:
                if (isMove_Dir(Direction.right_up))
                {
                    Move_Enemy(Direction.right_up);
                }
                break;
            case 6:
                if (isMove_Dir(Direction.right_down))
                {
                    Move_Enemy(Direction.right_down);
                }
                break;
            case 7:
                if (isMove_Dir(Direction.left_up))
                {
                    Move_Enemy(Direction.left_up);
                }
                break;
            case 8:
                if (isMove_Dir(Direction.left_down))
                {
                    Move_Enemy(Direction.left_down);
                }
                break;
        }
    }

    public int Distance_to_Player()
    {
        return Distance.Get_distances(PosX, PosY);
    }

    void Attack_End()
    {
        GameMode.Mode_Change(GameMode.Game_Mode.enemy_attack_controller);
        ActionEnd();
    }

    public override void ActionStart()
    {
        action = true;
    }
    public override void ActionEnd()
    {
        action = false;
    }
}
