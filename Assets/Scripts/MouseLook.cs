using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("Configurações")]
    public float mouseSensitivity = 25f;
    public Transform playerBody; // Arraste o Player para aqui no Inspector

    private float xRotation = 0f;
    private Vector2 lookInput;

    void Start()
    {
        // Trava o mouse no centro da tela e o esconde
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Chamado pelo Player Input (Message: OnLook)
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {
        // Calcula a rotação baseada no movimento do mouse
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Rotação Vertical (Cima/Baixo) - Limitada a 90 graus para não dar cambalhota
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Aplica a rotação na câmera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotação Horizontal (Esquerda/Direita) - Gira o corpo do player junto
        playerBody.Rotate(Vector3.up * mouseX);
    }
}