using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerManager : MonoBehaviour
{
    public static RunnerManager Instance;

    [Header("Controls")] public float moveSpeed = 1f;

    public Animator fadeAnimator;

    void Awake()
    {
        if (RunnerManager.Instance == null)
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
        
    }

    public void StopMovement()
    {
        moveSpeed = 0f;
    }

    public void MenuButton()
    {
        StartCoroutine(ReturnToMenu());
    }

    private IEnumerator ReturnToMenu()
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
