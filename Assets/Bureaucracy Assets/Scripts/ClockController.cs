using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField] private GameObject minuteHand;

    [SerializeField] private GameObject secondHand;
    
    [SerializeField] private AudioClip endMusic;

    private bool timesUp;
    // Start is called before the first frame update
    void Start()
    {
        timesUp = false;
        StartCoroutine(SecondMovement());
        StartCoroutine(MinuteMovement());
    }

    private IEnumerator SecondMovement()
    {
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(5f);

            secondHand.transform.rotation = Quaternion.Euler(new Vector3(secondHand.transform.rotation.eulerAngles.x,
                secondHand.transform.rotation.eulerAngles.y, secondHand.transform.rotation.eulerAngles.z + (360 / 12)));
        }

        StartCoroutine(SecondMovement());
    }
    
    private IEnumerator MinuteMovement()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(60f);

            minuteHand.transform.rotation = Quaternion.Euler(new Vector3(minuteHand.transform.rotation.eulerAngles.x,
                minuteHand.transform.rotation.eulerAngles.y, minuteHand.transform.rotation.eulerAngles.z + (360 / 12)));
        }

        //yield return new WaitForSeconds(5);
        OfficeCamera.Instance.TurnOffMusic();
        timesUp = true;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!timesUp)
        {
            return;
        }
        StopAllCoroutines();
        DialogueManager.Instance.HideCanvas();
        BurManager.Instance.ShowEndPanel();
        BurManager.Instance.GetComponent<AudioSource>().PlayOneShot(endMusic);
        timesUp = false;
    }
}
