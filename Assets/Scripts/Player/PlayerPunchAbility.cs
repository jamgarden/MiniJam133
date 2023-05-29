using UnityEngine;
using UnityEngine.Pool;

public class PlayerPunchAbility : MonoBehaviour
{
    [SerializeField] private GameObject punchEffectPrefab;
    [SerializeField] private float punchCooldown;
    [SerializeField] private float punchRadius;

    private ObjectPool<GameObject> punchEffectsPool;
    private IInput input;
    private Vector2 punchPosition;
    private float cooldownTimer;

    private void Awake()
    {
        input = GetComponent<IInput>();

        InitializeObjectPool();
    }

    private void InitializeObjectPool()
    {
        punchEffectsPool = new ObjectPool<GameObject>(
            () => Instantiate(punchEffectPrefab),
            punchEffect => punchEffect.SetActive(true),
            punchEffect => punchEffect.SetActive(false),
            punchEffect => Destroy(punchEffect));
    }

    private void Update()
    {
        SetPunchDirection();
        Punch();
        DecrementPunchCooldown();
    }

    private void SetPunchDirection()
    {
        if (input.Horizontal < 0)
            punchPosition = transform.TransformPoint(Vector2.left);
        else if (input.Horizontal > 0)
            punchPosition = transform.TransformPoint(Vector2.right);
    }

    private void Punch()
    {
        if (input.Punch == false || cooldownTimer > 0)
            return;

        cooldownTimer = punchCooldown;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, punchRadius);

        foreach (Collider2D collider in colliders)
        {
            IPunchable punchable = collider.GetComponent<IPunchable>();
            if (punchable == null)
                continue;

            punchable.Punch();
        }
        SpawnPunchEffect();
    }

    private void SpawnPunchEffect()
    {
        if (punchEffectPrefab == null)
            return;

        GameObject punchEffect = punchEffectsPool.Get();
        punchEffect.transform.position = punchPosition;
    }

    private void DecrementPunchCooldown()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }
}