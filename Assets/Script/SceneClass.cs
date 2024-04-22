using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneClass : MonoBehaviour
{
    GameObject Player;
    Scene Scene;
    Dungeon_Create Dungeon;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("player");
        Dungeon = GameObject.Find("GameController").GetComponent<Dungeon_Create>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //新しい階層へ行く
    public void Load_NewFoor()
    {
        Scene = SceneManager.GetActiveScene();  //現在の階層を読み込む（後でアンロードする）

        //シーンのロード・アンロード時の関数を追加
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.sceneUnloaded += SceneUnloaded;

        SceneManager.LoadScene("Dungeon", LoadSceneMode.Additive);  //新しい階層を読み込む
    }


    //何かのシーンがロードされたときの処理
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= SceneLoaded;    //この関数が呼び出されないようにする

        SceneManager.MoveGameObjectToScene(Player, nextScene);  //プレイヤーは次の階層へ引継ぎ

        //シーンの切り替え
        SceneManager.SetActiveScene(nextScene);
        SceneManager.UnloadSceneAsync(Scene);
    }

    //何かのシーンがアンロードされたときの処理
    void SceneUnloaded(Scene thisScene)
    {
        SceneManager.sceneUnloaded -= SceneUnloaded;    //この関数が呼び出されないようにする
        Player.GetComponent<Player_Move>().Load();  //次の階層になった時のプレイヤーの処理
        GameObject.Find("GameController").GetComponent<Dungeon_Create>().Floor = Dungeon.Floor + 1; //ダンジョンを次の階層へ
    }
}
