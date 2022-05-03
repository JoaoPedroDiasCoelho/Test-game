using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUI;

public class Pass : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] C_Button _LeftButton;
    [SerializeField] C_Button _RightButton;
    [SerializeField] float _AddForce = 1f;
    [SerializeField] float _ForceMultiplier = 10f;
    [SerializeField] float _DelayTimeToResetForce = 1.2f;
    [SerializeField] Vector2 _ClampPos;
    float _InternalForce;
    void Start()
    {
        _LeftButton._ClickEvent.AddListener(_SetLeft);
        _RightButton._ClickEvent.AddListener(_SetRight);
    }

    void LateUpdate()
    {
        Vector3 Direction = transform.localPosition + Vector3.right * _InternalForce;
        Vector3 _Force = Vector3.Lerp(transform.localPosition, Direction, (Time.deltaTime * _ForceMultiplier * 1000f));

        _Force.x = Mathf.Clamp(_Force.x, _ClampPos.x, _ClampPos.y);
        transform.localPosition = _Force; 
    }

    void _SetRight()
    {
        _InternalForce -= _AddForce;

        StartCoroutine(DelayToReset());
    }

    void _SetLeft()
    {
        _InternalForce += _AddForce;

        StartCoroutine(DelayToReset());
    }

    IEnumerator DelayToReset()
    {
        yield return new WaitForSeconds(_DelayTimeToResetForce);
        _InternalForce = 0;
    }
}
