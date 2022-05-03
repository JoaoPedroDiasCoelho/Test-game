using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Config Asset")]
public class PlayerConfig : ScriptableObject
{
    public float JumpForce = 1.3f;
    public float WalkSpeed = 3f;
    public float RunSpeed = 5f;
    public float MouseSense = 1.3f;
    public float GravityForce = 9.8f;
}
