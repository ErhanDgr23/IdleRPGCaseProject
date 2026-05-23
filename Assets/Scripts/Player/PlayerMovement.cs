using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick Joystick;

    private Rigidbody rb;
    private PlayerStats stats;
    private Vector3 movementInput;

    private PlayerCombat combatScript;
    private GlobalAnimator globalAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<PlayerStats>();
        combatScript = GetComponent<PlayerCombat>();
        globalAnimator = GetComponent<GlobalAnimator>();
    }

    void Update()
    {
        float moveX = Joystick.Horizontal;
        float moveZ = Joystick.Vertical;
        movementInput = new Vector3(moveX, 0f, moveZ).normalized;
    }

    void FixedUpdate()
    {
        // Fiziğe dayalı hareket
        rb.linearVelocity = new Vector3(movementInput.x * stats.moveSpeed, rb.linearVelocity.y, movementInput.z * stats.moveSpeed);

        bool isRunning = movementInput != Vector3.zero;

        // Tek animasyonlu koşma bool'unu tetikle
        if (globalAnimator != null)
        {
            globalAnimator.SetRunning(isRunning);
        }

        // SADECE yakında hedef YOKSA karakterin yüzünü hareket yönüne çevir.
        // Hedef varsa karakter geri geri düz bir şekilde koşacak (Arcade hissiyatı)
        if (isRunning && (combatScript == null || combatScript.CurrentTarget == null))
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }
}