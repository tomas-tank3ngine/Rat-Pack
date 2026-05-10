using UnityEngine;

public class RoundServingState : RoundBaseState
{
    public override void EnterState(RoundManager round)
    {
        round.cheeseController.ServeCurrentCut();

        round.SwitchState(round.endState);
    }

    public override void UpdateState(RoundManager round)
    {
    }
}