using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryInput : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("FIRE");
                var obj = FindObjectOfType<ArtileryController>();
                obj.Shot();
            }
        }
    }
}
