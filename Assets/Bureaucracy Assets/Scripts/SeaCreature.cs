using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCreature : MonoBehaviour
{
    
    public Vector3 startPosition;

    public Vector3 windowPosition;

    public Vector3 leavePosition;

    public float speed = 3f;

    private float startTime;

    private float entryLength;
    private float exitLength;

    public bool arrived;

    public bool leaving;

    public bool isDolphin = false;
    
    struct Answers
    {
        public bool isLying;
        public string name;
        public CreatureType species;
        public string food;

        public Answers(bool isLying, string name, CreatureType species, string fact1)
        {
            this.isLying = isLying;
            this.name = name;
            this.species = species;
            this.food = fact1;
        }
    }

    private Answers myAnswers;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        leaving = false;
        // Calculate the journey length.
        entryLength = Vector3.Distance(startPosition, windowPosition);
        exitLength = Vector3.Distance(windowPosition, leavePosition);
    }

    public void AssignAnswers(bool isLying, string name, CreatureType species, string fact1)
    {
        myAnswers = new Answers(isLying, name, species, fact1);
    }


    public string CreatureName()
    {
        return myAnswers.name;
    }

    public CreatureType myType()
    {
        return myAnswers.species;
    }

    public string FactOne()
    {
        return myAnswers.food;
    }

    public void Leave()
    {
        
        leaving = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (!arrived)
        {
            if (Vector3.Distance(transform.position, windowPosition) < 0.1f)
            {
                arrived = true;
                startTime = 0f;
                if (!isDolphin)
                {
                    DialogueManager.Instance.StartDialogue();
                    DialogueManager.Instance.ShowCanvas();
                }
                else
                {
                    DialogueManager.Instance.HideCanvas();
                }
                return;
            }
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / entryLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Slerp(startPosition, windowPosition, fractionOfJourney);
        }

        if (leaving)
        {
            if (startTime == 0f)
            {
                startTime = Time.time;
            }

            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / exitLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Slerp(windowPosition, leavePosition, fractionOfJourney);
        }

        if (Vector3.Distance(transform.position, leavePosition) < 0.1f)
        {
            BurManager.Instance.SpawnNewCreature();
            Destroy(gameObject);
        }
        
        
    }
}
