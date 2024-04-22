using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
アイテムメニューの表示
 */
public class Item_Menu : MonoBehaviour
{
    enum ItemMenu_Mode
    {
        select,
        command,
    }
    ItemMenu_Mode ItemMenuMode;
    [SerializeField] GameMode GameMode;
    [SerializeField] Menu Menu;
    belongings belongings;
    Player_Move Player_Move;
    Player_Status Player_Status;
    [SerializeField] Animator Select_Animator,Command_Animator;

    int Number_of_Item=0;                   //所持アイテム数
    [SerializeField] GameObject Item_UI;
    [SerializeField] RectTransform Select_Cursor,Command_Cursor;  //カーソルの位置
    [SerializeField] GameObject Item_Select;           //アイテムメニュー表示のON・OFF用
    [SerializeField] List<Text> Item_Text;  //アイテム名のテキスト
    [SerializeField] Text Item_Detail;      //アイテムの説明をするテキスト
    int Select_CursorPos = 0, Command_CursorPos = 0;    //カーソルの示す場所
    [SerializeField] GameObject Command;
    [SerializeField] GameObject ItemObject;

    [SerializeField] Status_Display Status_Display;
    // Start is called before the first frame update
    void Start()
    {
        belongings = GameObject.Find("player").GetComponent<belongings>();
        Player_Move = GameObject.Find("player").GetComponent<Player_Move>();
        Player_Status = GameObject.Find("player").GetComponent<Player_Status>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMode.Gamemode != GameMode.Game_Mode.menu)
            return;
        if (Menu.MenuMode != Menu.Menu_Mode.item)
            return;
        if (ItemMenuMode == ItemMenu_Mode.select)
        {
            Item_Select.SetActive(true);
            //アイテムを選択する
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (belongings.Item(Select_CursorPos) != null)
                    Open_Command();
            }

            //アイテム欄を閉じる
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Close_ItemMenu();
                Menu.Mode_Change(Menu.Menu_Mode.list);
            }

            //カーソル移動
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Select_CursorPos > 0)
                {
                    Select_CursorPos--;
                    Select_Cursor.localPosition = new Vector2(Select_Cursor.localPosition.x, 200 - 50 * Select_CursorPos);
                    Detail_Display(Select_CursorPos);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Select_CursorPos < Number_of_Item - 1)
                {
                    Select_CursorPos++;
                    Select_Cursor.localPosition = new Vector2(Select_Cursor.localPosition.x, 200 - 50 * Select_CursorPos);
                    Detail_Display(Select_CursorPos);
                }
            }
            
        }
        else if (ItemMenuMode == ItemMenu_Mode.command)
        {
            Command.SetActive(true);
            //選択されてるコマンドを実行する
            if (Input.GetKeyDown(KeyCode.Z))
            {
                switch (Command_CursorPos)
                {
                    case 0: //アイテムを使う
                        Use_Item();
                        break;
                    case 1: //アイテムを投げる
                        Throw_Item();
                        break;
                    case 2: //アイテムを置く
                        Put_Item();
                        break;
                    case 3: //アイテムの詳細を見る
                        break;
                    default:
                        break;
                }
            }

            //戻る
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Close_Command();
            }

            //カーソル移動
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Command_CursorPos > 0)
                {
                    Command_CursorPos--;
                    Command_Cursor.localPosition = new Vector2(Command_Cursor.localPosition.x, 75 - 50 * Command_CursorPos);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Command_CursorPos < 3)
                {
                    Command_CursorPos++;
                    Command_Cursor.localPosition = new Vector2(Command_Cursor.localPosition.x, 75 - 50 * Command_CursorPos);
                }
            }
            
        }

    }

    //アイテム欄を開くときの処理
    public void Open_ItemMenu()
    {
        ItemMenuMode = ItemMenu_Mode.select;
        Select_CursorPos = 0;
        Select_Cursor.localPosition = new Vector2(Select_Cursor.localPosition.x, 200 - 50 * Select_CursorPos);
        Item_Select.SetActive(true);
        Item_UI.SetActive(true);
        Number_of_Item = belongings.Number_of_Item();
        for (int i = 0; i < 10; i++)
        {
            Item_Display(i);
        }
        if (belongings.Item(Select_CursorPos) != null)
            Detail_Display(Select_CursorPos);
        else
            Item_Detail.text = "";
        Select_Animator.SetTrigger("Open");

        Status_Display.Open_StatusDisplay();
    }

    //アイテム欄を閉じるときの処理
    void Close_ItemMenu(bool return_menu = false)
    {
        if (return_menu)    //全体メニューに戻るか
        {
            Item_Select.SetActive(false);
            Status_Display.Close_StatusDisplay();
            Item_UI.SetActive(false);
        }
        else
        {
            Select_Animator.SetTrigger("Close");
            Status_Display.Close_StatusDisplay();
            Item_UI.SetActive(false);
        }
    }

    //アイテムコマンドを開くときの処理
    void Open_Command()
    {
        ItemMenuMode = ItemMenu_Mode.command;
        Command.SetActive(true);
        if (belongings.Item(Select_CursorPos).item_type == Item_Type.weapon)
        {
            if (Player_Status.Equip_Weapon != belongings.Item(Select_CursorPos))
            {
                Command.transform.Find("Equip").gameObject.SetActive(true);
                Command.transform.Find("Use").gameObject.SetActive(false);
                Command.transform.Find("UnEquip").gameObject.SetActive(false);
            }
            else
            {
                Command.transform.Find("UnEquip").gameObject.SetActive(true);
                Command.transform.Find("Use").gameObject.SetActive(false);
                Command.transform.Find("Equip").gameObject.SetActive(false);
            }
        }
        else
        {
            Command.transform.Find("Use").gameObject.SetActive(true);
            Command.transform.Find("Equip").gameObject.SetActive(false);
            Command.transform.Find("UnEquip").gameObject.SetActive(false);
        }
        Command_CursorPos = 0;
        Command_Cursor.localPosition = new Vector2(Command_Cursor.localPosition.x, 75 - 50 * Command_CursorPos);
        Command_Animator.SetTrigger("Open");
    }

    //アイテムコマンドを閉じるときの処理
    void Close_Command(bool instant = false)
    {
        ItemMenuMode = ItemMenu_Mode.select;
        if (instant)
        {
            Command.SetActive(false);
        }
        else
            Command_Animator.SetTrigger("Close");
    }

    //N番目のアイテムを表示させる
    void Item_Display(int Text_Number)
    {
        string text="";
        if (belongings.Item(Text_Number) != null)
        {
            if (belongings.Item(Text_Number).item_type == Item_Type.weapon)
            {
                if (Player_Status.Equip_Weapon == belongings.Item(Text_Number))
                {
                    text = "E:";
                }
            }
            text += belongings.Item(Text_Number).Name;
        }
        else
            text = "";
        Item_Text[Text_Number].text = text;
    }

    //アイテムの説明を表示する
    void Detail_Display(int CursorPos)
    {
        switch (belongings.Item(CursorPos).item_type)
        {
            case Item_Type.weapon:
                Weapon weapon = (Weapon)belongings.Item(CursorPos);
                Item_Detail.text = "攻" + (Player_Status.ATK + Player_Status.ATK_Plus) + "→" + (Player_Status.ATK + weapon.ATK) + "\n";
                Item_Detail.text += belongings.Item(CursorPos).Item_Detail;
                break;
            case Item_Type.bottle:
                Item_Detail.text = belongings.Item(CursorPos).Item_Detail;
                break;
            case Item_Type.scroll:
                break;
            case Item_Type.staff:
                break;
            case Item_Type.other:
                break;
        }
    }

    //アイテムを使う
    void Use_Item()
    {
        switch (belongings.Item(Select_CursorPos).item_type)
        {
            case Item_Type.weapon:
                if (belongings.Item(Select_CursorPos).Use())
                {
                    belongings.EquipWeapon(Select_CursorPos);
                }
                break;
            case Item_Type.bottle:
                if (belongings.Item(Select_CursorPos).Use())
                {
                    belongings.Delete(Select_CursorPos);
                }
                break;
            case Item_Type.scroll:
                break;
            case Item_Type.staff:
                break;
            case Item_Type.other:
                break;
        }
        //アイテム使用後の処理
        Close_Command(true);
        Close_ItemMenu(true);
        Menu.Close_Menu();
        Player_Move.ActionEnd();
        GameMode.Mode_Change(GameMode.Game_Mode.enemy_controller);
    }

    //アイテムを投げる
    void Throw_Item()
    {
        if (belongings.Item(Select_CursorPos) == Player_Status.Equip_Weapon)
            belongings.Item(Select_CursorPos).Use();    //装備中だったら解除
        GameObject gameObject = Instantiate(ItemObject, Player_Move.transform.position, Quaternion.identity);
        gameObject.GetComponent<Item_Object>().Initialization(belongings.Item(Select_CursorPos).id);
        gameObject.GetComponent<Item_Object>().Throw(Player_Move.Direction);
        belongings.Delete(Select_CursorPos);
        Close_Command(true);
        Close_ItemMenu(true);
        Menu.Close_Menu();
        Player_Move.ActionEnd();
    }

    //アイテムをその場に置く
    void Put_Item()
    {
        if (belongings.Item(Select_CursorPos) == Player_Status.Equip_Weapon)
            belongings.Item(Select_CursorPos).Use();    //装備中だったら解除
        GameObject gameObject = Instantiate(ItemObject, Player_Move.transform.position, Quaternion.identity);
        gameObject.GetComponent<Item_Object>().Initialization(belongings.Item(Select_CursorPos).id);
        belongings.Delete(Select_CursorPos);
        Close_Command(true);
        Close_ItemMenu(true);
        Menu.Close_Menu();
        Player_Move.ActionEnd();
        GameMode.Mode_Change(GameMode.Game_Mode.enemy_controller);
    }
}
