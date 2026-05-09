using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float patienceDuration = 10f; // Default patience duration for the current round

    private float currentTimer;
    private bool timerRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

    void StartPatienceTimer()
    {
        currentTimer = patienceDuration;
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
