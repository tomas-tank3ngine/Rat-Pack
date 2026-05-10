using UnityEngine;

public class RoundRatIntroState : RoundBaseState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void EnterState(RoundManager round)
    {
        round.MoveRatToCounter(round.currentRat);
        round.RunRatIntroSequence();
        round.GameplayTimersActive = false;

        if (round.currentRat != null)
        {
            round.currentRat
                .GetComponent<RatController>()
                .PausePatience();
        }
    }
    public override void UpdateState(RoundManager round)
    {

    }
}
