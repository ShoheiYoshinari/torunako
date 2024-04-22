using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
ゴブリンシーフ
 */
public class Thief : Job
{
    // Start is called before the first frame update
    void Awake()
    {
        Load("CharacterTip/Thief", "JOB/Thief");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
