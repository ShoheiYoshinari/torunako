using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
ゴブリンファイター
 */
public class Fighter : Job
{
    // Start is called before the first frame update
    void Awake()
    {
        Load("CharacterTip/Fighter", "JOB/Fighter");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
