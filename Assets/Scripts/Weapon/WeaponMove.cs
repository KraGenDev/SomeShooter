using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponMove : MonoBehaviour
{
    [SerializeField] private Camera _Camera;


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; 
        Ray ray = _Camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        if (hit.collider != null)
        {
            gameObject.transform.DOLookAt(hit.point,1);
        }
    }
}
