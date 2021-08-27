using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : Activity
{
    AudioSource audioSource;
    [SerializeField] AudioClip song;
    [SerializeField] float songPlayTime;

    new void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }
    public override void OnInteract()
    {
        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    {
        canInteract = false;
        if (!audioSource.isPlaying)
        {
            audioSource.time = 20;
            audioSource.Play();
        }
            

        yield return new WaitForSeconds(songPlayTime);

        audioSource.Stop();
        StartCoroutine(StartCooldown());
    }
}
