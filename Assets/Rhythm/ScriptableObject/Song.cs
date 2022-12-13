using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "ScriptableObjects/Song")]
public class Song : ScriptableObject
{
    public AudioClip songMP3;
    public string pPScoreName;
    public List<Melanchall.DryWetMidi.MusicTheory.NoteName> notes;
    public Object midiFile;
    public float SongStartDelay;
}
