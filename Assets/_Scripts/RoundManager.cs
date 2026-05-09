using UnityEngine;
using System.Collections.Generic;

public class RoundManager : MonoBehaviour
{
    //public float timeBetweenSpawns = 10f; // Default patience duration for the current round
    [SerializeField] public bool RoundRunning = false;


    public float currentTimer;
    public bool timerRunning;
    public List<RoundConfigSO> roundProfiles;

    [Header("Round Settings")]
    [SerializeField] public int CurrentRound = 1;
    [SerializeField] public int RatsToSpawn;
    [SerializeField] public float SpawnRate;
    [SerializeField] public float DifficultyModifier;

    [Header("Rat Profile Holder")]
    [SerializeField] public List<RatProfileSO> ratProfiles;

    [Header("Spawned Rats")]
    [SerializeField] public List<GameObject> queuedRats = new List<GameObject>();

    [Header("Rat Placement Locations")]
    [SerializeField] public List<GameObject> locations = new List<GameObject>();


    [Header("Round State Manager")]
    RoundBaseState currentState;
    public RoundLoadState loadState = new RoundLoadState();
    public RoundStartState startState = new RoundStartState();
    public RoundRatIntroState ratIntroState = new RoundRatIntroState();
    public RoundCuttingState cuttingState = new RoundCuttingState();
    public RoundServingState servingState = new RoundServingState();
    public RoundEndState endState = new RoundEndState();




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = loadState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
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

    public void SwitchState(RoundBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void StartSpawnTimer()
    {
        currentTimer = SpawnRate;
        timerRunning = true;
    }

    public void OnTimerFinished()
    {
        Debug.Log("Rat lost patience!");
        // lose star
        //destroy rat?
        //play animation
    }

    //Detect which round we are in and set the appropriate settings
    public void LoadRoundSettings()
    {
        foreach (var roundProfile in roundProfiles)
        {
            if (CurrentRound == roundProfile.roundNumber)
            {
                RatsToSpawn = roundProfile.ratCount;
                SpawnRate = roundProfile.spawnRate;
                DifficultyModifier = roundProfile.DifficultyModifier;

                Debug.Log("Loaded Round " + roundProfile.roundNumber);

                break;
            }
        }
    }

    //Using settings from the current round, spawn that many rats (choosing randomly from the selection of rat profiles,
    //and set inactive until they are spawned in th
    public void GenerateRatQueue()
    {        
        for (int i = 0; i < RatsToSpawn; i++)
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

            // Set initial position to first location (waitlist spawn point)
            rat.transform.position = locations[0].transform.position;

            Debug.Log("Queued rat: " + selectedProfile.ratName);
        }
    }
}
