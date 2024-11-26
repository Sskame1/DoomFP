using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float moveSpeed = 5f;
    public Transform cameraTransform;

    void Update()
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

        if (direction.magnitude >= 0.1f)
        {

            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
