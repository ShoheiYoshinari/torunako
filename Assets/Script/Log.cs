using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
�Q�[�����̃��O���Ǘ�����
 */
public class Log : MonoBehaviour
{
    List<string> All_Logs=new List<string>();  //�S�Ẵ��O�̃f�[�^
    [SerializeField] GameObject Log_Object; //���O�\���p�̂̃I�u�W�F�N�g
    [SerializeField] List<Text> Log_Texts;  //�Q�[����ʂɕ\�����郍�O��text

    float time;   //���Ԃ𑪂�p
    [SerializeField] float Display_Time;  //���O���\������Ă��������܂ł̎���
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

    //�V�������O���o��
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

    //���O��\������
    void Open_Log()
    {
        Log_Object.SetActive(true);
        time = 0;
    }

    //���O�̕\��������
    void Close_Log()
    {
        Log_Object.SetActive(false);
    }
}
