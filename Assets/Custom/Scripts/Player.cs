using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private List<Present> collectedPresents = new List<Present> ();
    public int collectedPresentsNum = 0;

    private string appliedPowerUpType;
    public float defaultSprintSpeed = 5.25f;
    public float poweredSprintSpeed = 8f;

    public Transform powerUpTransform;

    private RandomAudioPlayer randomAudioPlayer;
    private ThirdPersonController thirdPersonController;
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
        thirdPersonController = GetComponent<ThirdPersonController>();
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
            appliedPowerUpType = present.powerUpType;
            float powerUpDuration = present.powerUpDuration;
            GameObject powerUpIcon = present.powerUpIcon;

            present.CollectPresent();
            collectedPresents.Add(present);
            collectedPresentsNum += 1;
            ScaleBag(1 + collectedPresentsNum * 0.1f);
            gameMenu.UpdatePresentsText(collectedPresentsNum);

            ApplyPowerUp(appliedPowerUpType, powerUpIcon, powerUpDuration);

            spawner.SpawnPresentsAndElves();
        }
    }

    private void ScaleBag(float scale)
    {
        bag.transform.localScale = Vector3.one * scale;
    }

    private void ApplyPowerUp(string powerUpType, GameObject powerUpIcon, float powerUpDuration)
    {
        if (powerUpType == "SPEED")
        {
            thirdPersonController.SprintSpeed = poweredSprintSpeed;
        }

        Instantiate(powerUpIcon, powerUpTransform.position, Quaternion.identity, powerUpTransform);
        Invoke("EndPowerUp", powerUpDuration);
    }

    void EndPowerUp()
    {
        if (appliedPowerUpType == "SPEED")
        {
            thirdPersonController.SprintSpeed = defaultSprintSpeed;
        }

        Destroy(powerUpTransform.GetChild(0).gameObject);
        appliedPowerUpType = null;
    }
}
