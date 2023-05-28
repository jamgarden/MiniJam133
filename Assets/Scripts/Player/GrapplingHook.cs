using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int lineRendererLength;
    [SerializeField] private LayerMask ignoreLayers;
    [SerializeField] private float hookTravelTime = 0.1f;
    [SerializeField] private int wobbles;
    [SerializeField] private float wobbleStrength;
    [SerializeField] private float winchSpeed;
    [SerializeField] private AnimationCurve velocityCurve;
    [SerializeField] private AnimationCurve minWobbleCurve;
    [SerializeField] private AnimationCurve maxWobbleCurve;
    [SerializeField] private AudioClip grappleSound;
    [SerializeField] private float unhookDistance;

    private Vector2 hookPosition;
    private Vector2 initialDirection;
    private float grappleTimer;
    private bool hooked;
    private float velocityMultiplier;
    private float initialDistance;

    private void Awake()
    {
        ignoreLayers = ~ignoreLayers;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(FireHook());
        }

        if (hooked)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, Vector2.zero);
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(hookPosition));
            float distance = Vector2.Distance(transform.position, hookPosition);

            velocityMultiplier = velocityCurve.Evaluate(math.remap(initialDistance, unhookDistance, 0, 1, distance));

            if (distance <= unhookDistance)
                Unhook();
        }
    }

    public void Unhook()
    {
        if (hooked == false)
            return;

        hooked = false;
        lineRenderer.enabled = false;
        StartCoroutine(AddUnhookForce());
    }

    private IEnumerator AddUnhookForce()
    {
        float timer = 0.1f;
        while (timer > 0) 
        {
            playerRigidbody.AddForce((initialDirection + Vector2.up) * 50);
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FireHook()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mousePosition, Color.red, 1);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (mousePosition - (Vector2)transform.position).normalized, 100, ignoreLayers);
        grappleTimer = 0;

        if (hit != false)
        {
            if (grappleSound != null)
                AudioManager.Instance.PlaySound(grappleSound);
            hooked = false;
            initialDistance = Vector2.Distance(hit.point, transform.position);
            initialDirection = (hit.point - (Vector2)transform.position).normalized;
            lineRenderer.enabled = true;
            lineRenderer.positionCount = lineRendererLength;
            float wobbleCurveLerp = UnityEngine.Random.value;
            bool reverseWobble = UnityEngine.Random.value > 0.5f;
            while (grappleTimer < hookTravelTime)
            {
                grappleTimer += Time.deltaTime;
                float timeRemapped = math.remap(0, hookTravelTime, 1, 0, grappleTimer);

                for (int i = lineRendererLength - 1; i >= 0; i--)
                {
                    float indexRemapped = math.remap(0, lineRendererLength - 1, 0, 1, i);
                    Vector2 wobbleDirection = Vector2.Perpendicular(hit.point - (Vector2)transform.position).normalized * 0.3f;
                    float wobbleCurveValue = Mathf.Lerp(minWobbleCurve.Evaluate(indexRemapped), maxWobbleCurve.Evaluate(indexRemapped), wobbleCurveLerp);
                    wobbleDirection *= Mathf.Sin(0.1f + (Mathf.PI / 2 * timeRemapped * wobbleCurveValue * wobbles)) * wobbleStrength;
                    wobbleDirection /= (indexRemapped + 0.1f) * 10f;
                    if (reverseWobble)
                        wobbleDirection = -wobbleDirection;

                    lineRenderer.SetPosition(i, Vector2.Lerp(Vector2.zero, transform.InverseTransformPoint(hit.point), indexRemapped * (1 - timeRemapped)) + wobbleDirection);
                }
                yield return null;
            }
            hookPosition = hit.point;
            hooked = true;
        }
    }

    private void FixedUpdate()
    {
        if (hooked == false)
            return;

        Vector2 direction = (hookPosition - (Vector2)transform.position).normalized;
        playerRigidbody.AddForce(direction * winchSpeed * velocityMultiplier);
        Debug.Log(velocityMultiplier);
    }
}