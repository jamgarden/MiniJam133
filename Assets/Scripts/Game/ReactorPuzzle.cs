using UnityEngine;
using UnityEngine.Events;

public class ReactorPuzzle : MonoBehaviour
{
    [SerializeField] private Turret[] turrets;
    [SerializeField] private PunchableEventTriggerer[] buttons;
    [SerializeField] private int[] correctButtonSequence;
    [SerializeField] private SpriteRenderer[] healthBlips;
    [SerializeField] private UnityEvent OnSolvePuzzle;
    [SerializeField] private UnityEvent OnMeltdown;

    private int health;
    private int positionInSequence = 0;

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onPunched.AddListener(delegate { OnButtonPressed(index); });
        }
        health = healthBlips.Length;
    }

    public void OnButtonPressed(int buttonIndex)
    {
        Debug.Log(buttonIndex);
        if (correctButtonSequence[positionInSequence] == buttonIndex)
            positionInSequence++;
        else
            FailPuzzle();

        if (positionInSequence >= correctButtonSequence.Length)
            SolvePuzzle();
    }

    private void FailPuzzle()
    {
        for(int i = 0;i < turrets.Length; i++)
        {
            turrets[i].Shoot();
        }
        health--;
        positionInSequence = 0;

        if (health < 0)
            OnMeltdown?.Invoke();
        for (int i = 0; i < healthBlips.Length; i++)
        {
            if (i > health)
                healthBlips[i].color = Color.red;
            else
                healthBlips[i].color = Color.green;
        }
    }

    private void SolvePuzzle()
    {
        OnSolvePuzzle?.Invoke();
    }
}