using UnityEngine;

public class OldUnityInput : MonoBehaviour, IInput
{
    public float Horizontal { get; private set; }

    public bool Jump { get; private set; }
    public bool Punch { get; private set; }

    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Jump = Input.GetButtonDown("Jump");
        Punch = Input.GetButtonDown("Fire1");
    }
}