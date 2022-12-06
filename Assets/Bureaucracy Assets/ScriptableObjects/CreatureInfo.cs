using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "ScriptableObjects/SeaCreatureInfo")]
public class CreatureInfo : ScriptableObject
{

    public GameObject creaturePrefab;

    public CreatureType creatureType;

    public List<string> validNames;

    public List<string> validFoods;

    public string waterType;

    public string origin;

    public string sportTeam;

}
