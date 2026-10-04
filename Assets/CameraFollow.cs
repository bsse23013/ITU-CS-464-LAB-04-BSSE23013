using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 3, -5);
    [SerializeField] float smoothSpeed = 8f;

    void LateUpdate()
    {
        Vector3 wanted = target.position + target.rotation * offset;
        transform.position = Vector3.Lerp(transform.position, wanted, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}