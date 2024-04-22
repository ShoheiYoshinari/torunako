using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
�Q�[���I�[�o�[��ʑ���
 */
public class GameOver : MonoBehaviour
{
    [SerializeField] RectTransform cursor;
    int cursor_Pos = 1; //�J�[�\���̈ʒu��\��
    [SerializeField] int number_of_item; //�^�C�g����ʂ̍��ڐ�
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�J�[�\���ړ�
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
        //����
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (cursor_Pos)
            {
                case 1://������x�v���C����
                    SceneManager.LoadScene("Main");
                    break;
                case 2://�^�C�g���ɖ߂�
                    SceneManager.LoadScene("Title");
                    break;
                case 3://�Q�[���I��
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }

    }
}