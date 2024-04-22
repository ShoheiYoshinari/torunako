using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
階段を降りるかどうかのチェックをする
 */
public class Stairs : MonoBehaviour
{
    [SerializeField] GameMode GameMode;
    [SerializeField] RectTransform cursor;
    int cursor_Pos = 1; //カーソルの位置を表す
    [SerializeField] int number_of_item; //タイトル画面の項目数
    [SerializeField] GameObject UI;
    [SerializeField] SceneClass SceneClass;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UI.SetActive(false);
        if (GameMode.Gamemode != GameMode.Game_Mode.stairs)
            return;
        UI.SetActive(true);
        //カーソル移動
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (cursor_Pos < number_of_item)
            {
                cursor_Pos++;
                cursor.localPosition = new Vector2(cursor.localPosition.x, cursor.localPosition.y - 50);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (cursor_Pos > 1)
            {
                cursor_Pos--;
                cursor.localPosition = new Vector2(cursor.localPosition.x, cursor.localPosition.y + 50);
            }
        }
        //決定
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (cursor_Pos)
            {
                case 1://降りる
                    SceneClass.Load_NewFoor();
                    break;
                case 2://降りない
                    GameMode.Mode_Change(GameMode.Game_Mode.operabale);
                    break;
                default:
                    break;
            }
        }
    }
}
