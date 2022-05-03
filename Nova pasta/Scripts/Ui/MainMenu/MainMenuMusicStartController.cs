using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicStartController : MonoBehaviour
{
    [SerializeField] AudioSource _Source;
    [SerializeField] List<AudioClip> _Musics;
    void Start()
    {
        int index = Random.Range(0, _Musics.Count);
        _Source.clip = _Musics[index];
        _Source.Play();
    }
}
