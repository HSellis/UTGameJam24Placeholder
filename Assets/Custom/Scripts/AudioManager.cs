using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    private AudioSource audioSource;

    //public AudioClip menuButtonAudioClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persist across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate SoundManager objects
        }

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMenuButtonSound()
    {
        audioSource.Play();
    }
}
