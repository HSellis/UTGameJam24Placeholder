using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private List<Present> collectedPresents = new List<Present> ();
    public float bagSize = 1.0f;

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
            bagSize -= 0.1f;
            Debug.Log(bagSize);
            ScaleBag(bagSize);
            randomAudioPlayer.PlayRandomClip(damageAudioClips);

            if (bagSize < 0.5f)
            {
                Debug.Log("You lost!");
            }
        }

        Present present = other.gameObject.GetComponent<Present>();
        if (present != null && !collectedPresents.Contains(present))
        {
            present.CollectPresent();
            collectedPresents.Add(present);
            bagSize += 0.1f;
            Debug.Log(bagSize);
            ScaleBag(bagSize);

            spawner.SpawnPresentsAndElves();
        }
    }

    private void ScaleBag(float scale)
    {
        bag.transform.localScale = new Vector3(scale, scale, scale);
    }
}
