using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private List<Present> collectedPresents = new List<Present> ();
    public int collectedPresentsNum = 0;

    private RandomAudioPlayer randomAudioPlayer;
    public AudioClip[] damageAudioClips;

    public GameObject bag;
    public Spawner spawner;

    public GameMenu gameMenu;

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
            collectedPresentsNum -= 1;
            ScaleBag(1 + collectedPresentsNum * 0.1f);
            gameMenu.UpdatePresentsText(collectedPresentsNum);
            randomAudioPlayer.PlayRandomClip(damageAudioClips);

            if (collectedPresentsNum < 0)
            {
                gameMenu.DisplayGameOver(collectedPresentsNum);
            }
        }

        Present present = other.gameObject.GetComponent<Present>();
        if (present != null && !collectedPresents.Contains(present))
        {
            present.CollectPresent();
            collectedPresents.Add(present);
            collectedPresentsNum += 1;
            ScaleBag(1 + collectedPresentsNum * 0.1f);
            gameMenu.UpdatePresentsText(collectedPresentsNum);

            spawner.SpawnPresentsAndElves();
        }
    }

    private void ScaleBag(float scale)
    {
        bag.transform.localScale = Vector3.one * scale;
    }
}
