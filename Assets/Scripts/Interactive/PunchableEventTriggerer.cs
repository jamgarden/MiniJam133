using UnityEngine;
using UnityEngine.Events;

public class PunchableEventTriggerer : MonoBehaviour, IPunchable
{
    public UnityEvent onPunched;

    public void Punch()
    {
        onPunched?.Invoke();
    }
}