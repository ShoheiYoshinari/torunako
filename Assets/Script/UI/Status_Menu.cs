using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status_Menu : MonoBehaviour
{
    enum StatusMenu_Mode
    {
        select,
        command,
    }
    StatusMenu_Mode StatusMenuMode;
    [SerializeField] GameMode GameMode;
    [SerializeField] Menu Menu;

    [SerializeField] GameObject Status_UI;

    [SerializeField] Status_Display Status_Display;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameMode.Gamemode != GameMode.Game_Mode.menu)
            return;
        if (Menu.MenuMode != Menu.Menu_Mode.status)
            return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            Close_StatusMenu();
        }
    }

    //ステータス画面を開くときの処理
    public void Open_StatusMenu()
    {
        Status_UI.SetActive(true);
        Status_Display.Open_StatusDisplay();
    }

    //ステータス画面を閉じるときの処理
    void Close_StatusMenu()
    {
        Status_UI.SetActive(false);
        Status_Display.Close_StatusDisplay();
        Menu.Mode_Change(Menu.Menu_Mode.list);
    }
}
