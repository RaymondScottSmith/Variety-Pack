using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JA_WordSpawner : MonoBehaviour
{
    public List<GameObject> wordPrefabs;

    public GameObject jabberPrefab;
    [SerializeField] private Transform wordCanvas;
    [SerializeField] private Transform topSpawn;
    [SerializeField] private Transform bottomSpawn;
    [SerializeField] private Transform jabberSpawn;
    
    public WordDisplay SpawnWord()
    {
        Vector3 randomPosition = new Vector3(topSpawn.position.x, Random.Range(bottomSpawn.position.y, topSpawn.position.y));
        GameObject wordObj = Instantiate(wordPrefabs[Random.Range(0, wordPrefabs.Count)], randomPosition, Quaternion.identity, wordCanvas);
        wordObj.transform.SetAsFirstSibling();
        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();
        return wordDisplay;
    }

    public WordDisplay SpawnJabber()
    {
        GameObject jabberObj = Instantiate(jabberPrefab, jabberSpawn.position, Quaternion.identity, wordCanvas);
        WordDisplay wordDisplay = jabberObj.GetComponent<WordDisplay>();
        return wordDisplay;
    }
}
