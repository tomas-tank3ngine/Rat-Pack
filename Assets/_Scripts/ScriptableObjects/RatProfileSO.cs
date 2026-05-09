using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RatProfileSO", menuName = "Scriptable Objects/RatProfileSO")]
public class RatProfileSO : ScriptableObject
{
    public string ratName;
    public string preferredFlavour;
    public float patienceDuration;
    public List<string> dialogueLines;
    public GameObject ratPrefab;
}
