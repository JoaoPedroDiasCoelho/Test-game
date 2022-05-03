using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] PlayerConfig _PlayerConfig;
    [SerializeField] PlayerInputs _PlayerInputs;
    [SerializeField] Transform _PlayerBody;
    [SerializeField] float TimeLerp = 1.3f;
    float xRotation = 0f;
    float rotate;
    Vector3 ToInput;
    void Update()
    {
        Vector3 _Input = _PlayerInputs.Look();
        ToInput = Vector3.Lerp(ToInput, _Input, Time.deltaTime * 10f * TimeLerp);

        float mouseX = ToInput.x * _PlayerConfig.MouseSense * 150f * Time.deltaTime;
        float MouseY = ToInput.y * _PlayerConfig.MouseSense * 150f * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, rotate);
        _PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
