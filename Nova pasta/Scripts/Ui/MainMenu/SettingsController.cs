using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomUI;
using TMPro;
using System.IO;
using System;

class App_Settings{
    public int _TextureQuality;
    public int _MSAAQuality;
    public int _ShadowsQuality;
    public int _VSyncLevel;
    public bool _FullScreen;
    public Vector3 _Resolution;
    public int ResolutionIndex;
}

public class SettingsController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Slider _TextureSlider;
    [SerializeField] TMP_Text _TextureValue;
    [SerializeField] Slider _MSAASlider;
    [SerializeField] TMP_Text _MSAAValue;
    [SerializeField] Slider _ShadowsSlider;
    [SerializeField] TMP_Text _ShadowsValue;
    [SerializeField] Slider _VsyncSlider;
    [SerializeField] TMP_Text _VsyncValue;
    [SerializeField] TMP_Dropdown _ResolutionDrop;
    [SerializeField] Toggle _IsFullScreen;
    [Header("Buttons")]
    [SerializeField] C_Button SaveBtn;

    //
    int _TextureQuality;
    int _MSAAQuality;
    int _ShadowsQuality;
    int _VSyncLevel;
    bool _FullScreen;
    Vector3 _Resolution;
    int _ResolutionIndex;

    static string _datapach;

    Resolution[] resolutions;

    void Awake()
    {
        _datapach = $"c:/Users/{Environment.UserName}/Documents/Shooter/Config/";

        CreateData();

        LoadResolutionUI();
        
        //Data
        LoadSettingsData();

        //

        SetQualitySettings();

        SaveBtn._ClickEvent.AddListener(delegate{
            LoadSettingsofUi();

            SaveSettingsData();

            SetQualitySettings();
        });
    }

    void Start()
    {
        
    }

    void LoadResolutionUI()
    {
        resolutions = Screen.resolutions;

        _ResolutionDrop.ClearOptions();

        List<string> ResolutionsNames = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            ResolutionsNames.Add($"{resolutions[i].width} x {resolutions[i].height} @{resolutions[i].refreshRate}");
        }

        _ResolutionDrop.AddOptions(ResolutionsNames);
    }

    void LateUpdate()
    {
        switch (_TextureSlider.value)
        {
            case 3: _TextureValue.text = "VeryHigh"; break; 
            case 2: _TextureValue.text = "High"; break;
            case 1: _TextureValue.text = "Medium"; break;
            case 0: _TextureValue.text = "Low"; break;
        }

        switch(_MSAASlider.value)
        {
            case 0: _MSAAValue.text = "Disable"; break;
            case 1: _MSAAValue.text = "2X"; break;
            case 2: _MSAAValue.text = "4X"; break;
            case 3: _MSAAValue.text = "8X"; break;
        }

        switch(_ShadowsSlider.value)
        {
            case 0: _ShadowsValue.text = "Low"; break;
            case 1: _ShadowsValue.text = "Medium"; break;
            case 2: _ShadowsValue.text = "High"; break;
            case 3: _ShadowsValue.text = "VeryHigh"; break;
        }

        switch(_VsyncSlider.value)
        {
            case 0: _VsyncValue.text = "Disable"; break;
            case 1: _VsyncValue.text = "Enable"; break;
        }
    }

    void SetQualitySettings()
    {
        switch(_ShadowsQuality)
        {
            case 0: QualitySettings.shadowResolution = ShadowResolution.Low; break;
            case 1: QualitySettings.shadowResolution = ShadowResolution.Medium; break;
            case 2: QualitySettings.shadowResolution = ShadowResolution.High; break;
            case 3: QualitySettings.shadowResolution = ShadowResolution.VeryHigh; break;
        }

        switch (_TextureQuality)
        {
            case 3: QualitySettings.masterTextureLimit = 0; break; 
            case 2: QualitySettings.masterTextureLimit = 1; break;
            case 1: QualitySettings.masterTextureLimit = 2; break;
            case 0: QualitySettings.masterTextureLimit = 3; break;
        }

        switch(_MSAAQuality)
        {
            case 0: QualitySettings.antiAliasing = 0; break;
            case 1: QualitySettings.antiAliasing = 2; break;
            case 2: QualitySettings.antiAliasing = 4; break;
            case 3: QualitySettings.antiAliasing = 8; break;
        }

        QualitySettings.vSyncCount = _VSyncLevel;

        //
        int _IndexResolution = _ResolutionDrop.value;

        
        Screen.SetResolution((int)_Resolution.x, (int)_Resolution.y, _FullScreen, (int)_Resolution.z);
        
    }

    void LoadSettingsData()
    {
        var _settings = Get();

        _TextureQuality = _settings._TextureQuality;
        _MSAAQuality = _settings._MSAAQuality;
        _FullScreen = _settings._FullScreen;
        _ShadowsQuality = _settings._ShadowsQuality;
        _VSyncLevel = _settings._VSyncLevel;
        _ResolutionIndex = _settings.ResolutionIndex;

        _Resolution = _settings._Resolution;

        LoadSettingsForUi(_settings);
    }

    void SaveSettingsData()
    {
        var _settings = new App_Settings();
        _settings._FullScreen = _FullScreen;
        _settings._MSAAQuality = _MSAAQuality;
        _settings._ShadowsQuality = _ShadowsQuality;
        _settings._VSyncLevel = _VSyncLevel;
        _settings._TextureQuality = _TextureQuality;
        _settings.ResolutionIndex = _ResolutionDrop.value;

        _settings._Resolution = _Resolution;

        Save(_settings);
    }

    //Data
    void CreateData()
    {
        if (!Directory.Exists(_datapach))
        {
            Directory.CreateDirectory(_datapach);
        }

        if (!File.Exists(_datapach + "/config.json"))
        {
            var data = new App_Settings();
            string json = JsonUtility.ToJson(data);

            File.WriteAllText(_datapach + "/config.json", json);

            var initialSettings = new App_Settings();
            initialSettings._Resolution = new Vector3(1280, 720, 60);
            initialSettings._FullScreen = true;
            Save(initialSettings);
        } 
    }

    App_Settings Get()
    {
        string json = File.ReadAllText(_datapach + "/config.json");
        var data = JsonUtility.FromJson<App_Settings>(json);
        return data;
    }

    void Save(App_Settings _Data)
    {
        string json = JsonUtility.ToJson(_Data);

        File.WriteAllText(_datapach + "/config.json", json);
    }

    //

    void LoadSettingsofUi()
    {
        _TextureQuality = (int)_TextureSlider.value;
        _MSAAQuality = (int)_MSAASlider.value;
        _ShadowsQuality = (int)_ShadowsSlider.value;
        _VSyncLevel = (int)_VsyncSlider.value;
        _FullScreen = _IsFullScreen.isOn;

        _Resolution.x = resolutions[_ResolutionDrop.value].width;
        _Resolution.y = resolutions[_ResolutionDrop.value].height;
        _Resolution.z = resolutions[_ResolutionDrop.value].refreshRate;
    }

    void LoadSettingsForUi(App_Settings _Settings)
    {
        _TextureSlider.value = _Settings._TextureQuality;
        _MSAASlider.value = _Settings._MSAAQuality;
        _ShadowsSlider.value = _Settings._ShadowsQuality;
        _VsyncSlider.value = _Settings._VSyncLevel;
        _IsFullScreen.isOn = _Settings._FullScreen;

        _ResolutionDrop.value = _ResolutionIndex;
    }
}
