using UnityEngine;

public class RoundGameEndState : RoundBaseState
{    
    public override void EnterState(RoundManager round)
    {
        Debug.Log("Entered end game State");

        // show score screen
        // disable controls
        // calculate bonuses
        // todo
    }
    public override void UpdateState(RoundManager round)
    {

    }
}
