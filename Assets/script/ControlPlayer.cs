using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    
    private float _moveSpeed = 5f;
    private float _Run = 1f;
    private bool _IsRun;
    private float _speedRotCamer = 2f;
    private float _cameraPitch = 0f;
    private bool _IsGrounded; 
    private float _groundCheckerRadius = 0.1f;
    private float _jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform groundChecker;
    private Rigidbody _rb;
    [SerializeField]
    private Camera _PlayerCamera;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

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
        _rb.MovePosition(_rb.position + move * _moveSpeed * _Run * Time.deltaTime);
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
        Run();
    }

    private void HandleInput() 
    {
        if (Input.GetButtonDown("Jump")) 
        {
           Jump();
             
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
           _IsRun = true;
        } 
        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            _IsRun = false;
        }
    }

    private void Run() 
    {
        if (_IsRun)
        {
            _Run = 2f;
        } else 
        {
            _Run = 1f;
        }
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
}
