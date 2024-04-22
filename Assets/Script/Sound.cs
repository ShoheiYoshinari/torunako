using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioSource Intro;
    [SerializeField] AudioSource Loop;
    [SerializeField] float LoopTime;
    // Start is called before the first frame update
    void Start()
    {
        Intro.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void FixedUpdate()
    {
        if (Intro.time >= LoopTime)
        {
            if (!Loop.isPlaying)
                Loop.Play();
        }
    }
}
