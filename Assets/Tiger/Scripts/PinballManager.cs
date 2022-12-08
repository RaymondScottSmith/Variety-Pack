using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PinballManager : MonoBehaviour
{

    [Header("Prefabs")] [SerializeField] private GameObject basicBallPrefab;
    
    [Header("UI")]
    [SerializeField] private TMP_Text livesText;

    [Header("Other")] [SerializeField] private Transform ballSpawnLocation;
    [SerializeField] private int startingLives = 3;
    [SerializeField] private TigerCage tigerCage;


    public static PinballManager Instance;

    private int lives;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
        lives = startingLives;
        livesText.text = "Lives: " + lives;
    }

    public void DestroyBall(GameObject ball)
    {
        if (ball.CompareTag("Ball"))
        {
            Destroy(ball);


            StartCoroutine(KillReplaceBall());

        }
    }

    private IEnumerator KillReplaceBall()
    {
        yield return new WaitForFixedUpdate();
        Pinball[] ballsInScene = FindObjectsOfType<Pinball>();
        if (ballsInScene.Length <= 0)
        {
            UpdateLives(-1);
            SpawnBall();
        }
    }

    public void UpdateLives(int change)
    {
        lives += change;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            Debug.Log("Lose Code Here");
        }
    }

    public void SpawnBall()
    {
        Instantiate(basicBallPrefab, ballSpawnLocation.position, basicBallPrefab.transform.rotation);
        tigerCage.SetBlocker(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
