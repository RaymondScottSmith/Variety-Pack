using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Vector2 leftSpawnPoint;

    [SerializeField] private Vector2 rightSpawnPoint;

    [SerializeField] private float startDelay = 1f;

    [SerializeField] private float spawnDelay = 1f;

    private bool spawnLeft;

    public bool isRandom;
    // Start is called before the first frame update
    void Start()
    {
        if (!isRandom)
        {
            InvokeRepeating("SpawnLeftRight", startDelay, spawnDelay);
        }
    }

    private void SpawnLeftRight()
    {
        if (spawnLeft)
        {
            spawnLeft = false;
            Instantiate(platformPrefab, leftSpawnPoint, platformPrefab.transform.rotation);
        }
        else
        {
            spawnLeft = true;
            Instantiate(platformPrefab, rightSpawnPoint, platformPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
