using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Header("Distance")]
    [SerializeField] private float distance = 6f;
    [SerializeField] private float height = 2f;

    [Header("Rotation")]
    [SerializeField] private float sensitivity = 120f;
    [SerializeField] private float smooth = 10f;

    private float _yaw;
    private float _pitch = 20f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // ввод
        _yaw += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        _pitch -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        _pitch = Mathf.Clamp(_pitch, 10f, 60f);

        // позиция
        Quaternion rot = Quaternion.Euler(_pitch, _yaw, 0);
        Vector3 dir = rot * Vector3.back;

        Vector3 desiredPos = target.position + Vector3.up * height + dir * distance;

        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * smooth);
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}