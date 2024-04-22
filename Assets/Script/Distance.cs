using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
各マスにおけるプレイヤーまでの移動距離を計算する
 */
public class Distance : MonoBehaviour
{
    int[,] distances; //各マスのプレイヤーまでの移動距離
    Transform player;
    [SerializeField] Dungeon_Create Dungeon_Create;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").transform;
        distances = new int[Dungeon_Create.DungeonWidth, Dungeon_Create.DungeonHeight];
        Dis_Initialization((int)player.position.x, (int)player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //特定座標に地面のマスがあるか
    bool YukaCheck(int x, int y)
    {
        if (Dungeon_Create.Dungeon[x, y] >0)
            return true;
        else
            return false;
    }

    //距離更新のための初期化
    void Dis_Initialization(int x,int y)
    {
        for(int i = 0; i < Dungeon_Create.DungeonWidth; i++)
        {
            for(int j = 0; j < Dungeon_Create.DungeonHeight; j++)
            {
                if (YukaCheck(i, j))
                {
                    distances[i, j] = -1;
                }
                else
                {
                    distances[i, j] = -100;
                }
            }
        }
        Dis_Update(x, y);
    }

    //距離の更新
    public void Dis_Update(int x,int y)
    {
        for (int i = 0; i < Dungeon_Create.DungeonWidth; i++)
        {
            for (int j = 0; j < Dungeon_Create.DungeonHeight; j++)
            {
                if (distances[i, j] >= 0)
                {
                    distances[i, j] = -1;
                }
            }
        }
        distances[x, y] = 0;
        Around_Dis(x, y);
        //Debug.Log(distances[x+3, y+3]);
    }

    //周りの距離を更新
    void Around_Dis(int x, int y)
    {
        //上下左右
        if (distances[x + 1, y] == -1 || distances[x + 1, y] > distances[x, y] + 1)
        {
            distances[x + 1, y] = distances[x, y] + 1;
            Around_Dis(x + 1, y);
        }
        if (distances[x - 1, y] == -1 || distances[x - 1, y] > distances[x, y] + 1)
        {
            distances[x - 1, y] = distances[x, y] + 1;
            Around_Dis(x - 1, y);
        }
        if (distances[x, y + 1] == -1 || distances[x, y + 1] > distances[x, y] + 1)
        {
            distances[x, y + 1] = distances[x, y] + 1;
            Around_Dis(x, y + 1);
        }
        if (distances[x, y - 1] == -1 || distances[x, y - 1] > distances[x, y] + 1)
        {
            distances[x, y - 1] = distances[x, y] + 1;
            Around_Dis(x, y - 1);
        }
        //斜め方向
        if (distances[x + 1, y + 1] == -1 || distances[x + 1, y + 1] > distances[x, y] + 1)
        {
            if (distances[x + 1, y] != -100 && distances[x, y + 1] != -100)
            {
                distances[x + 1, y + 1] = distances[x, y] + 1;
                Around_Dis(x + 1, y + 1);
            }
        }
        if (distances[x + 1, y - 1] == -1 || distances[x + 1, y - 1] > distances[x, y] + 1)
        {
            if (distances[x + 1, y] != -100 && distances[x, y - 1] != -100)
            {
                distances[x + 1, y - 1] = distances[x, y] + 1;
                Around_Dis(x + 1, y - 1);
            }
        }
        if (distances[x - 1, y + 1] == -1 || distances[x - 1, y + 1] > distances[x, y] + 1)
        {
            if (distances[x - 1, y] != -100 && distances[x, y + 1] != -100)
            {
                distances[x - 1, y + 1] = distances[x, y] + 1;
                Around_Dis(x - 1, y + 1);
            }
        }
        if (distances[x - 1, y - 1] == -1 || distances[x - 1, y - 1] > distances[x, y] + 1)
        {
            if (distances[x - 1, y] != -100 && distances[x, y - 1] != -100)
            {
                distances[x - 1, y - 1] = distances[x, y] + 1;
                Around_Dis(x - 1, y - 1);
            }
        }
    }

    public int Get_distances(int x,int y)
    {
        return distances[x, y];
    }
}
