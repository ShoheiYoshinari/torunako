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

    //�V�����K�w�֍s��
    public void Load_NewFoor()
    {
        Scene = SceneManager.GetActiveScene();  //���݂̊K�w��ǂݍ��ށi��ŃA�����[�h����j

        //�V�[���̃��[�h�E�A�����[�h���̊֐���ǉ�
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.sceneUnloaded += SceneUnloaded;

        SceneManager.LoadScene("Dungeon", LoadSceneMode.Additive);  //�V�����K�w��ǂݍ���
    }


    //�����̃V�[�������[�h���ꂽ�Ƃ��̏���
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= SceneLoaded;    //���̊֐����Ăяo����Ȃ��悤�ɂ���

        SceneManager.MoveGameObjectToScene(Player, nextScene);  //�v���C���[�͎��̊K�w�ֈ��p��

        //�V�[���̐؂�ւ�
        SceneManager.SetActiveScene(nextScene);
        SceneManager.UnloadSceneAsync(Scene);
    }

    //�����̃V�[�����A�����[�h���ꂽ�Ƃ��̏���
    void SceneUnloaded(Scene thisScene)
    {
        SceneManager.sceneUnloaded -= SceneUnloaded;    //���̊֐����Ăяo����Ȃ��悤�ɂ���
        Player.GetComponent<Player_Move>().Load();  //���̊K�w�ɂȂ������̃v���C���[�̏���
        GameObject.Find("GameController").GetComponent<Dungeon_Create>().Floor = Dungeon.Floor + 1; //�_���W���������̊K�w��
    }
}
