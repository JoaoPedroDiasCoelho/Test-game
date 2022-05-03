using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private void Update()
    {

    }

    public Vector3 Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 _move = new Vector3(x, 0f, y);
        return _move;
    }

    public Vector3 Jump()
    {
        float jumpForce = Input.GetAxis("Jump");
        Vector3 _jump = new Vector3(0f, jumpForce, 0f);
        return _jump;
    }

    public Vector3 Look()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");
        Vector3 _look = new Vector3(x, y, 0f);
        return _look;
    }
}
