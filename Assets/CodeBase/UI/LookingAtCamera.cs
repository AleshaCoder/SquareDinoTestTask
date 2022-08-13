using UnityEngine;

public class LookingAtCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;

    private bool _isLooking = true;

    private Vector3 CameraPosition => _camera.transform.position;

    public bool IsLooking => _isLooking;

    public void SwitchOff() => _isLooking = false;
    public void SwitchOn() => _isLooking = true;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;
    }

    private void LateUpdate()
    {
        if (_isLooking == false)
            return;

        Vector3 point = new Vector3(transform.position.x, CameraPosition.y, CameraPosition.z);
        transform.LookAt(point);
    }
}
