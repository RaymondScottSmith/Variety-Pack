using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Answers", menuName = "ScriptableObjects/Answers")]
public class AnswerOptions : ScriptableObject
{
    //0-4 should start with Q
    //5-9 should be 2 part names
    public List<string> names;
}
