using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_Spawner : MonoBehaviour
{
    public static Run_Spawner Instance;
    public GameObject doorPrefab;

    public List<GameObject> doorPool;
    public float respawnRate = 3f;

    public List<GameObject> targets;

    public int targetCount;

    public Vector2 spawnXMinMax;
    public Vector2 spawnYMinMax;
    public Vector2 spawnZMinMax;

    public LayerMask interactLayer;

    private int numToIncrease = 16;
    private int currentMade;

    private bool spawnTargets;

    public GameObject instructions;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        instructions.SetActive(true);
        InvokeRepeating("SpawnDoor", 0,respawnRate);
        
    }

    public void StartTargetSpawn()
    {
        instructions.SetActive(false);
        spawnTargets = true;
    }
    
    

    public void SpawnDoor()
    {
        if (spawnTargets)
            SpawnTarget();
        //Instantiate(doorPrefab, transform.position, doorPrefab.transform.rotation);
        doorPool[0].transform.position = transform.position;
        doorPool[0].SetActive(true);
        doorPool.RemoveAt(0);
    }

    public void SpawnTarget()
    {

        if (currentMade >= numToIncrease)
        {
            targetCount++;
            currentMade = 0;
            numToIncrease = targetCount * 4;
        }
        //count = Random.Range(2, 6);
        float targetRadius = targets[0].GetComponent<SphereCollider>().radius;
        for (int i = 0; i < targetCount; i++)
        {

            Vector3 spawnPoint = RandomVector3(spawnXMinMax, spawnYMinMax, spawnZMinMax);
            //Assuming you are 2D
            Collider[] CollisionWithEnemy = Physics.OverlapSphere(spawnPoint, targetRadius, interactLayer);
            //If the Collision is empty then, we can instantiate
            int counter = 0;
            while (CollisionWithEnemy.Length != 0)
            {
                spawnPoint = RandomVector3(spawnXMinMax, spawnYMinMax, spawnZMinMax);
                CollisionWithEnemy = Physics.OverlapSphere(spawnPoint, targetRadius, interactLayer);
                counter++;
                //Debug.Log("Issue With Collision");
                if (counter > 10)
                {
                    break;
                }
            }

            targets[0].transform.position = spawnPoint;
            targets[0].SetActive(true);
            targets[0].GetComponentInChildren<Run_Ghost>().alive = true;
            targets.RemoveAt(0);
            currentMade++;
        }
        
    }

    private Vector3 RandomVector3(Vector2 xMinMax, Vector2 yMinMax, Vector2 zMinMax)
    {
        float x = Random.Range(xMinMax.x, xMinMax.y);
        float y = Random.Range(yMinMax.x, yMinMax.y);
        float z = Random.Range(zMinMax.x, zMinMax.y);

        return new Vector3(x, y, z);
    }

    public void ReturnDoorToPool(GameObject door)
    {
        doorPool.Add(door);
    }

    public void ReturnTargetToPool(GameObject target)
    {
        targets.Add(target);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
