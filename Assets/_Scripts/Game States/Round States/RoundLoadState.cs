using UnityEngine;

public class RoundLoadState : RoundBaseState
{
    public override void EnterState(RoundManager round)
    {
        round.roundEnded = false;
        round.LoadRoundSettings();
        round.GenerateRatQueue();

        round.SwitchState(round.startState);
    }
    public override void UpdateState(RoundManager round)
    {

    }
}
