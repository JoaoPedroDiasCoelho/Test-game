using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUI;
using System;

[Serializable] class BackToMain
{
    public AudioSource Source;
    public AudioClip Clipback;
}
public class MainManuManager : MonoBehaviour
{
    [SerializeField] C_Button PlayButton;
    [SerializeField] C_Button SettingsButton;
    [SerializeField] C_Button QuitButton;
    [SerializeField] List<GameObject> Panels;
    [Header("Other")]
    [SerializeField] GameObject MainMenu;
    [SerializeField] BackToMain BackToMain;
    [SerializeField] TMPro.TMP_Text textversion;
    void Start()
    {
        PlayButton._ClickEvent.AddListener(delegate{
            ActivePanel("Play Panel");
            DesableMain();
        });

        SettingsButton._ClickEvent.AddListener(delegate{
            ActivePanel("Options Panel");
            DesableMain();
        });

        QuitButton._ClickEvent.AddListener(delegate{
            Application.Quit();
        });

        textversion.text = Application.version;
    }

    void Update()
    {
        
    }

    void DesableMain()
    {
        MainMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        MainMenu.SetActive(true);
        foreach(GameObject panel in Panels)
        {
            panel.SetActive(false);
        }

        BackToMain.Source.PlayOneShot(BackToMain.Clipback);
    }

    void ActivePanel(string name)
    {
        foreach(GameObject panel in Panels) 
        {
            panel.SetActive(panel.name == name);
        }
    }
}
