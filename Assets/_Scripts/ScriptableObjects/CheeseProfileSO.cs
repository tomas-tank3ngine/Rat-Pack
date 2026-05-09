using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheeseProfileSO", menuName = "Scriptable Objects/CheeseProfileSO")]
public class CheeseProfileSO : ScriptableObject
{
    public string cheeseName;
    public string cheeseFlavour;
    public float cheeseWeight;
    public List<string> dialogueLines;
    public GameObject cheesePrefab;
}
