using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpHeight = 7f;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private PlayerHealthBar healthBar;
    [HideInInspector] public float currentHealth;

    private float gravity = -15f;
    private Vector3 velocity;
    private CharacterController characterController;

    private const string IS_ENEMY = "Enemy";

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = transform.right * xInput + transform.forward * zInput;

        characterController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (characterController.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(IS_ENEMY)) return;
        currentHealth -= 20f;        
        healthBar.SetHealth(currentHealth);
    }
}
