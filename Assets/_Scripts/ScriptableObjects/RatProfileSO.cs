using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;

[CreateAssetMenu(fileName = "RatProfileSO", menuName = "Scriptable Objects/RatProfileSO")]
public class RatProfileSO : ScriptableObject
{
    public string ratName;
    //    public float patienceDuration;
    public List<string> dialogueLines;
    public GameObject ratPrefab;
    //public float moveSpeed;
    public int initialSpend;
    public int tipMin;
    public int tipMax;
    public int requestedWeight;


    [SerializeField] public FlavourType preferredFlavour;

    public enum FlavourType
    {
        Soft,
        Crumbly,
        Fresh,
        Aged,
        Mild,
        Funky
    }
}
