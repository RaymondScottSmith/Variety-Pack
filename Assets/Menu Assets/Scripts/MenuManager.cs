using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator sceneAnimator;
    public void LoadScene(int sceneNumber)
    {
        StartCoroutine(LoadSceneAnimation(sceneNumber));
    }

    private IEnumerator LoadSceneAnimation(int sceneNumber)
    {
        sceneAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneNumber);
    }
}
