using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
ゲームクリア画面操作
 */
public class GameClear : MonoBehaviour
{
    [SerializeField] RectTransform cursor;
    int cursor_Pos = 1; //カーソルの位置を表す
    [SerializeField] int number_of_item; //タイトル画面の項目数
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //カーソル移動
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (cursor_Pos < number_of_item)
            {
                cursor_Pos++;
                cursor.localPosition = new Vector2(cursor.localPosition.x, cursor.localPosition.y - 100);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (cursor_Pos > 1)
            {
                cursor_Pos--;
                cursor.localPosition = new Vector2(cursor.localPosition.x, cursor.localPosition.y + 100);
            }
        }
        //決定
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (cursor_Pos)
            {
                case 1://タイトルに戻る
                    SceneManager.LoadScene("Title");
                    break;
                case 2://ゲーム終了
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }

    }
}

