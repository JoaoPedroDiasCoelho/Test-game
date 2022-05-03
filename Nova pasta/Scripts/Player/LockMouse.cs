using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMouse : MonoBehaviour
{
    void Start()
    {
        Lock(CursorLockMode.Locked, false);
    }

    public static void Lock(CursorLockMode LockMode, bool Visible)
    {
        Cursor.lockState = LockMode;
        Cursor.visible = Visible;
    }
}
