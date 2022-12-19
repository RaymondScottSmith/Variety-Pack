using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JA_WordSpawner : MonoBehaviour
{
    public GameObject wordPrefab;
    [SerializeField] private Transform wordCanvas;
    [SerializeField] private Transform groundSpawn;
    public WordDisplay SpawnWord()
    {
        //Vector3 randomPosition = new Vector3(Random.Range(130f, 820f), 700f);
        GameObject wordObj = Instantiate(wordPrefab, groundSpawn.position, Quaternion.identity, wordCanvas);

        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();
        return wordDisplay;
    }
}
