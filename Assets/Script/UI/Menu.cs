using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
���j���[��ʂ̊Ǘ�
 */
public class Menu : MonoBehaviour
{
    //���j���[�̂Ȃ�̑�������Ă��邩��\��
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

    [SerializeField] RectTransform Cursor;  //�J�[�\���̈ʒu
    int CursorPos = 0;  //�J�[�\�����w���R�}���h
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
        //���[�h�`�F���W
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

        //����{�^�����������Ƃ��̏���
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (CursorPos)
            {
                case 0://�X�L����ʂɑJ��
                    //Mode_Change(Menu_Mode.skill);
                    break;
                case 1://�C���x���g����ʂɑJ��
                    Item_Menu.Open_ItemMenu();
                    Mode_Change(Menu_Mode.item);
                    break;
                case 2://�X�e�[�^�X��ʂɑJ��
                    Status_Menu.Open_StatusMenu();
                    Mode_Change(Menu_Mode.status);
                    break;
                case 3://�ݒ��ʂɑJ��
                    //Mode_Change(Menu_Mode.config);
                    break;
                case 4://���j���[��ʂ����
                    Close_Menu();
                    GameMode.Mode_Change(GameMode.Game_Mode.operabale);
                    break;
            }
        }
        //�L�����Z���{�^�����������Ƃ��̏���
        else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
        {
            Close_Menu();
            GameMode.Mode_Change(GameMode.Game_Mode.operabale);
        }
        //�J�[�\���ړ�
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
    //���j���[���[�h��ς���
    public void Mode_Change(Menu_Mode menuMode)
    {
        temp = menuMode;
        isChange = true;
    }

    //���j���[��ʂ��J���Ƃ��̏���
    public void Open_Menu()
    {
        Menu_UI.SetActive(true);
        menumode = Menu_Mode.list;
        CursorPos = 0;
        Cursor.localPosition = new Vector2(Cursor.localPosition.x, 230 - 100 * CursorPos);
        Backs[CursorPos].enabled = true;
        Menu_Animator.SetTrigger("Open");
    }

    //���j���[��ʂ����Ƃ��̏���
    public void Close_Menu()
    {
        //Menu_UI.SetActive(false);
        Backs[CursorPos].enabled = false;
        Menu_Animator.SetTrigger("Close");
    }
}
