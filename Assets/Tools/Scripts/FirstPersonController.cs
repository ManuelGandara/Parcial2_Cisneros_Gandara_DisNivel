using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform _headTransform; // Transform de la c�mara

    [Header("Inputs")]
    [Range(50.0f, 1000.0f)]
    [SerializeField] private float _mouseSensitivity = 300.0f;
    private float _xAxis, _zAxis, _inputMouseX, _inputMouseY, _mouseX, _mouseY;

    [Header("Physics")]
    [SerializeField] private float _movSpeed = 5.0f;

    private Vector3 _dir;
    private Rigidbody _rb;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        // Captura el input de movimiento y rotaci�n
        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");

        _dir = (transform.right * _xAxis + transform.forward * _zAxis).normalized;

        _inputMouseX = Input.GetAxisRaw("Mouse X");
        _inputMouseY = Input.GetAxisRaw("Mouse Y");

        // Llama a la funci�n de rotaci�n si hay movimiento en el mouse
        if (_inputMouseX != 0 || _inputMouseY != 0)
        {
            Rotation(_inputMouseX, _inputMouseY);
        }
    }

    private void FixedUpdate()
    {
        // Mueve el personaje en la direcci�n especificada si hay input
        if (_xAxis != 0 || _zAxis != 0)
        {
            Movement(_dir);
        }
    }

    private void Movement(Vector3 dir)
    {
        // Mueve el personaje usando Rigidbody
        _rb.MovePosition(transform.position + dir * _movSpeed * Time.fixedDeltaTime);
    }

    private void Rotation(float x, float y)
    {
        // Rotaci�n horizontal (eje Y)
        _mouseX += x * _mouseSensitivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);

        // Rotaci�n vertical (eje X) limitada
        _mouseY -= y * _mouseSensitivity * Time.deltaTime;
        _mouseY = Mathf.Clamp(_mouseY, -90f, 90f); // Limita la rotaci�n vertical

        if (_headTransform != null)
        {
            _headTransform.localRotation = Quaternion.Euler(_mouseY, 0f, 0f);
        }
    }
}


