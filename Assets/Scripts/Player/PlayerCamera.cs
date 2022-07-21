using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    private PlayerInput _input;

    private float X, Y;

    private void Start()
    {
        _input = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        CameraRotate();
    }

    private void CameraRotate()
    {
        if (!Cursor.visible)
        {
            var player = transform.parent.gameObject;
            X = transform.localEulerAngles.y + _input.mouseX * rotateSpeed;
            Y += _input.mouseY * rotateSpeed;
            Y = Mathf.Clamp (Y, minAngle, maxAngle);
            transform.localEulerAngles = new Vector3(-Y, transform.localEulerAngles.y, 0);
            player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x, player.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotateSpeed, player.transform.localEulerAngles.z);
        }
    }
}
