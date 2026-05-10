using UnityEngine;

public class RoundCuttingState : RoundBaseState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void EnterState(RoundManager round)
    {
        round.GameplayTimersActive = true;

        if (round.currentRat != null)
        {
            round.currentRat
                .GetComponent<RatController>()
                .ResumePatience();
        }
    }
    public override void UpdateState(RoundManager round)
    {

    }
}
