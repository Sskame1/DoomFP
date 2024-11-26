using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float _moveSpeed = 5f;
    private float _groundCheckRadius = 0.2f;
    private float _mouseSens = 2f;
    private float _yRotation = 0f;

    public Transform cameraTransform;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform PlayerBody;

    private bool _isGrounded;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        RotationCharacter();

        Vector3 dir = GetInputDirection();

        if (dir.magnitude >= 0.1f)
        {
            MoveCharacter(dir);
        }
    }

    private Vector3 GetInputDirection()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = (forward * ver + right * hor).normalized;
        return direction;
    }
    
    private void MoveCharacter (Vector3 direction)
    {
        transform.position += direction * _moveSpeed * Time.deltaTime;
    }

    private void RotationCharacter()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSens;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        _yRotation += mouseX;
        //yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(0f, _yRotation, 0f);

        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
