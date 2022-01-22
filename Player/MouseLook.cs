using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    private RotationAxes _axes = RotationAxes.MouseXAndY;
    private float _sensitivityHor = 9.0f;
    private float _sensitivityVert = 9.0f;

    private float _minimumVert = -45.0f;
    private float _maximumVert = 45.0f;

    private float _rotationX = 0;

    private void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
            rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        if (_axes == RotationAxes.MouseX)
            transform.Rotate(0, Input.GetAxis("Mouse X") * _sensitivityHor, 0);

        else if (_axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, _minimumVert, _maximumVert);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }

        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, _minimumVert, _maximumVert);
            float delta = Input.GetAxis("Mouse X") * _sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}