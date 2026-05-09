using UnityEngine;

public abstract class RoundBaseState
{
    public abstract void EnterState(RoundManager round);
    public abstract void UpdateState(RoundManager round);
}
