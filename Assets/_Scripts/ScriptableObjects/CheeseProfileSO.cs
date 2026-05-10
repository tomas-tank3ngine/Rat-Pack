using UnityEngine;

[CreateAssetMenu(fileName = "CheeseProfileSO", menuName = "Scriptable Objects/CheeseProfileSO")]
public class CheeseProfileSO : ScriptableObject
{
    public string cheeseName;
    public GameObject cheesePrefab;

    public FlavourType cheeseFlavour;
    public float weightPerSlice = 10f;

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