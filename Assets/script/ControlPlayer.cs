using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    
    private float _moveSpeed = 5f;
    private float _speedRotCamer = 2f;
    private float _cameraPitch = 0f;
    private bool _IsGrounded; 
    private float _groundCheckerRadius = 0.1f;
    private float _jumpForce = 5f;
    private float _DashForce = 10;
    private float _dashCooldown = 1f;
    private bool _CanDash = true;
    public LayerMask groundLayer;
    public Transform groundChecker;
    private Rigidbody _rb;
    [SerializeField]
    private Camera _PlayerCamera;
    private RunOnWall _RunOnWall;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _RunOnWall = GetComponent<RunOnWall>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {        
        MovePlayer();
        RotatePlayer();
        Events();
        HandleInput();
    }

    private void MovePlayer() 
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.Normalize();
        _rb.MovePosition(_rb.position + move * _moveSpeed * Time.deltaTime);
    }

    private void RotatePlayer() 
    {
        float minPitch = -60f;
        float maxPitch = 60f;

        float mouseX = Input.GetAxis("Mouse X") * _speedRotCamer;
        float mouseY = Input.GetAxis("Mouse Y") * _speedRotCamer;

        transform.Rotate(0, mouseX, 0);

        _cameraPitch -= mouseY;

        _cameraPitch = Mathf.Clamp(_cameraPitch, minPitch, maxPitch);

        _PlayerCamera.transform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);
    }

    private void Events() {
        _IsGrounded = IsGrounded();
        RunOnWall();
        
    }

    private void HandleInput() 
    {
        if (Input.GetButtonDown("Jump")) 
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _CanDash) 
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash() 
    {
        _CanDash = false;

        Vector3 DashDirect = _PlayerCamera.transform.forward;
        DashDirect.y = 0;

        DashDirect.Normalize();

        _rb.AddForce(DashDirect * _DashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(_dashCooldown);

        _CanDash = true;
    }

    private void Jump() 
    {
        if (_IsGrounded) 
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded() 
    {
       return Physics.CheckSphere(groundChecker.position, _groundCheckerRadius, groundLayer);
    }

    private void RunOnWall() 
    {
        float tiltAngle = 15f;
        float tiltSpeed = 5f;

        if (_RunOnWall.runWall) 
        {
            if(_RunOnWall.IsLeftRun) 
            {
                Vector3 targetRotation = new Vector3(0, 0, tiltAngle);
                _PlayerCamera.transform.localRotation = Quaternion.Slerp(
                _PlayerCamera.transform.localRotation,
                Quaternion.Euler(targetRotation),
                Time.deltaTime * tiltSpeed
                );    
            } else 
            {
                Vector3 targetRotation = new Vector3(0, 0, -tiltAngle);
                _PlayerCamera.transform.localRotation = Quaternion.Slerp(
                _PlayerCamera.transform.localRotation,
                Quaternion.Euler(targetRotation),
                Time.deltaTime * tiltSpeed
                );
            }
        } else 
        {
            _PlayerCamera.transform.localRotation = Quaternion.Slerp(
            _PlayerCamera.transform.localRotation,
            Quaternion.Euler(Vector3.zero),
            Time.deltaTime * tiltSpeed
            );
        }
    }
    
}
