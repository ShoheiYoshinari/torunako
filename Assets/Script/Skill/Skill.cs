using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected string Skill_Name;
    protected int Skill_ID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Name { get { return Skill_Name; } }

    public int Id { get { return Skill_ID; } }

    public abstract bool isUse();

    public abstract void Use(Player_Status player_Status);
}
