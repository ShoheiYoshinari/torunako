using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
�Q�[�������ǂ�������Ԃ���\���ϐ�Gamemode���Ǘ�����
 */
public class GameMode : MonoBehaviour
{
    public enum Game_Mode
    {
        operabale,          //�v���C���[���ړ��C�U���̑��삪�ł���
        only_player_moving, //�v���C���[�������ړ���
        moving,             //�v���C���[�������͓G�̈ړ���
        enemy,              //�G�̍s����
        menu,               //���j���[���J���Ă���
        attack,              //�v���C���[�������͓G�̍U����
        enemy_controller,   //�GAI�ɖ��ߒ�
        enemy_attack_controller,    //�G�ւ̍U�����ߒ�
        throwing,           //�A�C�e���𓊂��Ă�
        game_over,          //�Q�[���I�[�o�[�ɂȂ��Ă���
        game_clear,         //�Q�[���N���A���Ă���
        stairs, //�K�i�`�F�b�N
    }

    [SerializeField] Game_Mode gamemode = Game_Mode.operabale;
    Game_Mode temp;
    bool isChange=false;

    
    public Game_Mode Gamemode
    {
        get { return gamemode; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange)
        {
            isChange = false;
            gamemode = temp;
        }
        
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    //�Q�[�����[�h��ς���
    public void Mode_Change(Game_Mode gameMode)
    {
        if (temp == Game_Mode.game_clear || temp == Game_Mode.game_over)
            return;
        temp = gameMode;
        isChange = true;
    }

}
