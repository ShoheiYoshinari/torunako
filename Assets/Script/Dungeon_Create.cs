using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
�_���W�����̎�������
 */
public class Dungeon_Create : MonoBehaviour
{
    public int Floor;  //�K�w
    int Dungeon_Width, Dungeon_Height;    //�_���W�����̉����E�c��
    int Max_Room_Width, Max_Room_Height, Min_Room_Width, Min_Room_Height; //�����̍ő�ƍŏ��̉����E�c��
    const int Margin = 2;   //�ǂ���邽�߂̗]�����̃}�X

    public int[,] Dungeon;   //�_���W�������̊e�}�X�̏��(�����ǂ��Ȃ�)
    public Character_Status[,] All_MOB;  //�_���W�������ɂ���v���C���[�ƓG�̈ʒu���
    List<Room> Areas = new List<Room>();  //�}�b�v����敪����������
    List<Room> Rooms = new List<Room>();  //��悲�Ƃɐ������ꂽ����

    //�_���W����������ɐݒu������̒B
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

        //�S�Ẵ}�X��ǂɂ���
        for (int i = 0; i < Dungeon_Width; i++)
        {
            for (int j = 0; j < Dungeon_Height; j++)
            {
                Dungeon[i, j] = 0;
                All_MOB[i, j] = null;
            }
        }

        //�}�b�v�S�̂��ŏ��̋��Ƃ��Đݒ�
        Areas.Add(new Room(Dungeon_Width - 1, 0, Dungeon_Height - 1, 0));

        //���𕪂��Ă���
        for (int i = 0; i < Areas.Count; i++)
        {
            while (Areas[i].width > Max_Room_Width + 2 * Margin || Areas[i].height > Max_Room_Height + 2 * Margin) //�w��̃T�C�Y���傫����Ε�����
            {
                Divide_Area(Areas[i]);
            }
        }

        //��������悲�Ƃɕ��������
        for (int i = 0; i < Areas.Count; i++)
        {
            Create_Room(Areas[i]);
        }

        //����������̃}�X�����ɂ���
        for (int i = 0; i < Rooms.Count; i++)
        {
            Fill(Rooms[i]);
        }

        //�e�����̑g�ݍ��킹�ɂ��ĒʘH���q���邱�Ƃ��\�Ȃ�q����
        for (int i = 0; i < Rooms.Count - 1; i++)
        {
            for (int j = i + 1; j < Rooms.Count; j++)
            {
                Connect_Room(i, j);
            }
        }

        //�e�}�X�̃I�u�W�F�N�g�𐶐�
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
        int x, y, r;    //�_���W�������Ƀ����_���ɓG�C�A�C�e���C�v���C���[�C�K�i��ݒu����p�̕ϐ�
        
        //�e�����ɓG��1�̂��ݒu����
        for (int i = 0; i < Rooms.Count; i++)
        {
            x = Random.Range(Rooms[i].left, Rooms[i].right + 1);
            y = Random.Range(Rooms[i].bottom, Rooms[i].top + 1);
            GameObject gameObject = Instantiate(enemy, new Vector2(x, y), Quaternion.identity);
            gameObject.GetComponent<Enemy_Status>().Character_Name = "enemy" + i;
            All_MOB[x, y] = gameObject.GetComponent<Enemy_Status>();
        }
        
        //�A�C�e����ݒu����
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

        //�A�C�e����ݒu����
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

        //�v���C���[�������_���ȕ����̃����_���Ȉʒu�ɒu��
        do
        {
            r = Random.Range(0, Rooms.Count);
            x = Random.Range(Rooms[r].left, Rooms[r].right + 1);
            y = Random.Range(Rooms[r].bottom, Rooms[r].top + 1);
        } while (Exist_MOB(x, y));
        player.transform.position = new Vector2(x, y);
        player.GetComponent<Player_Move>().TransformUpdate();
        All_MOB[x, y] = player.GetComponent<Player_Status>();

        //�K�i��1�ӏ��ݒu����
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

    //csv�t�@�C������l��ǂݎ��
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

    //�����̋���2�ɕ�����
    void Divide_Area(Room area)
    {
        int a, b, m;    //���E���̍ŏ��l�C���E���̍ő�l�C���E���̈ʒu
        //�������傫����΍��E�ɁC�c�����傫����Ώ㉺�ɕ�����
        if (area.width > area.height)
        {
            //���𕪂����Ƃ��ɋ�悪�������Ȃ肷���Ȃ��悤��a��b�����߂�
            a = area.left + (2 * Margin + Min_Room_Width);
            b = area.right - (2 * Margin + Min_Room_Width);
            //���𕪂��鋫�E����a����b�̊ԂŃ����_���Ɍ��߂�ꂽm�ɂȂ�
            m = a + Random.Range(0, b - a + 1);
            //���E�������Ƃɋ���2�ɕ�����
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

    //�����ɕ��������
    void Create_Room(Room area)
    {
        int l, r, b, t; //�����̍��[�C�E�[�C���[�C��[
        //�ǂ̂��߂̗]����݂��ĕ����̍ŏ��l�������Ȃ��悤�ɒl�������_���Ɍ��߂�
        l = Random.Range(area.left + Margin, area.right - (Min_Room_Width + Margin) + 1);
        r = Random.Range(l + Min_Room_Width - 1, area.right - Margin + 1);
        b = Random.Range(area.bottom + Margin, area.top - (Min_Room_Height + Margin) + 1);
        t = Random.Range(b + Min_Room_Height - 1, area.top - Margin + 1);
        //���߂��l�ŕ��������
        Rooms.Add(new Room(r, l, t, b));
    }

    //�������̃}�X�����ɂ���
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


    //������2�̕����ɂ��ĒʘH���q���邱�Ƃ��\�Ȃ�q����
    void Connect_Room(int RoomA, int RoomB)
    {
        int border, a, b;   //�����̋��E���CRoomA�̒ʘH�̏o�����CRoomB�̒ʘH�̏o����

        //�ʒu�֌W��RoomA���CRoomB�E�̏ꍇ
        if (Areas[RoomA].right == Areas[RoomB].left - 1)
        {
            //��悪�㉺�ɗ���Ă���ꍇ�͒ʘH�𐶐����Ȃ�
            if (Areas[RoomA].top <= Areas[RoomB].bottom || Areas[RoomA].bottom >= Areas[RoomB].top)
                return;
            border = Random.Range(Rooms[RoomA].right + Margin, Rooms[RoomB].left - Margin + 1); //���E����A��B�̊ԂɈ���
            a = Random.Range(Rooms[RoomA].bottom, Rooms[RoomA].top + 1);    //RoomA�̒ʘH�̏o����������
            b = Random.Range(Rooms[RoomB].bottom, Rooms[RoomB].top + 1);    //RoomB�̒ʘH�̏o����������
            //A���狫�E���܂ŒʘH������
            for (int i = Rooms[RoomA].right + 1; i <= border; i++)
            {
                Dungeon[i, a] = 2;
            }
            //B���狫�E���܂ŒʘH������
            for (int i = Rooms[RoomB].left - 1; i >= border; i--)
            {
                Dungeon[i, b] = 2;
            }
            //A����������ʘH��B����������ʘH���q����
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
        //�ʒu�֌W��RoomA�E�CRoomB���̏ꍇ
        else if (Areas[RoomA].left - 1 == Areas[RoomB].right)
        {
            //��悪�㉺�ɗ���Ă���ꍇ�͒ʘH�𐶐����Ȃ�
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
        //�ʒu�֌W��RoomA���CRoomB��̏ꍇ
        else if (Areas[RoomA].top == Areas[RoomB].bottom - 1)
        {
            //��悪���E�ɗ���Ă���ꍇ�͒ʘH�𐶐����Ȃ�
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
        //�ʒu�֌W��RoomA��CRoomB���̏ꍇ
        else if (Areas[RoomA].bottom - 1 == Areas[RoomB].top)
        {
            //��悪���E�ɗ���Ă���ꍇ�͒ʘH�𐶐����Ȃ�
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

    //(x,y)�𒆐S�Ƃ���3�~3�̃}�X�̏���Ԃ�
    int[,] Dungeon_3x3(int x, int y)
    {
        int[,] Grid_3x3 = new int[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (x + i - 1 < 0 || x + i - 1 >= Dungeon_Width || y + j - 1 < 0 || y + j - 1 >= Dungeon_Height)
                    Grid_3x3[i, j] = 0; //�z��͈̔͊O�̏ꍇ��0�ɂ���
                else
                    Grid_3x3[i, j] = Dungeon[x + i - 1, y + j - 1];
            }
        }
        return Grid_3x3;
    }

    //�����̍��W��MOB�����邩
    public bool Exist_MOB(int x, int y)
    {
        if (All_MOB[x, y] != null)
            return true;
        else
            return false;
    }

    //������W�ɕǂ����邩
    public bool WallCheck(int x, int y)
    {
        if (Dungeon[x, y] == 0)
            return true;
        else
            return false;
    }

    //�A�C�e������
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
               Debug.Log("�A�C�e��ID" + int.Parse(csvDatas[csvDatas.Count - 2][i]));
                Debug.Log("�m��"+int.Parse(csvDatas[csvDatas.Count - 1][i]));
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
