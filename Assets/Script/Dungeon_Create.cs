using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
ダンジョンの自動生成
 */
public class Dungeon_Create : MonoBehaviour
{
    public int Floor;  //階層
    int Dungeon_Width, Dungeon_Height;    //ダンジョンの横幅・縦幅
    int Max_Room_Width, Max_Room_Height, Min_Room_Width, Min_Room_Height; //部屋の最大と最小の横幅・縦幅
    const int Margin = 2;   //壁を作るための余白分のマス

    public int[,] Dungeon;   //ダンジョン内の各マスの情報(床か壁かなど)
    public Character_Status[,] All_MOB;  //ダンジョン内にいるプレイヤーと敵の位置情報
    List<Room> Areas = new List<Room>();  //マップを区画分けしたもの
    List<Room> Rooms = new List<Room>();  //区画ごとに生成された部屋

    //ダンジョン生成後に設置するもの達
    [SerializeField] GameObject yuka, wall;
    [SerializeField] GameObject enemy;
    GameObject player;
    [SerializeField] GameObject staris;
    [SerializeField] GameObject Item;

    [SerializeField] MiniMap MiniMap;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Floor);
        player = GameObject.Find("player");

        Load();
        Dungeon = new int[Dungeon_Width, Dungeon_Height];
        All_MOB = new Character_Status[Dungeon_Width, Dungeon_Height];
        MiniMap.Tiles = new SpriteRenderer[Dungeon_Width, Dungeon_Height];

        //全てのマスを壁にする
        for (int i = 0; i < Dungeon_Width; i++)
        {
            for (int j = 0; j < Dungeon_Height; j++)
            {
                Dungeon[i, j] = 0;
                All_MOB[i, j] = null;
            }
        }

        //マップ全体を最初の区画として設定
        Areas.Add(new Room(Dungeon_Width - 1, 0, Dungeon_Height - 1, 0));

        //区画を分けていく
        for (int i = 0; i < Areas.Count; i++)
        {
            while (Areas[i].width > Max_Room_Width + 2 * Margin || Areas[i].height > Max_Room_Height + 2 * Margin) //指定のサイズより大きければ分ける
            {
                Divide_Area(Areas[i]);
            }
        }

        //分けた区画ごとに部屋を作る
        for (int i = 0; i < Areas.Count; i++)
        {
            Create_Room(Areas[i]);
        }

        //作った部屋のマスを床にする
        for (int i = 0; i < Rooms.Count; i++)
        {
            Fill(Rooms[i]);
        }

        //各部屋の組み合わせについて通路を繋げることが可能なら繋げる
        for (int i = 0; i < Rooms.Count - 1; i++)
        {
            for (int j = i + 1; j < Rooms.Count; j++)
            {
                Connect_Room(i, j);
            }
        }

        //各マスのオブジェクトを生成
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                GameObject gameObject;
                if (Dungeon[i, j] == 1 || Dungeon[i, j] == 2)
                {
                    gameObject = Instantiate(yuka, new Vector2(i, j), Quaternion.identity);
                    MiniMap.Tiles[i, j] = gameObject.GetComponentsInChildren<SpriteRenderer>()[1];
                }
                else if (Dungeon[i, j] == 0)
                {
                    gameObject = Instantiate(wall, new Vector2(i, j), Quaternion.identity);
                    gameObject.GetComponent<Wall_Tile>().Create_Wall(Dungeon_3x3(i, j));
                }
            }
        }
        int x, y, r;    //ダンジョン内にランダムに敵，アイテム，プレイヤー，階段を設置する用の変数
        
        //各部屋に敵を1体ずつ設置する
        for (int i = 0; i < Rooms.Count; i++)
        {
            x = Random.Range(Rooms[i].left, Rooms[i].right + 1);
            y = Random.Range(Rooms[i].bottom, Rooms[i].top + 1);
            GameObject gameObject = Instantiate(enemy, new Vector2(x, y), Quaternion.identity);
            gameObject.GetComponent<Enemy_Status>().Character_Name = "enemy" + i;
            All_MOB[x, y] = gameObject.GetComponent<Enemy_Status>();
        }
        
        //アイテムを設置する
        for (int i = 0; i < Rooms.Count-6; i++)
        {
            float random = Random.value;
            x = Random.Range(Rooms[i].left, Rooms[i].right + 1);
            y = Random.Range(Rooms[i].bottom, Rooms[i].top + 1);
            if (random < 1)
            {
                GameObject gameObject = Instantiate(Item, new Vector2(x, y), Quaternion.identity);
                gameObject.GetComponent<Item_Object>().Initialization(1);
            }
            else if (random < 0.7)
            {
                GameObject gameObject = Instantiate(Item, new Vector2(x, y), Quaternion.identity);
                gameObject.GetComponent<Item_Object>().Initialization(2);
            }

        }

        //アイテムを設置する
        for (int i = 0; i < Rooms.Count - 10; i++)
        {
            float random = Random.value;
            x = Random.Range(Rooms[i].left, Rooms[i].right + 1);
            y = Random.Range(Rooms[i].bottom, Rooms[i].top + 1);
            if (random < 1)
            {
                GameObject gameObject = Instantiate(Item, new Vector2(x, y), Quaternion.identity);
                gameObject.GetComponent<Item_Object>().Initialization(101);
            }
            else if (random < 0.7)
            {
                GameObject gameObject = Instantiate(Item, new Vector2(x, y), Quaternion.identity);
                gameObject.GetComponent<Item_Object>().Initialization(2);
            }

        }

        //プレイヤーをランダムな部屋のランダムな位置に置く
        do
        {
            r = Random.Range(0, Rooms.Count);
            x = Random.Range(Rooms[r].left, Rooms[r].right + 1);
            y = Random.Range(Rooms[r].bottom, Rooms[r].top + 1);
        } while (Exist_MOB(x, y));
        player.transform.position = new Vector2(x, y);
        player.GetComponent<Player_Move>().TransformUpdate();
        All_MOB[x, y] = player.GetComponent<Player_Status>();

        //階段を1箇所設置する
        r = Random.Range(0, Rooms.Count);
        x = Random.Range(Rooms[r].left, Rooms[r].right + 1);
        y = Random.Range(Rooms[r].bottom, Rooms[r].top + 1);
        staris.transform.position = new Vector2(x, y);

        player.GetComponent<Player_Move>().ActionStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //csvファイルから値を読み取る
    void Load()
    {
        TextAsset csvFile;
        List<string[]> csvDatas = new List<string[]>();
        csvFile = Resources.Load("Dungeon/Dungeon") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        Dungeon_Width = int.Parse(csvDatas[1][0]);
        Dungeon_Height = int.Parse(csvDatas[1][1]);
        Max_Room_Width = int.Parse(csvDatas[1][2]);
        Max_Room_Height = int.Parse(csvDatas[1][3]);
        Min_Room_Width = int.Parse(csvDatas[1][4]);
        Min_Room_Height = int.Parse(csvDatas[1][5]);
    }

    //引数の区画を2つに分ける
    void Divide_Area(Room area)
    {
        int a, b, m;    //境界線の最小値，境界線の最大値，境界線の位置
        //横幅が大きければ左右に，縦幅が大きければ上下に分ける
        if (area.width > area.height)
        {
            //区画を分けたときに区画が小さくなりすぎないようにaとbを決める
            a = area.left + (2 * Margin + Min_Room_Width);
            b = area.right - (2 * Margin + Min_Room_Width);
            //区画を分ける境界線はaからbの間でランダムに決められたmになる
            m = a + Random.Range(0, b - a + 1);
            //境界線をもとに区画を2つに分ける
            Areas.Add(new Room(area.right, m + 1, area.top, area.bottom));
            area.Set(m, area.left, area.top, area.bottom);
        }
        else
        {
            a = area.bottom + (2 * Margin + Min_Room_Height);
            b = area.top - (2 * Margin + Min_Room_Height);
            m = a + Random.Range(0, b - a + 1);
            Areas.Add(new Room(area.right, area.left, area.top, m + 1));
            area.Set(area.right, area.left, m, area.bottom);
        }
    }

    //区画内に部屋を作る
    void Create_Room(Room area)
    {
        int l, r, b, t; //部屋の左端，右端，下端，上端
        //壁のための余白を設けて部屋の最小値を下回らないように値をランダムに決める
        l = Random.Range(area.left + Margin, area.right - (Min_Room_Width + Margin) + 1);
        r = Random.Range(l + Min_Room_Width - 1, area.right - Margin + 1);
        b = Random.Range(area.bottom + Margin, area.top - (Min_Room_Height + Margin) + 1);
        t = Random.Range(b + Min_Room_Height - 1, area.top - Margin + 1);
        //決めた値で部屋を作る
        Rooms.Add(new Room(r, l, t, b));
    }

    //部屋内のマスを床にする
    void Fill(Room room)
    {
        for (int i = room.left; i <= room.right; i++)
        {
            for (int j = room.bottom; j <= room.top; j++)
            {
                Dungeon[i, j] = 1;
            }
        }
    }


    //引数の2つの部屋について通路を繋げることが可能なら繋げる
    void Connect_Room(int RoomA, int RoomB)
    {
        int border, a, b;   //部屋の境界線，RoomAの通路の出入口，RoomBの通路の出入口

        //位置関係がRoomA左，RoomB右の場合
        if (Areas[RoomA].right == Areas[RoomB].left - 1)
        {
            //区画が上下に離れている場合は通路を生成しない
            if (Areas[RoomA].top <= Areas[RoomB].bottom || Areas[RoomA].bottom >= Areas[RoomB].top)
                return;
            border = Random.Range(Rooms[RoomA].right + Margin, Rooms[RoomB].left - Margin + 1); //境界線をAとBの間に引く
            a = Random.Range(Rooms[RoomA].bottom, Rooms[RoomA].top + 1);    //RoomAの通路の出入口を決定
            b = Random.Range(Rooms[RoomB].bottom, Rooms[RoomB].top + 1);    //RoomBの通路の出入口を決定
            //Aから境界線まで通路を引く
            for (int i = Rooms[RoomA].right + 1; i <= border; i++)
            {
                Dungeon[i, a] = 2;
            }
            //Bから境界線まで通路を引く
            for (int i = Rooms[RoomB].left - 1; i >= border; i--)
            {
                Dungeon[i, b] = 2;
            }
            //Aから引いた通路とBから引いた通路を繋げる
            if (a > b)
            {
                for (int i = b + 1; i <= a; i++)
                {
                    Dungeon[border, i] = 2;
                }
            }
            else
            {
                for (int i = a + 1; i <= b; i++)
                {
                    Dungeon[border, i] = 2;
                }
            }
        }
        //位置関係がRoomA右，RoomB左の場合
        else if (Areas[RoomA].left - 1 == Areas[RoomB].right)
        {
            //区画が上下に離れている場合は通路を生成しない
            if (Areas[RoomA].top <= Areas[RoomB].bottom || Areas[RoomA].bottom >= Areas[RoomB].top)
                return;
            border = Random.Range(Rooms[RoomB].right + Margin, Rooms[RoomA].left - Margin + 1);
            a = Random.Range(Rooms[RoomA].bottom, Rooms[RoomA].top + 1);
            b = Random.Range(Rooms[RoomB].bottom, Rooms[RoomB].top + 1);
            for (int i = Rooms[RoomA].left - 1; i >= border; i--)
            {
                Dungeon[i, a] = 2;
            }
            for (int i = Rooms[RoomB].right + 1; i <= border; i++)
            {
                Dungeon[i, b] = 2;
            }
            if (a > b)
            {
                for (int i = b + 1; i <= a; i++)
                {
                    Dungeon[border, i] = 2;
                }
            }
            else
            {
                for (int i = a + 1; i <= b; i++)
                {
                    Dungeon[border, i] = 2;
                }
            }
        }
        //位置関係がRoomA下，RoomB上の場合
        else if (Areas[RoomA].top == Areas[RoomB].bottom - 1)
        {
            //区画が左右に離れている場合は通路を生成しない
            if (Areas[RoomA].right <= Areas[RoomB].left || Areas[RoomA].left >= Areas[RoomB].right)
                return;
            a = Random.Range(Rooms[RoomA].left, Rooms[RoomA].right + 1);
            b = Random.Range(Rooms[RoomB].left, Rooms[RoomB].right + 1);
            border = Random.Range(Rooms[RoomA].top + Margin, Rooms[RoomB].bottom - Margin + 1);
            for (int i = Rooms[RoomA].top + 1; i <= border; i++)
            {
                Dungeon[a, i] = 2;
            }
            for (int i = Rooms[RoomB].bottom - 1; i >= border; i--)
            {
                Dungeon[b, i] = 2;
            }
            if (a > b)
            {
                for (int i = b + 1; i <= a; i++)
                {
                    Dungeon[i, border] = 2;
                }
            }
            else
            {
                for (int i = a + 1; i <= b; i++)
                {
                    Dungeon[i, border] = 2;
                }
            }
        }
        //位置関係がRoomA上，RoomB下の場合
        else if (Areas[RoomA].bottom - 1 == Areas[RoomB].top)
        {
            //区画が左右に離れている場合は通路を生成しない
            if (Areas[RoomA].right <= Areas[RoomB].left || Areas[RoomA].left >= Areas[RoomB].right)
                return;
            border = Random.Range(Rooms[RoomB].top + Margin, Rooms[RoomA].bottom - Margin + 1);
            a = Random.Range(Rooms[RoomA].left, Rooms[RoomA].right + 1);
            b = Random.Range(Rooms[RoomB].left, Rooms[RoomB].right + 1);
            for (int i = Rooms[RoomA].bottom - 1; i >= border; i--)
            {
                Dungeon[a, i] = 2;
            }
            for (int i = Rooms[RoomB].top + 1; i <= border; i++)
            {
                Dungeon[b, i] = 2;
            }
            if (a > b)
            {
                for (int i = b + 1; i <= a; i++)
                {
                    Dungeon[i, border] = 2;
                }
            }
            else
            {
                for (int i = a + 1; i <= b; i++)
                {
                    Dungeon[i, border] = 2;
                }
            }
        }
    }

    //(x,y)を中心とした3×3のマスの情報を返す
    int[,] Dungeon_3x3(int x, int y)
    {
        int[,] Grid_3x3 = new int[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (x + i - 1 < 0 || x + i - 1 >= Dungeon_Width || y + j - 1 < 0 || y + j - 1 >= Dungeon_Height)
                    Grid_3x3[i, j] = 0; //配列の範囲外の場合は0にする
                else
                    Grid_3x3[i, j] = Dungeon[x + i - 1, y + j - 1];
            }
        }
        return Grid_3x3;
    }

    //引数の座標にMOBがいるか
    public bool Exist_MOB(int x, int y)
    {
        if (All_MOB[x, y] != null)
            return true;
        else
            return false;
    }

    //特定座標に壁があるか
    public bool WallCheck(int x, int y)
    {
        if (Dungeon[x, y] == 0)
            return true;
        else
            return false;
    }

    //アイテム生成
    void Item_Create(int floor)
    {
        TextAsset csvFile;
        List<string[]> csvDatas = new List<string[]>();
        csvFile = Resources.Load("Dungeon/Item_Pop") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        for(int i = 0; i < floor * 2 + 2; i++)
        {
          string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        for (int i = 1; i < csvDatas[csvDatas.Count - 1].Length; i++)
        {
            if (csvDatas[csvDatas.Count - 2][i] != "")
            {
               Debug.Log("アイテムID" + int.Parse(csvDatas[csvDatas.Count - 2][i]));
                Debug.Log("確率"+int.Parse(csvDatas[csvDatas.Count - 1][i]));
            }
        }
    }

    public int DungeonWidth
    {
        get { return Dungeon_Width; }
    }
    
    public int DungeonHeight
    {
        get { return Dungeon_Height; }
    }
    public int MaxRoomHeight
    {
        get { return Max_Room_Width; }
    }
    public int MaxRoomWidth
    {
        get { return Max_Room_Height; }
    }
    public int MinRoomWidth
    {
        get { return Min_Room_Width; }
    }
    public int MinRoomHeight
    {
        get { return Min_Room_Height; }
    }
}
