using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundConfigSO", menuName = "Scriptable Objects/RoundConfigSO")]
public class RoundConfigSO : ScriptableObject
{
    public int roundNumber;
    public int ratCount;
    public float spawnRate;
    public float DifficultyModifier;
}
