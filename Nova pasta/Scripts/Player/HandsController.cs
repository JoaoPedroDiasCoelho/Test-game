using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    Animator _HandsAnimator;
    PlayerStatesEvents _PlayerStates;
    private void Awake()
    {
        _PlayerStates = GetComponentInParent<PlayerStatesEvents>();
    }

    void Update()
    {
        if(TryGetComponent(out Animator _HandsAnimator))
        {
            _HandsAnimator.enabled = !_PlayerStates.PlayerAiming;
            if(_PlayerStates.PlayerAiming)
            {
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }
}
