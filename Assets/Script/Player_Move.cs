using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
�v���C���[�̋���
 */
public class Player_Move : Character_Move
{
    Menu Menu;
    [SerializeField] belongings belongings;
    Red_Panel Red_Panel;
    [SerializeField]MiniMap MiniMap;
    

    int AutoHeal_Count = 0; //�����񕜗p�̃J�E���g

    void Start()
    {
        Red_Panel = GameObject.Find("Red_Panel").GetComponent<Red_Panel>();
        Menu = GameObject.Find("Menu").GetComponent<Menu>();

        DirectionChange(Direction.down);
    }

    //�V�����K�w�ɂȂ������̏���
    public void Load()
    {
        GameObject gameObject = GameObject.Find("GameController");
        GameMode = gameObject.GetComponent<GameMode>();
        Distance = gameObject.GetComponent<Distance>();
        Dungeon_Create = gameObject.GetComponent<Dungeon_Create>();
        Move_Controller = gameObject.GetComponent<Move_Controller>();
        MiniMap = gameObject.GetComponent<MiniMap>();
        Red_Panel = GameObject.Find("Red_Panel").GetComponent<Red_Panel>();
        Menu = GameObject.Find("Menu").GetComponent<Menu>();
        Log = GameObject.Find("Log").GetComponent<Log>();

    }
    void Update()
    {
        //HP���Ȃ��Ȃ�����Q�[���I�[�o�[
        if (Status.NowHP<=0)
        {
            Destroy(this.gameObject);
            GameMode.Mode_Change(GameMode.Game_Mode.game_over);
        }
        if (!action)
        {
            if (GameMode.Gamemode == GameMode.Game_Mode.operabale)
                GameMode.Mode_Change(GameMode.Game_Mode.enemy_controller);
            return;
        }
        if (GameMode.Gamemode == GameMode.Game_Mode.operabale)
        {
            //�㉺���E�ړ�
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (isMove_Dir(Direction.right))
                    {
                        DirectionChange(Direction.right);
                        Move();
                        return;
                    }

                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (isMove_Dir(Direction.left))
                    {
                        DirectionChange(Direction.left);
                        Move();
                        return;
                    }
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    if (isMove_Dir(Direction.up))
                    {
                        DirectionChange(Direction.up);
                        Move();
                        return;
                    }
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (isMove_Dir(Direction.down))
                    {
                        DirectionChange(Direction.down);
                        Move();
                        return;
                    }
                }
            }

            //�΂߈ړ����[�h
            else
            {
                if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
                {
                    if (isMove_Dir(Direction.right_up))
                    {
                        DirectionChange(Direction.right_up);
                        Move();
                        return;
                    }
                }
                else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
                {
                    if (isMove_Dir(Direction.right_down))
                    {
                        DirectionChange(Direction.right_down);
                        Move();
                        return;
                    }
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
                {
                    if (isMove_Dir(Direction.left_up))
                    {
                        DirectionChange(Direction.left_up);
                        Move();
                        return;
                    }
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
                {
                    if (isMove_Dir(Direction.left_down))
                    {
                        DirectionChange(Direction.left_down);
                        Move();
                        return;
                    }
                }
            }
            //�΂߂̕����]��
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (direction == Direction.right_up || direction == Direction.right)
                {
                    DirectionChange(Direction.right_down);
                    Red_Panel.Display_Pnale((int)transform.position.x + 1, (int)transform.position.y - 1);
                }
                else if (direction == Direction.right_down || direction == Direction.down)
                {
                    DirectionChange(Direction.left_down);
                    Red_Panel.Display_Pnale((int)transform.position.x - 1, (int)transform.position.y - 1);
                }
                else if (direction == Direction.left_down || direction == Direction.left)
                {
                    DirectionChange(Direction.left_up);
                    Red_Panel.Display_Pnale((int)transform.position.x - 1, (int)transform.position.y + 1);
                }
                else if (direction == Direction.left_up || direction == Direction.up)
                {
                    DirectionChange(Direction.right_up);
                    Red_Panel.Display_Pnale((int)transform.position.x + 1, (int)transform.position.y + 1);
                }
                return;
            }
            //�㉺���E�̕����]��
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (direction == Direction.right_up || direction == Direction.right)
                {
                    DirectionChange(Direction.down);
                    Red_Panel.Display_Pnale((int)transform.position.x, (int)transform.position.y - 1);
                }
                else if (direction == Direction.right_down || direction == Direction.down)
                {
                    DirectionChange(Direction.left);
                    Red_Panel.Display_Pnale((int)transform.position.x - 1, (int)transform.position.y);
                }
                else if (direction == Direction.left_down || direction == Direction.left)
                {
                    DirectionChange(Direction.up);
                    Red_Panel.Display_Pnale((int)transform.position.x, (int)transform.position.y + 1);
                }
                else if (direction == Direction.left_up || direction == Direction.up)
                {
                    DirectionChange(Direction.right);
                    Red_Panel.Display_Pnale((int)transform.position.x + 1, (int)transform.position.y);
                }
                return;
            }

            //�U��
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                GameMode.Mode_Change(GameMode.Game_Mode.attack);
                return;
            }
            //�A�C�e�������J��
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Menu.Open_Menu();
                GameMode.Mode_Change(GameMode.Game_Mode.menu);
                return;
            }
        }

        //�ړ���
        else if (GameMode.Gamemode == GameMode.Game_Mode.moving||GameMode.Gamemode==GameMode.Game_Mode.only_player_moving)
        {
            if (Moving())   //�ړ����I��������
            {
                if (Item_Check())
                {
                    GameMode.Mode_Change(GameMode.Game_Mode.enemy_controller);
                }
                if (Stairs_Check())
                {
                    GameMode.Mode_Change(GameMode.Game_Mode.stairs);
                }
                ActionEnd();
            }
        }
        
    }

    //�v���C���[�ړ����̏���
    void Move()
    {
        Move_Dir(direction);
        Distance.Dis_Update(PosX, PosY);
        
        if (Stairs_Check(false) || Item_Check(false))
        {
            GameMode.Mode_Change(GameMode.Game_Mode.only_player_moving);
        }
        else
        {
            GameMode.Mode_Change(GameMode.Game_Mode.enemy_controller);
        }
    }

    //�ړ����ɃA�C�e�������ɗ����Ă邩
    bool Item_Check(bool get=true)
    {
        Ray ray = new Ray(new Vector2(PosX, PosY), new Vector3(0, 0, 1));
        int layerMask = LayerMask.GetMask(new string[] { "item" });
        RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        //�A�C�e������������E��
        if (hit2d)
        {
            if (!get)
                return true;
            if (belongings.Item_Get(hit2d.collider.gameObject.GetComponent<Item_Object>().Item))
            {
                Log.Add_Log(hit2d.collider.gameObject.GetComponent<Item_Object>().Item.Name + "���E����");
                Destroy(hit2d.collider.gameObject);
            }
            return true;
        }
        return false;
    }

    //�����ɊK�i�����邩�̃`�F�b�N
    bool Stairs_Check(bool use=true)
    {
        Ray ray = new Ray(new Vector2(PosX, PosY), new Vector3(0, 0, 1));
        int layerMask = LayerMask.GetMask(new string[] { "stairs" });
        RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
        if (hit2d)
        {
            if (!use)
                return true;
            GameMode.Mode_Change(GameMode.Game_Mode.stairs);
            return true;
        }
        return false;
    }

    //�U���A�j���[�V�����I�����̏���
    void Attack_End()
    {
        GameMode.Mode_Change(GameMode.Game_Mode.enemy_controller);
        ActionEnd();
    }

    //�s���J�n���̏���
    public override void ActionStart()
    {
        Auto_Heal();
        action = true;
        MiniMap.MiniMap_Update(PosX, PosY);
    }

    //�s���I�����̏���
    public override void ActionEnd()
    {
        action = false;
    }


    //������
    void Auto_Heal()
    {
        AutoHeal_Count++;
        if (AutoHeal_Count >= 10)
        {
            if (Status.NowHP < Status.MaxHP)
                Status.NowHP++;
            AutoHeal_Count = 0;
        }
    }
}
