using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSound : MonoBehaviour
{
    public AudioClip healSound;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHealSound()
    {
        audioSource.PlayOneShot(healSound,0.6f);
    }
}
