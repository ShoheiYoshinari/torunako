using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
HP���Q�[�W�Ɛ����ŕ\��
 */
public class HPbar : MonoBehaviour
{
    Player_Status Player;
    [SerializeField] Slider Slider; //HP�o�[�̃Q�[�W
    [SerializeField] Text Text;     //HP�̐����\��
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("player").GetComponent<Player_Status>();
    }

    // Update is called once per frame
    void Update()
    {
        //���݂�HP�ɍX�V
        Slider.value = (float)Player.NowHP / Player.MaxHP;
        Text.text = Player.NowHP + " / " + Player.MaxHP;
    }
}
