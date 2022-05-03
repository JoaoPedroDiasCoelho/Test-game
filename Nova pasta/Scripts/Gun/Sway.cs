using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] float SmoothTime = 5.5f;
    [SerializeField] float Range = 3f;
    [SerializeField] PlayerInputs _Inputs;
    //Internal
    Vector3 StartRotation;
    float SmoothRotationX;
    float SmoothRotationY;
    void Start()
    {
        StartRotation = transform.localEulerAngles;
    }

    private void LateUpdate()
    {
        Vector3 val = _Inputs.Look();
        SmoothRotationX = Mathf.Lerp(SmoothRotationX, val.x, (Time.deltaTime * SmoothTime));
        SmoothRotationY = Mathf.Lerp(SmoothRotationY, val.y, (Time.deltaTime * SmoothTime));
        transform.localRotation = Quaternion.Euler(new Vector3(SmoothRotationY, 0f, SmoothRotationX) * Range);
    }
}
