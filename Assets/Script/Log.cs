using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
ゲーム内のログを管理する
 */
public class Log : MonoBehaviour
{
    List<string> All_Logs=new List<string>();  //全てのログのデータ
    [SerializeField] GameObject Log_Object; //ログ表示用ののオブジェクト
    [SerializeField] List<Text> Log_Texts;  //ゲーム画面に表示するログのtext

    float time;   //時間を測る用
    [SerializeField] float Display_Time;  //ログが表示されてから消えるまでの時間
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < Log_Texts.Count; i++)
        {
            Log_Texts[i].text = "";
        }
        Close_Log();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > Display_Time)
        {
            Close_Log();
        }
    }

    //新しいログを出す
    public void Add_Log(string log)
    {
        All_Logs.Add(log);
        Open_Log();
        for (int i = 0; i < Log_Texts.Count - 1; i++)
        {
            Log_Texts[i].text = Log_Texts[i + 1].text;
        }
        Log_Texts[Log_Texts.Count - 1].text = log;
    }

    //ログを表示する
    void Open_Log()
    {
        Log_Object.SetActive(true);
        time = 0;
    }

    //ログの表示を消す
    void Close_Log()
    {
        Log_Object.SetActive(false);
    }
}
