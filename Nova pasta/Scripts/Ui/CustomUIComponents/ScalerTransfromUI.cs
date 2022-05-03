using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScalerTransfromUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float _SmoothTime = 15f;
    [SerializeField] float _Scale = 1.1f;
    [SerializeField] Transform _Transform;
    bool IsPointEnter;
    float _XStartScale;

    void Start()
    {
        _XStartScale = transform.localScale.x;

        if(_Transform == null)
        {
            _Transform = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsPointEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsPointEnter = false;
    }

    void LateUpdate()
    {
        if(IsPointEnter && transform.localScale.x != _Scale)
        {
            Vector3 SmoothScale = Vector3.Lerp(_Transform.localScale, new Vector3(_Scale, _Scale, 0f), Time.deltaTime * _SmoothTime);
            _Transform.localScale = SmoothScale;
        }

        else if(!IsPointEnter && transform.localScale.x != _XStartScale)
        {
            Vector3 SmoothScale = Vector3.Lerp(_Transform.localScale, new Vector3(_XStartScale, _XStartScale, 0f), Time.deltaTime * _SmoothTime);
            _Transform.localScale = SmoothScale;
        }
    }
}
