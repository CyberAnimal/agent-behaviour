using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    private Vector3 _position;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _position = new Vector3(-8.0f, 3.5f, 0f);
    }

    private void LateUpdate()
    {
        transform.position = Player.transform.position + _position;
    }

    private void ChangePositionToRight()
    {
        _position = new Vector3(_position.z, _position.y, -_position.x);
    }

    private void ChangePositionToLeft()
    {
        _position = new Vector3(_position.z, _position.y, _position.x);
    }

    private void OnEnable()
    {
        Game.Player.Collision.Changed += Rotate;
    }

    private void OnDisable()
    {
        Game.Player.Collision.Changed -= Rotate;
    }

    private void Rotate(int directionAngle)
    {
        float targetAngle = 90.0f;

            ChangePositionToLeft();
            Quaternion startRotation = _camera.transform.rotation;
            _camera.transform.rotation = new Quaternion(startRotation.x, startRotation.y - targetAngle, startRotation.z, startRotation.w);


        if (directionAngle > 0)
        {
            ChangePositionToRight();
            _camera.transform.rotation = new Quaternion(startRotation.x, startRotation.y + targetAngle, startRotation.z, startRotation.w);
        }

        StartCoroutine(SmoothRotate(directionAngle));

        Debug.Log("Камера повернута!");
    }

    private IEnumerator SmoothRotate(int directionAngle)
    {
        float targetAngle = 90.0f;
        float timeCount = 0.0f;

        if (directionAngle < 0)
        {
            Quaternion startRotation = _camera.transform.rotation;
            Quaternion endRotation = new Quaternion(startRotation.x, startRotation.y - targetAngle, startRotation.z, startRotation.w);

            ChangePositionToLeft();

            while (timeCount < 1)
            {
                timeCount += Time.deltaTime;

                _camera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, timeCount);

                yield return new WaitForSeconds(.05f);
            }
        }

        else
        {
            Quaternion startRotation = _camera.transform.rotation;
            Quaternion endRotation = new Quaternion(startRotation.x, startRotation.y + targetAngle, startRotation.z, startRotation.w);

            ChangePositionToRight();

            while (timeCount < 1)
            {
                timeCount += Time.deltaTime;

                _camera.transform.rotation = Quaternion.Slerp(startRotation, endRotation, timeCount);

                yield return null;
            }
        }
    }
}