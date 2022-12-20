using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JA_WordTimer : MonoBehaviour
{
    public WordManager wordManager;

    public float wordDelay = 1.5f;

    private float nextWordTime = 0f;

    public int creatureCount;

    public int maxCreatures = 100;

    public bool isOver = true;

    [SerializeField] private TMP_Text countdownTimer;
    // Start is called before the first frame update
    void Start()
    {
        wordManager = GetComponent<WordManager>();
        creatureCount = 0;
        isOver = true;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        for (int i = 0; i <= 3; i++)
        {
            countdownTimer.text = (3 - i).ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownTimer.text = "";
        isOver = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOver)
        {
            return;
        }
        if (creatureCount > maxCreatures)
        {
            return;
        }
        if (Time.time >= nextWordTime)
        {
            creatureCount++;
            wordManager.AddWord();
            nextWordTime = Time.time + wordDelay;
            //So that the spawn delay slowly gets faster and faster
            wordDelay *= 0.99f;
            if (creatureCount > maxCreatures)
            {
                StartCoroutine(StartBoss());
            }
        }
    }

    private IEnumerator StartBoss()
    {
        yield return new WaitForSeconds(3f);
        WordManager.Instance.SpawnJabber();
    }
}
