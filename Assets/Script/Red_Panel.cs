using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
�v���C���[�̕����]�����Ɍ����������������p�l����\������
 */
public class Red_Panel : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    float time = 0;
    [SerializeField] float Display_Time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > Display_Time)
        {
            Panel.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    //�\��������Ƃ��ɌĂяo��
    public void Display_Pnale(int x,int y)
    {
        Panel.transform.position = new Vector2(x, y);
        Panel.GetComponent<SpriteRenderer>().enabled = true;
        time = 0;
    }
}
