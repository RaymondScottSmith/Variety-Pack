using System;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using UnityEditor;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;
    int inputIndex = 0;

    private int numSpawned = 0;

    public bool done;

    private bool imDone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AssignKey(Melanchall.DryWetMidi.MusicTheory.NoteName noteName)
    {
        noteRestriction = noteName;
    }
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, Conductor.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (Conductor.GetAudioSourceTime() >= timeStamps[spawnIndex] - Conductor.Instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
                numSpawned++;
            }
        }

        if (timeStamps.Count > 0 && !imDone)
        {
            if (numSpawned >= timeStamps.Count)
            {
                Conductor.Instance.LaneComplete();
                imDone = true;
            } 
        }
        

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = Conductor.Instance.marginOfError;
            double audioTime = Conductor.GetAudioSourceTime() - (Conductor.Instance.inputDelayInMilliseconds / 1000.0);

            /*
            if (Input.GetKeyDown(input))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit();
                    print($"Hit on {inputIndex} note");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    print($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            }
            */
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                //Debug.Log($"Missed {inputIndex} note");
                inputIndex++;
            }
        }

    }
    private void Hit()
    {
        Debug.Log("Red Lane Hit");
    }
    private void Miss()
    {
        //Debug.Log("Missed a Note");
        //RhythmManager.Instance.MissedNote();
    }
}
