using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance; //Instance of class, to be called everywhere

    [Header("SoundFX")]
    [SerializeField] private AudioSource soundFXObject; //Object for game sounds

    [Header("Music")]
    [SerializeField] private AudioSource backGroundMusic; //Object for game music

    private void Awake()
    {
        //Istantiate if null
        if (Instance == null)
            Instance = this;
    }

    // ==========================
    //        SOUND FX
    // ==========================
    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //Spawn game object
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //assing the audioClip
        audioSource.clip = audioClip;

        //Assign Volume
        audioSource.volume = volume;

        //Play sound
        audioSource.Play();

        //Get length of sound FX clip
        float clipLenght = audioSource.clip.length;

        //Destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLenght);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        //Assign a random index
        int rand = Random.Range(0, audioClip.Length);

        //Spawn game object
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //assing the audioClip
        audioSource.clip = audioClip[rand];

        //Assign Volume
        audioSource.volume = volume;

        //Play sound
        audioSource.Play();

        //Get length of sound FX clip
        float clipLenght = audioSource.clip.length;

        //Destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLenght);
    }


    // ==========================
    //        MUSIC API
    // ==========================
    public void PlayMusic(AudioClip clip, float volume = -1f, bool loop = true)
    {
        //Check if clip is null
        if (clip == null)
            return;

        //Set elements of backGroundMusic
        backGroundMusic.clip = clip;        //Clip to play
        backGroundMusic.volume = volume;    //Volume
        backGroundMusic.loop = loop;        //Loop

        //Play the clip
        backGroundMusic.Play();
    }
}
