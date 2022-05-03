using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatesEvents : MonoBehaviour
{
    public bool PlayerAiming{get; private set;}
    public void SetAiming(bool value)
    {
        PlayerAiming = value;
    }
}
