using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] List<Behaviour> PlayerComponents;
    
    public void OnEnablePlayer(bool state)
    {
        foreach(Behaviour component in PlayerComponents)
        {
            component.enabled = state;
        }
    }
}
