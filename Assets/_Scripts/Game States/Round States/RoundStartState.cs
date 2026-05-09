using UnityEngine;

public class RoundStartState : RoundBaseState
{
    public override void EnterState(RoundManager round)
    {
        round.queuedRats[0].SetActive(true);
        round.SwitchState(round.ratIntroState);
    }
    public override void UpdateState(RoundManager round)
    {
        // Rat moves to counter
        //Rat says their flavour request
        
    }
}
