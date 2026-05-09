using UnityEngine;

public abstract class RatBaseState
{
    public abstract void EnterState(RatStateManager rat);
    public abstract void UpdateState(RatStateManager rat);
}
