using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects instance { get; private set; }

    private AudioSource audioSource;
    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BombExplosionSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
