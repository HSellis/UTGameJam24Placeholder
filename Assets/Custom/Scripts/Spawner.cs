using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float y;

    public GameObject PresentPrefab;
    public GameObject ElfPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPresentsAndElves()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        Vector3 presentLocation = new Vector3(x, y, z);
        GameObject presentObj = Instantiate(PresentPrefab, presentLocation, Quaternion.identity);
        GameObject elfObj = Instantiate(ElfPrefab, presentLocation, Quaternion.identity);
        Elf elf = elfObj.GetComponent<Elf>();
        elf.centerPoint = presentObj.transform;
    }
}
