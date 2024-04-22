using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
落ちてるアイテムに持たせるプログラム
 */
public class Item_Object : MonoBehaviour
{
    public Item Item;   //アイテムの情報
    [SerializeField] int ItemID;    //何のアイテムなのかを表す
    int MoveX = 0, MoveY = 0;
    int GoalX, GoalY;
    int move_count, Move_interval=3;
    bool isThrowing = false;
    ItemIDList ItemIDList;
    Dungeon_Create Dungeon_Create;
    GameMode GameMode;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (GameMode.Gamemode != GameMode.Game_Mode.throwing)
            return;
        if (!isThrowing)
            return;
        if (throwing())
        {
            if (Dungeon_Create.Exist_MOB((int)transform.position.x, (int)transform.position.y))
            {
                if (Item.item_type == Item_Type.bottle)
                {
                    Ray ray = new Ray(new Vector2(transform.position.x, transform.position.y), new Vector3(0, 0, 1));
                    int layerMask = LayerMask.GetMask(new string[] { "enemy" });
                    RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
                    if (hit2d)
                    {
                        Bottle bottle = (Bottle)Item;
                        bottle.Use_Bottle(hit2d.collider.gameObject.GetComponent<Enemy_Status>());
                    }
                    Destroy(this.gameObject);
                }
                GameMode.Mode_Change(GameMode.Game_Mode.enemy_controller);
                isThrowing = false;
            }
            else
            {
                throw_OneBlock(MoveX, MoveY);
            }
        }
    }

    //アイテム生成時の処理
    public void Initialization(int _ItemID)
    {
        ItemID = _ItemID;
        ItemIDList = GameObject.Find("GameController").GetComponent<ItemIDList>();
        GameMode = GameObject.Find("GameController").GetComponent<GameMode>();
        Item = ItemIDList.Creat_Item(ItemID);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Item.Item_Image;
    }

    //投げられるときの処理
    public void Throw(Direction direction)
    {
        Dungeon_Create = GameObject.Find("GameController").GetComponent<Dungeon_Create>();
        GameMode.Mode_Change(GameMode.Game_Mode.throwing);
        switch (direction)
        {
            case Direction.right:
                throw_OneBlock(1, 0);
                break;
            case Direction.left:
                throw_OneBlock(-1, 0);
                break;
            case Direction.up:
                throw_OneBlock(0, 1);
                break;
            case Direction.down:
                throw_OneBlock(0, -1);
                break;
            case Direction.right_up:
                throw_OneBlock(1, 1);
                break;
            case Direction.right_down:
                throw_OneBlock(1, -1);
                break;
            case Direction.left_up:
                throw_OneBlock(-1, 1);
                break;
            case Direction.left_down:
                throw_OneBlock(-1, -1);
                break;
        }
    }

    //投げられるときの1ブロック分の処理
    void throw_OneBlock(int x,int y)
    {
        if (Dungeon_Create.WallCheck((int)transform.position.x + x, (int)transform.position.y + y)
         || Dungeon_Create.WallCheck((int)transform.position.x + x, (int)transform.position.y )
         || Dungeon_Create.WallCheck((int)transform.position.x    , (int)transform.position.y + y))
        {
            GameMode.Mode_Change(GameMode.Game_Mode.enemy_controller);
            isThrowing = false;
            return;
        }
        move_count = 0;
        GoalX = (int)transform.position.x + x;
        GoalY = (int)transform.position.y + y;
        MoveX = x;
        MoveY = y;
        GameMode.Mode_Change(GameMode.Game_Mode.throwing);
        isThrowing = true;
    }

    //投げられ中の処理
    bool throwing()
    {
        transform.Translate(MoveX / Move_interval, MoveY / Move_interval, 0);
        move_count++;
        if (move_count >= Move_interval)
        {
            transform.position = new Vector2(GoalX, GoalY);
            return true;
        }
        return false;
    }
}
