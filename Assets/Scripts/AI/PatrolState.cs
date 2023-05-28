using UnityEngine;

public class PatrolState : AIState
{
    private float reverseDirectionTimer;
    private bool moveRight;
    private AIBrain brain;
    private bool initialized;

    public override void ExecuteState(AIBrain brain)
    {
        if (initialized == false)
            Initialize(brain);

        brain.Horizontal = moveRight ? 1f : -1f;

        if (AtLedge() || AgainstWall())
        {
            ReverseDirection();
            Debug.Log("AA");
        }

        ReverseDirectionOnTimer();
    }

    private void Initialize(AIBrain brain)
    {
        this.brain = brain;

        if (Random.value > 0.5f)
            moveRight = true;
        reverseDirectionTimer = Random.Range(brain.MinReverseDirectionTime, brain.MaxReverseDirectionTime);
        initialized = true;
    }

    private void ReverseDirectionOnTimer()
    {
        reverseDirectionTimer -= Time.deltaTime;

        if (reverseDirectionTimer < 0f)
        {
            reverseDirectionTimer = Random.Range(brain.MinReverseDirectionTime, brain.MaxReverseDirectionTime);
            ReverseDirection();
        }
    }

    private bool AtLedge()
    {
        Vector2 rayOrigin = new Vector2(brain.transform.position.x + brain.Horizontal, brain.transform.position.y);
        return !Physics2D.Raycast(rayOrigin, Vector2.down, brain.LedgeDetectRayDistance);
    }

    private bool AgainstWall()
    {
        return Physics2D.Raycast(brain.transform.position, Vector2.right * brain.Horizontal, brain.WallDetectRayDistance, brain.WallLayer);
    }

    private void ReverseDirection()
    {
        moveRight = !moveRight;
    }
}