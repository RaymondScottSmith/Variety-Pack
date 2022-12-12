using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine.Networking;

public class Conductor : MonoBehaviour
{
    public static Conductor Instance;

    
    public static MidiFile midiFile;
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position in seconds
    public float songPosition;

    //Current song position in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;
    
    public AudioSource musicScore;

    public int beatsShownInAdvance = 5;

    [SerializeField] private GameObject notePrefab;
    [SerializeField]
    public Object myMidi;

    //Keep all the position-in-beats of notes in the song
    public MusicNote[] notes;

    [SerializeField] private float musicStartDelay;

    [SerializeField] private float minX, maxX;

    private bool musicStarted;
    
    //The index of the next note to be spawned
    private int nextIndex = 0;


    //Below are from new version

    [SerializeField] private List<Lane> lanes;
    
    //public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    
    //Possibly remove
    public double marginOfError = 0; // in seconds

    public float songDelayInSeconds = 3;

    public int inputDelayInMilliseconds = 0;
    
    [Header("Note Spawn Data")]
    //How Long should the note be on the screen
    public float noteTime = 2f;
    public float noteSpawnY;
    public float noteTapY;
    public float noteTappedY;
    public float noteDespawnY
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;
    
    

    
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
        
        

        //StartCoroutine(StartMusic());
        
    }

    public void StartSong(int songNumber)
    {
        if (songNumber == 1)
        {
            ReadMidiFromFile();
        
            musicStarted = true;
            musicScore = GetComponent<AudioSource>();
            secPerBeat = 60f / songBpm;
            dspSongTime = (float)AudioSettings.dspTime;
        }
    }

    private void ReadMidiFromFile()
    {
        midiFile = MidiFile.Read(AssetDatabase.GetAssetPath(myMidi));
        //Invoke(nameof(GetDataFromMidi), songDelayInSeconds);
        GetDataFromMidi();
    }

    private void GetDataFromMidi()
    {
        var noteHolder = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[noteHolder.Count];
        noteHolder.CopyTo(array,0);
        StartCoroutine(StartMusic((int)musicStartDelay));
        foreach (Lane lane in lanes)
        {
            lane.SetTimeStamps(array);

        }

        //StartMusic();
    }

    public void LaneComplete()
    {
        foreach (var lane in lanes)
        {
            if (!lane.done)
            {
                lane.done = true;
                break;
            }
        }

        foreach (var lane in lanes)
        {
            if (!lane.done)
            {
                return;
            }
        }
        StartCoroutine(SongDone());
    }

    private IEnumerator SongDone()
    {
        yield return new WaitForSeconds(5f);
        musicScore.Stop();
        yield return new WaitForSeconds(2f);
        RhythmManager.Instance.ShowEndPanel();
    }

    

    private IEnumerator StartMusic(int delay)
    {
        for (int i = 0; i < delay; i++)
        {
            timerText.text = (delay - i).ToString();
            yield return new WaitForSeconds(1f);
        }
        
        musicStarted = true;
        musicScore.Play();
        timerText.text = "Go!";
        yield return new WaitForSeconds(0.5f);
        timerText.text = "";
    }

    public IEnumerator PauseMusic()
    {
        musicScore.volume = 0f;
        yield return new WaitForSeconds(0.5f);
        musicScore.volume = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        /*
        if (!musicStarted)
        {
            return;
        }
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        songPositionInBeats = songPosition / secPerBeat;
        if (nextIndex < notes.Length && notes[nextIndex].note < songPositionInBeats + beatsShownInAdvance)
        {
            float startX = Random.Range(minX, maxX);
            //Instantiate note here
            //Vector3 noteSpawn = new Vector3(startX, notePrefab.GetComponent<Note>().SpawnPos.y, 0);
            GameObject noteObj = Instantiate(notePrefab, notePrefab.GetComponent<Note>().SpawnPos, notePrefab.transform.rotation);
            //noteObj.GetComponent<Note>().AssignValues(newNote);

            noteObj.GetComponent<Note>().Init(notes[nextIndex].note, notes[nextIndex].noteColor);
            
            nextIndex++;
        }
        
        */
    }
    
    public static double GetAudioSourceTime()
    {
        return (double)Instance.musicScore.timeSamples / Instance.musicScore.clip.frequency;
    }
}

public enum NoteColor
{
    Red,
    Green,
    Blue,
    Yellow
}
