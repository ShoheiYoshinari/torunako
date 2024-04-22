using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
�J�����̓���
 */
public class Camera : MonoBehaviour
{
    [SerializeField] GameMode GameMode;
    [SerializeField] Transform CameraPos;  //�J�����̍��W
    Transform Player;  //�v���C���[�̍��W
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("player").transform;
        CameraPos.position = new Vector3(Player.position.x, Player.position.y, CameraPos.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�̈ړ����ɒǔ�����
        if (GameMode.Gamemode == GameMode.Game_Mode.moving || GameMode.Gamemode == GameMode.Game_Mode.only_player_moving)
        {
            CameraPos.position = new Vector3(Player.position.x, Player.position.y, CameraPos.position.z);
        }
    }
}
