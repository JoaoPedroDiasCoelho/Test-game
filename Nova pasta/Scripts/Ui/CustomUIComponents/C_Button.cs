using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using TMPro;
using UnityEngine.UI;

namespace CustomUI
{
    [Serializable]
    class SoundsSettings
    {
        [HideInInspector] public AudioSource _Source;
        public AudioClip _PointEnter;
        public AudioClip _PointClick;
    }

    [RequireComponent(typeof(AudioSource))]
    public class C_Button : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [Header("Setup Text")]
        [SerializeField] TMP_Text _Text;
        [SerializeField] Color32 _TEnterColor;
        [SerializeField] Color32 _TNormalColor;
        [SerializeField] Color32 _TClickColor;
        [Header("Setup Button")]
        [SerializeField] Image _Image;
        [SerializeField] Color32 _BEnterColor;
        [SerializeField] Color32 _BNormalColor;
        [SerializeField] Color32 _BClickColor;
        [SerializeField] SoundsSettings _Sounds;
        public UnityEvent _ClickEvent;
        public UnityEvent _EnterEvent;

        void Awake()
        {
            _Sounds._Source = GetComponent<AudioSource>();

            if(_Text){
                _Text.color = _TNormalColor;
            }

            _Image.color = _BNormalColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_Text){
                _Text.color = _TNormalColor;
            }

            _Image.color = _BClickColor;

            _Sounds._Source.PlayOneShot(_Sounds._PointClick);

            _ClickEvent.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(_Text){
                _Text.color = _TNormalColor;
            }

            _Image.color = _BEnterColor;

            _Sounds._Source.PlayOneShot(_Sounds._PointEnter);

            _EnterEvent.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(_Text){
                _Text.color = _TNormalColor;
            }
            
            _Image.color = _BNormalColor;
        }
    }
}