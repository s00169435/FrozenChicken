using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : Activity
{
    AudioSource audioSource;
    [SerializeField] AudioClip song;
    [SerializeField] float songPlayTime;

    private void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }
    public override void OnInteract()
    {
        base.OnInteract();
    }

    IEnumerator PlayMusic()
    {
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(song);

        yield return new WaitForSeconds(songPlayTime);

        audioSource.Stop();

    }
}
