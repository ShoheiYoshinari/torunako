using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
ゴブリンノービス
 */
public class Novice : Job
{

    // Start is called before the first frame update
    void Awake()
    {
        Load("CharacterTip/Novice", "JOB/Novice");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
