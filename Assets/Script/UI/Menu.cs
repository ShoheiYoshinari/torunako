using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
メニュー画面の管理
 */
public class Menu : MonoBehaviour
{
    //メニューのなんの操作をしているかを表す
    public enum Menu_Mode
    {
        list,
        item,
        skill,
        status,
        config,
    }
    [SerializeField] Menu_Mode menumode = Menu_Mode.list;
    Menu_Mode temp;
    bool isChange = false;

    [SerializeField] RectTransform Cursor;  //カーソルの位置
    int CursorPos = 0;  //カーソルが指すコマンド
    [SerializeField] GameObject Menu_UI;

    [SerializeField] GameMode GameMode;
    [SerializeField] Item_Menu Item_Menu;
    [SerializeField] Status_Menu Status_Menu;
    [SerializeField] Animator Menu_Animator;
    [SerializeField] List<Image> Backs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //モードチェンジ
        if (isChange)
        {
            isChange = false;
            menumode = temp;
            return;
        }

        if (GameMode.Gamemode != GameMode.Game_Mode.menu)
            return;
        if (menumode != Menu_Mode.list)
            return;

        //決定ボタンを押したときの処理
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (CursorPos)
            {
                case 0://スキル画面に遷移
                    //Mode_Change(Menu_Mode.skill);
                    break;
                case 1://インベントリ画面に遷移
                    Item_Menu.Open_ItemMenu();
                    Mode_Change(Menu_Mode.item);
                    break;
                case 2://ステータス画面に遷移
                    Status_Menu.Open_StatusMenu();
                    Mode_Change(Menu_Mode.status);
                    break;
                case 3://設定画面に遷移
                    //Mode_Change(Menu_Mode.config);
                    break;
                case 4://メニュー画面を閉じる
                    Close_Menu();
                    GameMode.Mode_Change(GameMode.Game_Mode.operabale);
                    break;
            }
        }
        //キャンセルボタンを押したときの処理
        else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
        {
            Close_Menu();
            GameMode.Mode_Change(GameMode.Game_Mode.operabale);
        }
        //カーソル移動
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Backs[CursorPos].enabled = false;
            if (CursorPos > 0)
            {
                CursorPos--;
            }
            else
            {
                CursorPos = 4;
            }
            Cursor.localPosition = new Vector2(Cursor.localPosition.x, 230 - 100 * CursorPos);
            Backs[CursorPos].enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Backs[CursorPos].enabled = false;
            if (CursorPos < 4)
            {
                CursorPos++;
            }
            else
            {
                CursorPos = 0;
            }
            Cursor.localPosition = new Vector2(Cursor.localPosition.x, 230 - 100 * CursorPos);
            Backs[CursorPos].enabled = true;
        }
    }
    public Menu_Mode MenuMode
    {
        get { return menumode; }
    }
    //メニューモードを変える
    public void Mode_Change(Menu_Mode menuMode)
    {
        temp = menuMode;
        isChange = true;
    }

    //メニュー画面を開くときの処理
    public void Open_Menu()
    {
        Menu_UI.SetActive(true);
        menumode = Menu_Mode.list;
        CursorPos = 0;
        Cursor.localPosition = new Vector2(Cursor.localPosition.x, 230 - 100 * CursorPos);
        Backs[CursorPos].enabled = true;
        Menu_Animator.SetTrigger("Open");
    }

    //メニュー画面を閉じるときの処理
    public void Close_Menu()
    {
        //Menu_UI.SetActive(false);
        Backs[CursorPos].enabled = false;
        Menu_Animator.SetTrigger("Close");
    }
}
