using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip BackgroundMusic;
    // Start is called before the first frame update
    void Awake()
    {
    }
    void Start()
    {
        musicSource.clip = BackgroundMusic;
        musicSource.Play();
    }

}
