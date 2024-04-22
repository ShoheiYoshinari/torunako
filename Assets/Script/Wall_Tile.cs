using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Tile : MonoBehaviour
{
    [SerializeField] SpriteRenderer Left_Up, Right_Up, Left_Down, Right_Down;   //����A�E��A�����A�E���̕ǂ̃I�u�W�F�N�g���Q��
    [SerializeField] List<Sprite> Tiles;    //�ǂ̉摜������
    [SerializeField] SpriteRenderer Mini_Left_Up, Mini_Right_Up, Mini_Left_Down, Mini_Right_Down;   //�~�j�}�b�v�̃I�u�W�F�N�g���Q��
    [SerializeField] List<Sprite> Mini_Tiles;   //�~�j�}�b�v�p�̕ǂ̉摜������

    //�ǂ̌����ڂ�ݒ肷��
    public void Create_Wall(int[,] Grid_3x3)
    {
        //����
        if (Grid_3x3[0, 1] > 0 && Grid_3x3[1, 2] > 0)
        {
            Left_Up.sprite = Tiles[1];
            Mini_Left_Up.sprite = Mini_Tiles[1];
        }
        else if (Grid_3x3[0, 1] > 0)
        {
            Left_Up.sprite = Tiles[5];
            Mini_Left_Up.sprite = Mini_Tiles[5];
        }
        else if (Grid_3x3[1, 2] > 0)
        {
            Left_Up.sprite = Tiles[9];
            Mini_Left_Up.sprite = Mini_Tiles[7];
        }
        else if (Grid_3x3[0, 2] > 0)
        {
            Left_Up.sprite = Tiles[13];
            Mini_Left_Up.sprite = Mini_Tiles[9];
        }
        else
        {
            Left_Up.sprite = Tiles[0];
            Mini_Left_Up.sprite = Mini_Tiles[0];
        }

        //�E��
        if (Grid_3x3[2, 1] > 0 && Grid_3x3[1, 2] > 0)
        {
            Right_Up.sprite = Tiles[2];
            Mini_Right_Up.sprite = Mini_Tiles[2];
        }
        else if (Grid_3x3[2, 1] > 0)
        {
            Right_Up.sprite = Tiles[6];
            Mini_Right_Up.sprite = Mini_Tiles[6];
        }
        else if (Grid_3x3[1, 2] > 0)
        {
            Right_Up.sprite = Tiles[10];
            Mini_Right_Up.sprite = Mini_Tiles[7];
        }
        else if (Grid_3x3[2, 2] > 0)
        {
            Right_Up.sprite = Tiles[14];
            Mini_Right_Up.sprite = Mini_Tiles[10];
        }
        else
        {
            Right_Up.sprite = Tiles[0];
            Mini_Right_Up.sprite = Mini_Tiles[0];
        }

        //����
        if (Grid_3x3[0, 1] > 0 && Grid_3x3[1, 0] > 0)
        {
            Left_Down.sprite = Tiles[3];
            Mini_Left_Down.sprite = Mini_Tiles[3];
        }
        else if (Grid_3x3[0, 1] > 0)
        {
            Left_Down.sprite = Tiles[7];
            Mini_Left_Down.sprite = Mini_Tiles[5];
        }
        else if (Grid_3x3[1, 0] > 0)
        {
            Left_Down.sprite = Tiles[11];
            Mini_Left_Down.sprite = Mini_Tiles[8];
        }
        else if (Grid_3x3[0, 0] > 0)
        {
            Left_Down.sprite = Tiles[15];
            Mini_Left_Down.sprite = Mini_Tiles[11];
        }
        else
        {
            Left_Down.sprite = Tiles[0];
            Mini_Left_Down.sprite = Mini_Tiles[0];
        }

        //�E��
        if (Grid_3x3[2, 1] > 0 && Grid_3x3[1, 0] > 0)
        {
            Right_Down.sprite = Tiles[4];
            Mini_Right_Down.sprite = Mini_Tiles[4];
        }
        else if (Grid_3x3[2, 1] > 0)
        {
            Right_Down.sprite = Tiles[8];
            Mini_Right_Down.sprite = Mini_Tiles[6];
        }
        else if (Grid_3x3[1, 0] > 0)
        {
            Right_Down.sprite = Tiles[12];
            Mini_Right_Down.sprite = Mini_Tiles[8];
        }
        else if (Grid_3x3[2, 0] > 0)
        {
            Right_Down.sprite = Tiles[16];
            Mini_Right_Down.sprite = Mini_Tiles[12];
        }
        else
        {
            Right_Down.sprite = Tiles[0];
            Mini_Right_Down.sprite = Mini_Tiles[0];
        }
    }
}
