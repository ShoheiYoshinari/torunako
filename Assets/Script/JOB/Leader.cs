using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
ゴブリンリーダー
 */
public class Leader : Job
{
    // Start is called before the first frame update
    void Awake()
    {
        Load("CharacterTip/Leader", "JOB/Leader");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
