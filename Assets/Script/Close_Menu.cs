using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_Menu : MonoBehaviour
{
    [SerializeField]GameObject GameObject;
    public void Close()
    {
        GameObject.SetActive(false);
    }
}
