using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
�A�C�e�����j���[�̕\��
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

    int Number_of_Item=0;                   //�����A�C�e����
    [SerializeField] GameObject Item_UI;
    [SerializeField] RectTransform Select_Cursor,Command_Cursor;  //�J�[�\���̈ʒu
    [SerializeField] GameObject Item_Select;           //�A�C�e�����j���[�\����ON�EOFF�p
    [SerializeField] List<Text> Item_Text;  //�A�C�e�����̃e�L�X�g
    [SerializeField] Text Item_Detail;      //�A�C�e���̐���������e�L�X�g
    int Select_CursorPos = 0, Command_CursorPos = 0;    //�J�[�\���̎����ꏊ
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
            //�A�C�e����I������
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (belongings.Item(Select_CursorPos) != null)
                    Open_Command();
            }

            //�A�C�e���������
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Close_ItemMenu();
                Menu.Mode_Change(Menu.Menu_Mode.list);
            }

            //�J�[�\���ړ�
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
            //�I������Ă�R�}���h�����s����
            if (Input.GetKeyDown(KeyCode.Z))
            {
                switch (Command_CursorPos)
                {
                    case 0: //�A�C�e�����g��
                        Use_Item();
                        break;
                    case 1: //�A�C�e���𓊂���
                        Throw_Item();
                        break;
                    case 2: //�A�C�e����u��
                        Put_Item();
                        break;
                    case 3: //�A�C�e���̏ڍׂ�����
                        break;
                    default:
                        break;
                }
            }

            //�߂�
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Close_Command();
            }

            //�J�[�\���ړ�
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

    //�A�C�e�������J���Ƃ��̏���
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

    //�A�C�e���������Ƃ��̏���
    void Close_ItemMenu(bool return_menu = false)
    {
        if (return_menu)    //�S�̃��j���[�ɖ߂邩
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

    //�A�C�e���R�}���h���J���Ƃ��̏���
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

    //�A�C�e���R�}���h�����Ƃ��̏���
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

    //N�Ԗڂ̃A�C�e����\��������
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

    //�A�C�e���̐�����\������
    void Detail_Display(int CursorPos)
    {
        switch (belongings.Item(CursorPos).item_type)
        {
            case Item_Type.weapon:
                Weapon weapon = (Weapon)belongings.Item(CursorPos);
                Item_Detail.text = "�U" + (Player_Status.ATK + Player_Status.ATK_Plus) + "��" + (Player_Status.ATK + weapon.ATK) + "\n";
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

    //�A�C�e�����g��
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
        //�A�C�e���g�p��̏���
        Close_Command(true);
        Close_ItemMenu(true);
        Menu.Close_Menu();
        Player_Move.ActionEnd();
        GameMode.Mode_Change(GameMode.Game_Mode.enemy_controller);
    }

    //�A�C�e���𓊂���
    void Throw_Item()
    {
        if (belongings.Item(Select_CursorPos) == Player_Status.Equip_Weapon)
            belongings.Item(Select_CursorPos).Use();    //�����������������
        GameObject gameObject = Instantiate(ItemObject, Player_Move.transform.position, Quaternion.identity);
        gameObject.GetComponent<Item_Object>().Initialization(belongings.Item(Select_CursorPos).id);
        gameObject.GetComponent<Item_Object>().Throw(Player_Move.Direction);
        belongings.Delete(Select_CursorPos);
        Close_Command(true);
        Close_ItemMenu(true);
        Menu.Close_Menu();
        Player_Move.ActionEnd();
    }

    //�A�C�e�������̏�ɒu��
    void Put_Item()
    {
        if (belongings.Item(Select_CursorPos) == Player_Status.Equip_Weapon)
            belongings.Item(Select_CursorPos).Use();    //�����������������
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
