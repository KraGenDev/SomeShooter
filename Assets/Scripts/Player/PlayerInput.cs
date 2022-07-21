using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float axisX { get; private set; }
    public float axisZ { get; private set; }
    public float mouseX { get; private set; }
    public float mouseY { get; private set; }
    public bool jump { get; private set;}
    public bool run { get; private set; }
    
    public delegate void PlayerEvents();
    public static event PlayerEvents  Fire1;
    public static event PlayerEvents  Reload;
    public static event PlayerEvents  Torch;
    public static event PlayerEvents  Laser;
    
    void Update()
    {
        axisX = Input.GetAxis("Horizontal");
        axisZ = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        jump = Input.GetButtonDown("Jump");
        run = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetMouseButton(0) && !Cursor.visible)
        {
            Fire1?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Torch?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Laser?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
