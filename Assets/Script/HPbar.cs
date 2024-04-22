using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
HPをゲージと数字で表示
 */
public class HPbar : MonoBehaviour
{
    Player_Status Player;
    [SerializeField] Slider Slider; //HPバーのゲージ
    [SerializeField] Text Text;     //HPの数字表示
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("player").GetComponent<Player_Status>();
    }

    // Update is called once per frame
    void Update()
    {
        //現在のHPに更新
        Slider.value = (float)Player.NowHP / Player.MaxHP;
        Text.text = Player.NowHP + " / " + Player.MaxHP;
    }
}
