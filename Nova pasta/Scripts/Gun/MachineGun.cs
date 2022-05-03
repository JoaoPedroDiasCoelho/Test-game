using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable] class WeaponSounds
{
    public AudioSource _Source;
    public AudioClip _FireSound;
    public AudioClip _ReloadSound;
    public AudioClip _SwitchMode;
    public AudioClip _LoadSound;
    public AudioClip _AimSound;
}

[Serializable] class WeaponRecoil
{
    public float _Range = 1f;
    public float _Multiplier;
}

[Serializable] class WeaponEvents
{
    public UnityEvent _FireEvent;
    public UnityEvent _ReloadWithAmmEvent;
    public UnityEvent _ReloadWithNotAmmEvent;
    public UnityEvent _SwitchModeEvent;
}

[Serializable] class Delays
{
    public float _SwitchModeDelay = 0.4f;
    public float _EquipDelay = 0.7f;
    public float _DelayLoadSound = 0.3f;
    public float _DelayAimSound = 0.3f;
}

[Serializable] class Aiming
{
    public Vector3 _AimPos;
    public Vector3 _StartPos;
    public float _SmoothTime;
}

[Serializable] class WeaponVFX
{
    public GameObject MuzzeFlare;
}

public class MachineGun : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] WeaponMode _Mode = WeaponMode.FullAutoAndSingleFire;
    [SerializeField] Transform _WeaponPoint;
    [SerializeField] float _RayDistance = 500f;
    [SerializeField] int _Damage = 100;
    [SerializeField] float FireRate = 20f;
    [SerializeField] WeaponSounds _Sounds;
    [SerializeField] WeaponRecoil _Recoil;
    [SerializeField] WeaponEvents _Events;
    [SerializeField] Delays _Delays;
    [SerializeField] Aiming _Aim;
    [SerializeField] WeaponVFX _VFX;

    //Internal
    Transform _Direction;
    bool _FullAutoMode;
    float _TimeFire;
    bool _CanShoot;
    bool _Aiming;
    GameObject _CamScope;
    PlayerStatesEvents _PlayerStates;

    private void Awake()
    {
        SetDiretionForFire(Camera.main.transform);
        StartCoroutine(CanShootDelay(_Delays._EquipDelay));
        _PlayerStates = GetComponentInParent<PlayerStatesEvents>();

        _Aim._StartPos = transform.localPosition;
        var scope = GetComponentInChildren<Scope>();
        if(scope != null)
        {
            _CamScope = scope.GetComponentInChildren<Camera>().gameObject;
        }
    }

    private void Start()
    {
        StartCoroutine(PlayDelayClip(
            _Delays._DelayLoadSound,
            _Sounds._LoadSound
        ));
    }

    private void Update()
    {
        switch(_FullAutoMode)
        {
            case false:
                if (Input.GetButtonDown("Fire1") && _CanShoot)
                {
                    Shooting();
                }
            break;
            case true:
                if(Input.GetButton("Fire1") && _CanShoot && Time.time >= _TimeFire)
                {
                    //SetFireRate
                    _TimeFire = Time.time + 1f / FireRate;

                    Shooting();
                }
            break;
        }

        if(_Mode == WeaponMode.FullAutoAndSingleFire && Input.GetKeyDown(KeyCode.B) && _CanShoot)
        {
            _FullAutoMode = !_FullAutoMode;
            _Sounds._Source.PlayOneShot(_Sounds._SwitchMode);
            _Events._SwitchModeEvent.Invoke();
            StartCoroutine(CanShootDelay(_Delays._SwitchModeDelay));
        }

        if(Input.GetButtonDown("Fire2") && !Input.GetButton("Fire1"))
        {
            _Aiming = !_Aiming;
            StartCoroutine(PlayDelayClip(
                _Delays._DelayAimSound, 
                _Sounds._AimSound
            ));
        }

        _PlayerStates.SetAiming(_Aiming);
    }

    private void Reload()
    {
        
    }

    private void LateUpdate()
    {
        #region Aim
        var scope = GetComponentInChildren<Scope>();
        if(_CamScope != null)
        {
            _CamScope.SetActive(_Aiming);
        }

        switch(_Aiming)
        {
            case true:
                Vector3 _pos = Vector3.Lerp(transform.localPosition, _Aim._AimPos, Time.deltaTime * _Aim._SmoothTime);
                transform.localPosition = _pos;
                if(scope != null && _Direction != scope._ScopePoint)
                {
                    SetDiretionForFire(scope._ScopePoint);
                }
            break;
            case false:
                Vector3 _notpos = Vector3.Lerp(transform.localPosition, _Aim._StartPos, Time.deltaTime * _Aim._SmoothTime);
                transform.localPosition = _notpos;
                if(scope != null && _Direction == scope._ScopePoint)
                {
                    SetDiretionForFire(Camera.main.transform);
                }
            break;
        }
        
        #endregion
    }

    private void Shooting()
    {
        _Events._FireEvent.Invoke();
        _Sounds._Source.PlayOneShot(_Sounds._FireSound);

        #region VFX

        var _MuzzeFlare = Instantiate(_VFX.MuzzeFlare, _WeaponPoint.position, _WeaponPoint.rotation);
        _MuzzeFlare.transform.SetParent(transform);
        Destroy(_MuzzeFlare, 0.1f);
        #endregion

        #region recoil
            var _rec = GetComponentInParent<RecoilHead>();
            if(_rec != null)
            {
                _rec.SetRecoil(_Recoil._Multiplier, _Recoil._Range);
            }
        #endregion

        #region Raycasting
        RaycastHit hit;
        if (Physics.Raycast(_WeaponPoint.position, _Direction.forward, out hit, _RayDistance))
        {
            if (hit.transform.TryGetComponent(out IDamageable _Damageable))
            {
                _Damageable.TakeDamage(_Damage);
            }
        }
        #endregion
    }

    public void SetDiretionForFire(Transform _dir)
    {
        _Direction = _dir;
    }

    IEnumerator CanShootDelay(float time)
    {
        _CanShoot = false;
        yield return new WaitForSeconds(time);
        _CanShoot = true;
    }

    IEnumerator PlayDelayClip(float time, AudioClip clip)
    {
        yield return new WaitForSeconds(time);
        _Sounds._Source.PlayOneShot(clip);
    }
}
