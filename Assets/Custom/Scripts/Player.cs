using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public GameObject bag;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }

        Present present = other.gameObject.GetComponent<Present>();
        if (present != null)
        {
            Destroy(present.gameObject);
            ScaleBag(1.1f);
        }
    }

    private void ScaleBag(float scale)
    {
        bag.transform.localScale = Vector3.Scale(bag.transform.localScale, new Vector3(scale, scale, scale));
    }
}
