using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
�}�b�v�̊e���C�܂��͕����̏��
 */
public class Room
{
    public int right,left, top, bottom, width, height;  //�����̉E�[�C���[�C��[�C���[�C�����C�c��

    //�����̏�����
    public Room(int R,int L,int T,int B)
    {
        right = R;
        left = L;
        top = T;
        bottom = B;
        width = right - left;
        height = top - bottom;
    }

    //�����̃T�C�Y����蒼��
    public void Set(int R, int L, int T, int B)
    {
        right = R;
        left = L;
        top = T;
        bottom = B;
        width = right - left;
        height = top - bottom;
    }

}
