using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMain : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] AudioSource _Source;
    [SerializeField] AudioClip _StartClip;
    [SerializeField] float _DestroyTime;
    [SerializeField] GameObject _MainMenu;

    void Awake()
    {
        _MainMenu.SetActive(false);
    }
    
    public void ClickToStart()
    {
        _Source.PlayOneShot(_StartClip);
        Destroy(gameObject, _DestroyTime);
        _MainMenu.SetActive(true);
    }
}
