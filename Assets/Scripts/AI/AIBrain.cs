using UnityEngine;

public class AIBrain : MonoBehaviour, IInput
{
    public float Horizontal { get; set; }

    public bool Jump { get; private set; }

    public bool Punch { get; private set; }

    [field: SerializeField]
    public LayerMask WallLayer { get; private set; }

    [field: SerializeField]
    public float MinReverseDirectionTime { get; private set; }

    [field: SerializeField]
    public float MaxReverseDirectionTime { get; private set; }

    [field: SerializeField]
    public float LedgeDetectRayDistance { get; private set; }

    [field: SerializeField]
    public float WallDetectRayDistance { get; private set; }


    [SerializeField] private float stateChangeTime;
    private float stateChangeTimer;

    private AIState currentState;
    private AIState nextState;

    private void Awake()
    {
        currentState = new PatrolState();
        nextState = new PauseState();
    }

    private void Update()
    {
        currentState.ExecuteState(this);

        stateChangeTimer -= Time.deltaTime;
        if (stateChangeTimer < 0)
        {
            stateChangeTimer = stateChangeTime;
            (currentState, nextState) = (nextState, currentState);
        }
    }
}