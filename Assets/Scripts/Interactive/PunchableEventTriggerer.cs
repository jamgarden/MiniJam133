using UnityEngine;
using UnityEngine.Events;

public class PunchableEventTriggerer : MonoBehaviour, IPunchable
{
    [SerializeField] private UnityEvent onPunched;

    public void Punch()
    {
        onPunched?.Invoke();
    }
}