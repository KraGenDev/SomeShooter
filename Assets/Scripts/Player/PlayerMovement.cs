using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6.0F;
    [SerializeField] private float runSpeed = 12.0F;
    [SerializeField] private float jumpSpeed = 8.0F;
    [SerializeField] private float gravityForce = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private PlayerInput _playerInput;
    private CharacterController controller;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
    }

    void Update() {
        Move(); ;
    }

    private void Move()
    {
        if (controller.isGrounded) {
            moveDirection = new Vector3(_playerInput.axisX, 0, _playerInput.axisZ);
            moveDirection = transform.TransformDirection(moveDirection);
            if (_playerInput.run)
            {
                moveDirection *= runSpeed;
            }
            else
            {
                moveDirection *= speed;
            }

            if (_playerInput.jump)
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravityForce * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    
}
