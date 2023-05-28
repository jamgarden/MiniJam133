public class PauseState : AIState
{
    public override void ExecuteState(AIBrain brain)
    {
        brain.Horizontal = 0;
    }
}