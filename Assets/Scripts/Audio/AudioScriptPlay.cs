using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScriptPlay : MonoBehaviour
{
    public AudioClip[] Audio;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayAuido()
    {
        int randomIndex = Random.Range(0, Audio.Length);
        audioSource.clip = Audio[randomIndex];
        audioSource.PlayOneShot(audioSource.clip);
    }
}
