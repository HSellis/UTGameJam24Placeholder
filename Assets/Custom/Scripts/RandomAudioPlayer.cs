using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // Auto-assign if not set
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Call this method to play a random audio clip
    public void PlayRandomClip(AudioClip[] audioClips)
    {
        if (audioClips.Length > 0)
        {
            // Select a random index in the array
            int randomIndex = Random.Range(0, audioClips.Length);
            AudioClip randomClip = audioClips[randomIndex];

            // Play the randomly selected audio clip
            audioSource.clip = randomClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No audio clips assigned to play!");
        }
    }
}
