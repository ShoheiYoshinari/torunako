using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Profiling;

public class Move_Controller : MonoBehaviour
{
    public List<Character_Move> Move_MOBs = new List<Character_Move>();
    [SerializeField] GameMode GameMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMode.Gamemode != GameMode.Game_Mode.moving)
            return;
        //�ړ����I�������G�̍U�����߂Ɉڂ�
        if (Move_MOBs.Count == 0)
            GameMode.Mode_Change(GameMode.Game_Mode.enemy_attack_controller);

    }
}
