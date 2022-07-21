using System;
using UnityEngine;

public class LaserControll : MonoBehaviour
{
    private bool active = false;
    private LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }


    void Update()
    {
        RaycastHit hit;
        line.SetPosition(0,gameObject.transform.position);
        if (active)
        {
            if(Physics.Raycast(transform.position, transform.forward, out hit)) {
                line.enabled = true;
            }
            else
            {
                line.enabled = false;
            }
            line.SetPosition(1 , hit.point);
        }
    }

    private void OnEnable()
    {
        PlayerInput.Laser += LaserActivity;
    }
    private void OnDisable()
    {
        PlayerInput.Laser -= LaserActivity;
    }
    private void LaserActivity()
    {
        active = !active;
        line.enabled = active;
    }
}
