using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;          // drag your Player here
    public Vector3 offset = new Vector3(0f, 8f, -6f);  // above and behind
    public float smoothSpeed = 8f;
    public float rotateSpeed = 100f;

    private float currentAngle = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        // Optional: rotate camera around player with Q/E keys
        if (Input.GetKey(KeyCode.Q)) currentAngle -= rotateSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) currentAngle += rotateSpeed * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}