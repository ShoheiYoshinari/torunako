using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
カメラの動き
 */
public class Camera : MonoBehaviour
{
    [SerializeField] GameMode GameMode;
    [SerializeField] Transform CameraPos;  //カメラの座標
    Transform Player;  //プレイヤーの座標
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("player").transform;
        CameraPos.position = new Vector3(Player.position.x, Player.position.y, CameraPos.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの移動中に追尾する
        if (GameMode.Gamemode == GameMode.Game_Mode.moving || GameMode.Gamemode == GameMode.Game_Mode.only_player_moving)
        {
            CameraPos.position = new Vector3(Player.position.x, Player.position.y, CameraPos.position.z);
        }
    }
}
