using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPbar : MonoBehaviour
{
    Player_Status Player;
    [SerializeField] Slider Slider; //MPバーのゲージ
    [SerializeField] Text Text;     //MPの数字表示
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("player").GetComponent<Player_Status>();
    }

    // Update is called once per frame
    void Update()
    {
        //現在のMPに更新
        Slider.value = (float)Player.NowMP / Player.MaxMP;
        Text.text = Player.NowMP + " / " + Player.MaxMP;
    }
}
