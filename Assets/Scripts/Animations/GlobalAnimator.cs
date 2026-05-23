using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GlobalAnimator : MonoBehaviour
{
    private Animator animator;

    private readonly int runHash = Animator.StringToHash("Run");
    private readonly int hitHash = Animator.StringToHash("Hit");
    private readonly int deadHash = Animator.StringToHash("Dead");
    private readonly int attackHash = Animator.StringToHash("Attack");

    [Header("Layer Settings")]
    public float layerTransitionSpeed = 10f;
    private int bodyLayerIndex;
    private float targetBodyWeight = 0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        bodyLayerIndex = animator.GetLayerIndex("Body");
    }

    void Update()
    {
        // Gövde katmanı ağırlığını yumuşak bir şekilde ayarla
        if (bodyLayerIndex != -1)
        {
            float currentWeight = animator.GetLayerWeight(bodyLayerIndex);
            float newWeight = Mathf.Lerp(currentWeight, targetBodyWeight, Time.deltaTime * layerTransitionSpeed);
            animator.SetLayerWeight(bodyLayerIndex, newWeight);
        }
    }

    public void SetCombatState(bool inCombat)
    {
        targetBodyWeight = inCombat ? 1f : 0f;
    }

    public void TriggerAttack() { if (animator != null) animator.SetTrigger(attackHash); }
    public void SetRunning(bool isRunning) { if (animator != null) animator.SetBool(runHash, isRunning); }
    public void TriggerHit() { if (animator != null) animator.SetTrigger(hitHash); }
    public void TriggerDeath() { if (animator != null) animator.SetTrigger(deadHash); }
}