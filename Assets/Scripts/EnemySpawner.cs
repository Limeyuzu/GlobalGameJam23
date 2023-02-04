using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject lawnyMowy;
    float spawnTimer;
    float timeToRespawn = 10f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnLawnyMowy();
        spawnTimer = timeToRespawn;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTimer <= 0)
        {
            SpawnLawnyMowy();
            spawnTimer = timeToRespawn;
        } else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    void SpawnLawnyMowy()
    {
        Instantiate(lawnyMowy, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
