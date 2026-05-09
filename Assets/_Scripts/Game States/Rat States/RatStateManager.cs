using System.Xml;
using UnityEngine;

public class RatStateManager : MonoBehaviour
{
    RatBaseState currentState;
    public RatReactState ReactState = new RatReactState();
    public RatCounterState CounterState = new RatCounterState();
    public RatQueueState QueueState = new RatQueueState();
    public RatHiddenState HiddenState = new RatHiddenState();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = HiddenState;

        currentState.EnterState(this);

    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(RatBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
