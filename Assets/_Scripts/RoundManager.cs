using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RoundManager : MonoBehaviour
{
    //public float timeBetweenSpawns = 10f; // Default patience duration for the current round
    [SerializeField] public bool GameplayTimersActive = false;


    public float currentTimer;
    public bool timerRunning;
    public List<RoundConfigSO> roundProfiles;

    public string[] allViews = 
    {
        "Counter", "Cheese 1", "Cheese 2", "Cheese 3"
    };

    [Header("Camera View Settings")]
    public string currentView;
    public CameraManager cameraManagerScript;
    public List<GameObject> cameraTargets = new List<GameObject>();


    [Header("Round Profile Settings")]
    [SerializeField] public int CurrentRound = 1;
    [SerializeField] public int RatsToSpawn;
    [SerializeField] public float SpawnRate;
    [SerializeField] public float DifficultyModifier;

    [Header("Rat Profile Holder")]
    [SerializeField] public List<RatProfileSO> ratProfiles;

    [Header("Spawned Rats")]
    [SerializeField] public GameObject currentRat;
    [SerializeField] public List<GameObject> queuedRats = new List<GameObject>();

    [Header("Rat Checkpoint Locations")]
    [SerializeField] public List<GameObject> locations = new List<GameObject>();


    [Header("Round States")]
    RoundBaseState currentState;
    public RoundLoadState loadState = new RoundLoadState();
    public RoundStartState startState = new RoundStartState();
    public RoundRatIntroState ratIntroState = new RoundRatIntroState();
    public RoundCuttingState cuttingState = new RoundCuttingState();
    public RoundServingState servingState = new RoundServingState();
    public RoundEndState endState = new RoundEndState();

    [Header("CoRoutine setup")]
    public float introDelay = 1f;
    private Coroutine activeRoutine;


    void Start()
    {
        currentState = loadState;
        currentState.EnterState(this);
        cameraManagerScript = GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

        if (!GameplayTimersActive) 
            return;

        if (!timerRunning) 
            return;

        currentTimer -= Time.deltaTime;

        if (currentTimer <= 0f)
        {
            currentTimer = 0f;
            timerRunning = false;

            OnSpawnTimerFinished();
        }
    }

    public void SwitchState(RoundBaseState newState)
    {
        Debug.Log("State switch from: " + currentState + " > " + newState);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void StartSpawnTimer()
    {
        currentTimer = SpawnRate;
        timerRunning = true;
    }

    public void OnSpawnTimerFinished()
    {
        Debug.Log("Rat Spawn Timer Finished - new rat should spawn");
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
            var ratController = rat.GetComponent<RatController>();
            ratController.moveSpeed = 5;
            ratController.preferredFlavour = selectedProfile.preferredFlavour.ToString();
            ratController.initialSpend = selectedProfile.initialSpend;
            ratController.tipMin = selectedProfile.tipMin;
            ratController.tipMax = selectedProfile.tipMax;

            // Disable until spawned later
            rat.SetActive(false);

            // Store in queue/list
            queuedRats.Add(rat);

            // Set initial position to first location (waitlist spawn point)
            rat.transform.position = locations[0].transform.position;

            //Debug.Log("Queued rat: " + selectedProfile.ratName);
        }
    }

    public void MoveRatToCounter(GameObject rat)
    {
        rat.GetComponent<RatController>().MoveTo(locations[1].transform.position);
    }

    //todo
    public void ShowUiMessage()
    {
        Debug.Log("UI MESSAGE: RAT ARRIVAL");
    }

    public void RunRatIntroSequence()
    {
        if (activeRoutine != null)
            StopCoroutine(activeRoutine);

        activeRoutine = StartCoroutine(RatIntroRoutine());
    }

    private IEnumerator RatIntroRoutine()
    {
        // Wait for movement to finish
        while (currentRat != null && currentRat.GetComponent<RatController>().isMoving)
        {
            yield return null;
        }

        ShowUiMessage();

        yield return new WaitForSeconds(introDelay);

        SwitchState(cuttingState);
    }

    public void ChangeCameraView(string cameraView)
    {
        if (cameraView == currentView)
            Debug.Log("Already in this view");
            return;

        if (cameraView == "Counter")
        {
            cameraManagerScript.LookCounter();
            currentView = cameraView;
        }

        else if (cameraView == "Cheese1")
        {

            currentView = cameraView;
        }
        
        else if (cameraView == "Cheese2")
        {

            currentView = cameraView;
        }
        
        else if (cameraView == "Cheese3")
        {

            currentView = cameraView;
        }
        
    }
}
