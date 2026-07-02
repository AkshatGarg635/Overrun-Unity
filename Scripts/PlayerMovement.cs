using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    public float groundCheckDistance = 0.6f;
    public LayerMask groundLayer;

    public float maxEnergy = 100f;
    public float currentEnergy;
    public float energyCost = 30f;
    public float rechargeRate = 20f;
    public float pulseRadius = 5f;
    public float pulseForce = 8f;
    public Slider EnergyBar;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentEnergy = maxEnergy;
        EnergyBar.maxValue = maxEnergy;
        EnergyBar.value = currentEnergy;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Energy recharge
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += rechargeRate * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
            EnergyBar.value = currentEnergy;
        }

        // Pulse ability
        if (Input.GetKeyDown(KeyCode.C) && currentEnergy >= energyCost)
        {
            Pulse();
            currentEnergy -= energyCost;
            EnergyBar.value = currentEnergy;
        }
    }

    void Pulse()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, pulseRadius);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    Vector3 direction = (enemy.transform.position - transform.position).normalized;
                    enemyRb.AddForce(direction * pulseForce, ForceMode.Impulse);
                }
            }
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Move relative to where player is facing
        Vector3 move = transform.right * h + transform.forward * v;
        move.y = 0f;

        rb.linearVelocity = new Vector3(move.x * speed, rb.linearVelocity.y, move.z * speed);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, pulseRadius);
    }
}