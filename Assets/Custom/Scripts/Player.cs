using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private RandomAudioPlayer randomAudioPlayer;
    public AudioClip[] damageAudioClips;

    public GameObject bag;
    public Spawner spawner;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        randomAudioPlayer = GetComponent<RandomAudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Elf elf = other.gameObject.GetComponent<Elf>();
        if (elf != null)
        {
            ScaleBag(0.9f);
            randomAudioPlayer.PlayRandomClip(damageAudioClips);
        }

        Present present = other.gameObject.GetComponent<Present>();
        if (present != null)
        {
            Destroy(present.gameObject);
            ScaleBag(1.1f);

            spawner.SpawnPresentsAndElves();
        }
    }

    private void ScaleBag(float scale)
    {
        bag.transform.localScale = Vector3.Scale(bag.transform.localScale, new Vector3(scale, scale, scale));
    }
}
