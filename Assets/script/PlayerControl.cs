using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float _jumpForce = 5f;
    private float _moveSpeed = 5f;
    private float _groundCheckRadius = 0.2f;
    private float _mouseSens = 2f;
    private float _yRotation = 0f;
    private SystemInventory _inventory;
    private float _dashSpeed = 5f;
    public float _dashDuration = 0.2f;
    private float _timeSinceLastPress = 0f;
    private float _GroundJumpValue = 0f;


    public Transform cameraTransform;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform PlayerBody;

    private Rigidbody _rb;

    private bool _isGrounded;
    private bool _isDashing;
    private bool _isKeyPressed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();
        _inventory = GetComponent<SystemInventory>();
    }

    void Update()
    {
        CheckedCharacterOnGround();
        RotationCharacter();

        Vector3 dir = GetInputDirection();

        if (dir.magnitude >= 0.1f)
        {
            MoveCharacter(dir);
        }

        if (Input.GetKey(KeyCode.W) && !_isGrounded)
        {
            _isKeyPressed = true;
            _dashSpeed += 0.1f * Time.deltaTime;
        }
        else if (_isKeyPressed) 
        {
            _timeSinceLastPress += Time.deltaTime;
            if (_timeSinceLastPress >= 1f)
            {
                _dashSpeed = 2f;
                _isKeyPressed = false;
            }
        }

        if (_isDashing)
        {
            Dash(dir);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isDashing)
        {
            _isDashing = true;
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

    private void CheckedCharacterOnGround()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, _groundCheckRadius, groundLayer);

        if (_isGrounded )
        {
            _GroundJumpValue = 1f;
        }

        if (_GroundJumpValue == 1 && Input.GetButtonDown("Jump"))
        {
            _GroundJumpValue = 0;
            Jump();
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void PickUpItem(int itemID)
    {
        _inventory.AddItem(itemID);
    }

    private void Dash(Vector3 direction)
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + direction.normalized * _moveSpeed * _dashSpeed, Time.deltaTime / _dashDuration);
    }

}
