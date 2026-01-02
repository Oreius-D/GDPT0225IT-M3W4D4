using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundClip;

    private void Start()
    {
        //Start background music
        SoundFXManager.Instance.PlayMusic(backgroundClip, 0.4f);
    }
}
