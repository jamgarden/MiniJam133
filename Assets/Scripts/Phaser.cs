using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phaser : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    AnimationCurve flashCurve;

    [SerializeField]
    float timeFactor = 0.5f;

    Image backDrop;

    float refreshTimer = 0f;
    void Start()
    {
        backDrop = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        refreshTimer += Time.deltaTime * timeFactor;
        Color testColor = backDrop.color;
        backDrop.color = new Color(testColor.r, testColor.g, testColor.b, flashCurve.Evaluate(refreshTimer));
        if(refreshTimer > 1)
        {
            refreshTimer = 0;
        }
    }
}
