using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    public float rotationSpeed = 50f; // Speed of rotation
    public float floatSpeed = 2f;     // Speed of floating up and down
    public float floatHeight = 0.5f;  // How high/low the object floats

    private Vector3 startPosition;
    private RandomAudioPlayer randomAudioPlayer;

    public AudioClip[] presentOpenClips;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        randomAudioPlayer = GetComponent<RandomAudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the Y-axis
        RotateObject();

        // Move the object up and down smoothly
        FloatObject();
    }

    void RotateObject()
    {
        // Rotate the object based on rotation speed and time
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    void FloatObject()
    {
        // Create a smooth up and down motion using a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }

    public void CollectPresent()
    {
        randomAudioPlayer.PlayRandomClip(presentOpenClips);
        Destroy(gameObject, 0.5f);
    }
}
