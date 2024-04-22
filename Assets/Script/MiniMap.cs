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
        //�ړ���̃}�X�������̏��̏ꍇ
        if (Dungeon_Create.Dungeon[Player_x, Player_y] == 1)
        {
            Update_Room(Player_x, Player_y);
            Visible.enabled = false;
        }
            
        
        //�ړ���̃}�X���ʘH�̏��̏ꍇ
        else if(Dungeon_Create.Dungeon[Player_x, Player_y] == 2)
        {
            Update_Road(Player_x, Player_y);
            Visible.enabled = true;
        }
    }

    //�����X�V�p
    void Update_Room(int x, int y)
    {
        Update_Mas(x + 1, y,true);
        Update_Mas(x - 1, y,true);
        Update_Mas(x, y + 1,true);
        Update_Mas(x, y - 1,true);
    }

    //�����1�}�X���X�V
    void Update_Mas(int x,int y,bool Room)
    {
        //�}�b�v�͈̔͊O���m�F
        if (x < 0 || x > Dungeon_Create.DungeonWidth - 1 || y < 0 || y > Dungeon_Create.DungeonHeight - 1)
            return;
        //�}�X����Ȃ��Ƃ��납�m�F
        if (Dungeon_Create.Dungeon[x, y] == 0)
            return;
        //�X�V�ς݂��m�F
        if (Check[x, y])
            return;
        Check[x, y] = true;

        //���̃}�X�������̏�
        if (Dungeon_Create.Dungeon[x , y] == 1)
        {
            Tiles[x, y].enabled = true; //�~�j�}�b�v��ɕ\��

            if (Room)
                Update_Room(x, y);    //�����̏��}�X�͘A���I�ɕ\��������
        }
        //���̃}�X���ʘH�̏�
        else if (Dungeon_Create.Dungeon[x, y] == 2)
        {
            Tiles[x, y].enabled = true; //�~�j�}�b�v��ɕ\��
        }

        if (Dungeon_Create.All_MOB[x, y]!=null)
        {
            //�G��������\��
            if (Dungeon_Create.All_MOB[x, y].GetType() == typeof(Enemy_Status))
                Dungeon_Create.All_MOB[x, y].gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        }
        //�A�C�e�������邩�̊m�F
        Ray ray_i = new Ray(new Vector2(x, y), new Vector3(0, 0, 1));
        int layerMask_i = LayerMask.GetMask(new string[] { "item" });
        RaycastHit2D hit2d_i = Physics2D.Raycast(ray_i.origin, ray_i.direction, Mathf.Infinity, layerMask_i);
        if (hit2d_i)
        {
            hit2d_i.collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    //�ʘH�X�V�p
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
