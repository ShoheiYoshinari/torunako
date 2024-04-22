using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
‘S‚Ä‚Ì“G‚ÌƒŠƒXƒg‚ğ‚¿C“G‚ğ1‘Ì‚¸‚Âs“®‚³‚¹‚é
 */
public class EnemyController : MonoBehaviour
{
    public List<Enemy_Move> Enemies = new List<Enemy_Move>();  //“G‚ÌƒŠƒXƒg
    List<Enemy_Move> Attack_Enemies = new List<Enemy_Move>();
    Player_Move Player;
    [SerializeField] GameMode GameMode;
    int count;  //s“®‚µ‚½“G‚Ì”‚ğƒJƒEƒ“ƒg
    int enemy_num;  //¡‚¢‚é“G‚Ì”
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("player").GetComponent<Player_Move>();
        
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //“G‚ÉˆÚ“®‚Ì–½—ß‚ğ‚·‚éUŒ‚‚·‚é“G‚ÍŒã‰ñ‚µ
        if (GameMode.Gamemode == GameMode.Game_Mode.enemy_controller)
        {
            enemy_num = Enemies.Count;
            count = 0;
            Attack_Enemies.Clear();
            Sort_by_Distance();
            for(int i = 0; i < enemy_num; i++)
            {
                if (Enemies[i].Action())
                {
                    Attack_Enemies.Add(Enemies[i]);
                }
                else
                {
                    
                    Enemies[i].ActionStart();
                }
            }
            GameMode.Mode_Change(GameMode.Game_Mode.moving);
        }

        //“G‚ÉUŒ‚‚Ì–½—ß‚ğ‚·‚é
        if (GameMode.Gamemode == GameMode.Game_Mode.enemy_attack_controller)
        {
            if (count < Attack_Enemies.Count)
            {
                Attack_Enemies[count].ActionStart();
                GameMode.Mode_Change(GameMode.Game_Mode.attack);
                count++;
            }
            else
            {
                GameMode.Mode_Change(GameMode.Game_Mode.operabale);
                Player.ActionStart();
            }
        }
    }

    //“G‚ª€‚ñ‚¾‚Ìˆ—
    public void Enemy_Dead(Enemy_Move enemy)
    {
        Enemies.Remove(enemy);
        enemy_num--;
    }

    //“G‚ğƒvƒŒƒCƒ„[‚É‹ß‚¢‡‚É•À‚×‚é
    void Sort_by_Distance()
    {
        Enemy_Move temp;
        for (int i = 0; i < Enemies.Count; i++)
        {
            for (int j = i + 1; j < Enemies.Count; j++)
            {
                if (Enemies[i].Distance_to_Player() > Enemies[j].Distance_to_Player())
                {
                    temp = Enemies[i];
                    Enemies[i] = Enemies[j];
                    Enemies[j] = temp;
                }
            }
        }
    }

    public void SpriteRenderer_Reset()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
