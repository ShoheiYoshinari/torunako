using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Mainシーンからのシーン遷移
 */
public class MainScene : MonoBehaviour
{
    [SerializeField] GameMode GameMode;
    void Update()
    {
        if (GameMode.Gamemode == GameMode.Game_Mode.game_over)
        {
            Invoke(nameof(Game_Over), 1.0f);
        }
        else if (GameMode.Gamemode == GameMode.Game_Mode.game_clear)
        {
            Invoke(nameof(Game_Clear), 1.0f);
        }
    }
    public void Game_Clear()
    {
        SceneManager.LoadScene("GameClear");
    }

    public void Game_Over()
    {
        SceneManager.LoadScene("GameOver");
    }
    
}
