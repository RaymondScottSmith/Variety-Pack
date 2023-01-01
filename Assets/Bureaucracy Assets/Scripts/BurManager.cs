using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

//using Debug = System.Diagnostics.Debug;

public class BurManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> creaturePrefabs;

    [SerializeField] private List<CreatureInfo> creatures;

    [SerializeField] private int testCreatureNumber;
    
    public TMP_Text commScreen;

    public SeaCreature currentCreature;

    public bool currentLying;

    [SerializeField] private GameObject dolphinPrefab;

    public static BurManager Instance;

    public GameObject net;

    [SerializeField] private Animator lightAnimator;

    [SerializeField] private AudioClip passButtonSound;

    [SerializeField] private AudioClip netButtonSound;
    [SerializeField] private AudioClip dolphinTauntSound;
    [SerializeField] private AudioClip dolphinCatchMusic;
    [SerializeField] private AudioClip angryFishMusic;
    [SerializeField] private AudioClip goodPassMusic;

    

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private int score = 0;

    private AudioSource myAudioSource;

    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text endScoreText;

    [SerializeField] private int oddsOfDolphin = 4;

    public Animator fadeAnimator;

    private int highScore;
    void Awake()
    {
        Instance = this;
    }

    public void AddPoints(int numAdded)
    {
        score += numAdded;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("DolphinScore");
        endPanel.SetActive(false);
        myAudioSource = GetComponent<AudioSource>();
        foreach (CreatureInfo creature in creatures)
        {
            DialogueManager.Instance.dolphinNames.AddRange(creature.validNames);
        }
        
        SpawnNewCreature();
    }

    public void SpawnNewCreature()
    {
        int liarChance = Random.Range(0, oddsOfDolphin);

        if (liarChance == 0)
        {
            currentLying = true;
        }
        else
        {
            currentLying = false;
        }
        
        int creatureNum = Random.Range(0, creatures.Count);
        /*
        SeaCreature creature = creaturePrefabs[creatureNum].GetComponent<SeaCreature>();
        creature.AssignAnswers(false, "Quade", CreatureType.Whale, "I love cheerios.");
        Instantiate(creature.gameObject, creature.startPosition, creature.transform.rotation);
        */
        
        currentCreature = creatures[creatureNum].creaturePrefab.GetComponent<SeaCreature>();
        currentCreature = Instantiate(currentCreature.gameObject, currentCreature.startPosition, currentCreature.transform.rotation).GetComponent<SeaCreature>();
        DialogueManager.Instance.EnterDialogueMode(creatures[creatureNum], currentLying);
        
        
        string randomName = creatures[creatureNum].validNames[Random.Range(0, creatures[creatureNum].validNames.Count)];
        string randomFood = creatures[creatureNum].validFoods[Random.Range(0, creatures[creatureNum].validFoods.Count)];
        currentCreature.AssignAnswers(false, randomName, creatures[creatureNum].creatureType, randomFood);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreText.text = "SCORE: " + score;
        highScoreText.text = "HIGHSCORE: " + highScore;
        /*
        if (highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("DolphinScore", highScore);
        }
        */
    }

    public void ShowEndPanel()
    {
        
        endPanel.SetActive(true);
        endScoreText.text = "SCORE: " + score;
        if (highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("DolphinScore", highScore);
        }
    }

    public void HitPassButton()
    {
        OfficeCamera.Instance.freeSwitch = false;
        myAudioSource.PlayOneShot(passButtonSound);
        DialogueManager.Instance.HideCanvas();
        if (currentLying)
        {
            AddPoints(-50);
            StartCoroutine(DolphinTaunt());
            Debug.Log("Handle Failure Here");
            //Code to handle failure here
        }
        else
        {
            AddPoints(10);
            Debug.Log("Hit Pass Button");
            StartCoroutine(CreatureLeave());
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        StartCoroutine(ReturnMenuAnimation());
    }

    private IEnumerator ReturnMenuAnimation()
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    public void HitNetButton()
    {
        OfficeCamera.Instance.freeSwitch = false;
        myAudioSource.PlayOneShot(netButtonSound);
        DialogueManager.Instance.HideCanvas();
        if (currentLying)
        {
            AddPoints(10);
            StartCoroutine(CatchDolphin());
            //Add code to catch dolphin
        }
        else
        {
            AddPoints(-20);
            StartCoroutine(AngryCreatureLeave());
            Debug.Log("Add failure consequence for false accusation here.");
        }
    }

    private IEnumerator CatchDolphin()
    {
        
        Destroy(currentCreature.gameObject);
        currentCreature = Instantiate(dolphinPrefab).GetComponent<SeaCreature>();
        currentCreature.isDolphin = true;
        yield return new WaitForSeconds(1f);
        myAudioSource.PlayOneShot(dolphinCatchMusic);
        net.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        commScreen.text = "Drat! You caught me. Curse you Tuna!";
        yield return new WaitForSeconds(3f);
        currentCreature.GetComponent<SeaCreature>().RemoveCaughtDolphin();
    }
    
    private IEnumerator AngryCreatureLeave()
    {
        yield return new WaitForSeconds(0.5f);
        myAudioSource.PlayOneShot(angryFishMusic);
        lightAnimator.SetTrigger("Light");
        commScreen.text = "How Dare You! You'll be hearing from my attorneys!";
        yield return new WaitForSeconds(3f);
        currentCreature.Leave();
    }

    private IEnumerator DolphinTaunt()
    {
        
        Destroy(currentCreature.gameObject);
        currentCreature = Instantiate(dolphinPrefab).GetComponent<SeaCreature>();
        currentCreature.isDolphin = true;
        OfficeCamera.Instance.TurnOffMusic();
        yield return new WaitForSeconds(0.5f);
        myAudioSource.PlayOneShot(dolphinTauntSound);
        //currentCreature.arrived = true;
        commScreen.text = "Hah! You fell for it you stupid Tuna!";
        yield return new WaitForSeconds(3f);
        currentCreature.Leave();
    }

    private IEnumerator CreatureLeave()
    {
        yield return new WaitForSeconds(0.5f);
        myAudioSource.PlayOneShot(goodPassMusic);
        commScreen.text = "Thanks. Now I'm late!";
        yield return new WaitForSeconds(1f);
        currentCreature.Leave();
    }

    public void AskName()
    {
        commScreen.text = "My name is " +
                          currentCreature.CreatureName()
                          + ". Pleased to meet you.";
    }

    public void AskFood()
    {
        commScreen.text = "I happen to enjoy the occasional "
                          + currentCreature.FactOne() +
                          ". When I'm in the mood.";
    }


}

public enum CreatureType
{
    Whale,
    Dolphin,
    Dugong,
    GreatWhite,
    Hammerhead,
    MantaRay,
    SeaTurtle,
    Sunfish,
    Swordfish
}
