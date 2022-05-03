using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypingPauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PausedMenu;
    [SerializeField] PlayerSetup _Setup;
    void Awake()
    {
        PausedMenu.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PausedMenu.SetActive(!PausedMenu.activeInHierarchy);
            _Setup.OnEnablePlayer(!PausedMenu.activeInHierarchy);
            switch(PausedMenu.activeInHierarchy)
            {
                case true: LockMouse.Lock(CursorLockMode.None, true); break;
                case false: LockMouse.Lock(CursorLockMode.Locked, false); break;
            }
        }
    }
}
