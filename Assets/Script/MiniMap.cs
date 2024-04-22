using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] Dungeon_Create Dungeon_Create;
    [SerializeField] EnemyController EnemyController;

    public SpriteRenderer[,] Tiles;
    bool[,] Check;

    [SerializeField] SpriteRenderer Visible;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MiniMap_Update(int Player_x,int Player_y)
    {
        EnemyController.SpriteRenderer_Reset();
        Check = new bool[Dungeon_Create.DungeonWidth, Dungeon_Create.DungeonHeight];
        //移動後のマスが部屋の床の場合
        if (Dungeon_Create.Dungeon[Player_x, Player_y] == 1)
        {
            Update_Room(Player_x, Player_y);
            Visible.enabled = false;
        }
            
        
        //移動後のマスが通路の床の場合
        else if(Dungeon_Create.Dungeon[Player_x, Player_y] == 2)
        {
            Update_Road(Player_x, Player_y);
            Visible.enabled = true;
        }
    }

    //部屋更新用
    void Update_Room(int x, int y)
    {
        Update_Mas(x + 1, y,true);
        Update_Mas(x - 1, y,true);
        Update_Mas(x, y + 1,true);
        Update_Mas(x, y - 1,true);
    }

    //特定の1マスを更新
    void Update_Mas(int x,int y,bool Room)
    {
        //マップの範囲外か確認
        if (x < 0 || x > Dungeon_Create.DungeonWidth - 1 || y < 0 || y > Dungeon_Create.DungeonHeight - 1)
            return;
        //マスじゃないところか確認
        if (Dungeon_Create.Dungeon[x, y] == 0)
            return;
        //更新済みか確認
        if (Check[x, y])
            return;
        Check[x, y] = true;

        //このマスが部屋の床
        if (Dungeon_Create.Dungeon[x , y] == 1)
        {
            Tiles[x, y].enabled = true; //ミニマップ上に表示

            if (Room)
                Update_Room(x, y);    //部屋の床マスは連鎖的に表示させる
        }
        //このマスが通路の床
        else if (Dungeon_Create.Dungeon[x, y] == 2)
        {
            Tiles[x, y].enabled = true; //ミニマップ上に表示
        }

        if (Dungeon_Create.All_MOB[x, y]!=null)
        {
            //敵がいたら表示
            if (Dungeon_Create.All_MOB[x, y].GetType() == typeof(Enemy_Status))
                Dungeon_Create.All_MOB[x, y].gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        }
        //アイテムがあるかの確認
        Ray ray_i = new Ray(new Vector2(x, y), new Vector3(0, 0, 1));
        int layerMask_i = LayerMask.GetMask(new string[] { "item" });
        RaycastHit2D hit2d_i = Physics2D.Raycast(ray_i.origin, ray_i.direction, Mathf.Infinity, layerMask_i);
        if (hit2d_i)
        {
            hit2d_i.collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    //通路更新用
    void Update_Road(int x, int y)
    {
        Update_Mas(x + 1, y + 1, false);
        Update_Mas(x + 1, y, false);
        Update_Mas(x + 1, y - 1, false);
        Update_Mas(x, y + 1, false);
        Update_Mas(x, y - 1, false);
        Update_Mas(x - 1, y + 1, false);
        Update_Mas(x - 1, y, false);
        Update_Mas(x - 1, y - 1, false);
    }

}
