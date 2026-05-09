using UnityEngine;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour
{
    //public float timeBetweenSpawns = 10f; // Default patience duration for the current round
    [SerializeField] private bool RoundRunning = false;


    private float currentTimer;
    private bool timerRunning;
    public List<RoundConfigSO> roundProfiles;

    [Header("Round Settings")]
    [SerializeField] private int CurrentRound = 0;
    [SerializeField] private int RatsToSpawn;
    [SerializeField] private float SpawnRate;
    [SerializeField] private float DifficultyModifier;

    [Header("Rat Profile Holder")]
    [SerializeField] private List<RatProfileSO> ratProfiles;

    [Header("Spawned Rats")]
    [SerializeField] private List<GameObject> queuedRats = new List<GameObject>();




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Detect which round we are in and set the appropriate settings
        foreach (var roundProfile in roundProfiles)
        {
            if (CurrentRound == roundProfile.roundNumber)
            {
                Debug.Log("Round " + roundProfile.roundNumber + " started with " + roundProfile.ratCount + " rats and spawn rate of " + roundProfile.spawnRate);
                RatsToSpawn = roundProfile.ratCount;
                SpawnRate = roundProfile.spawnRate;
                DifficultyModifier = roundProfile.DifficultyModifier;

                StartSpawnTimer();
                break;
            }
        }
        //Using settings from the current round, spawn that many rats (choosing randomly from the selection of rat profiles,
        //and set inactive until they are spawned in th
        while (RatsToSpawn > 0)
        {
            // Pick random rat profile
            RatProfileSO selectedProfile = ratProfiles[Random.Range(0, ratProfiles.Count)];

            // Create rat
            GameObject rat = Instantiate(selectedProfile.ratPrefab);

            // Rename for organization
            rat.name = selectedProfile.ratName;

            // Disable until spawned later
            rat.SetActive(false);

            // Store in queue/list
            queuedRats.Add(rat);

            Debug.Log("Queued rat: " + selectedProfile.ratName);

            RatsToSpawn--;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!timerRunning)
            return;

        currentTimer -= Time.deltaTime;

        if (currentTimer <= 0f)
        {
            currentTimer = 0f;
            timerRunning = false;

            OnTimerFinished();
        }
    }

    void StartSpawnTimer()
    {
        currentTimer = SpawnRate;
        timerRunning = true;
    }

    void OnTimerFinished()
    {
        Debug.Log("Rat lost patience!");
        // lose star
        //destroy rat?
        //play animation
    }
}
