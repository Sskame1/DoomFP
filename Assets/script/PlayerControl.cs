using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float moveSpeed = 5f;
    public Transform cameraTransform;

    void Update()
    {
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
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
