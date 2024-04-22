using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
ƒSƒuƒŠƒ“ƒ^ƒ“ƒN
 */
public class Tank : Job
{
    // Start is called before the first frame update
    void Awake()
    {
        Load("CharacterTip/Tank", "JOB/Tank");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
