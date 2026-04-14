using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class SimpleMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    [Header("Configurações")]
    public float moveSpeed = 5.0f;
    public float gravity = -9.81f;

    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Este método é chamado pelo componente Player Input (Broadcast Messages)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        ApplyMovement();
        ApplyGravity();
    }

    private void ApplyMovement()
    {
        // Converte o input 2D em movimento 3D relativo ao jogador
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        moveDirection = transform.TransformDirection(moveDirection);

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        // Aplica uma gravidade simples para o player não flutuar
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        controller.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
    }
}