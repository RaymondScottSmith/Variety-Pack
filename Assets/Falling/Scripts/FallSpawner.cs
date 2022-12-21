using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fallPrefab;

    [SerializeField] private Transform leftMax, rightMax;

    private Vector3 left, right;

    public float spawnRate = 0.5f;

    public int maxPerSpawn = 1;
    // Start is called before the first frame update
    void Start()
    {
        left = leftMax.position;
        right = rightMax.position;
        
        InvokeRepeating("StartSpawning", 0, spawnRate);
    }

    private void StartSpawning()
    {
        if (maxPerSpawn > 1)
        {
            int max = Random.Range(1, maxPerSpawn + 1);
            for (int i = 0; i < max; i++)
            {
                SpawnFaller();
            }
        }
        else
        {
            SpawnFaller();
        }
    }

    private void SpawnFaller()
    {
        float xRand = Random.Range(left.x, right.x);
        Vector3 spawnPoint = new Vector3(xRand, left.y, 0);
        Instantiate(fallPrefab, spawnPoint, fallPrefab.transform.rotation);
    }
}
