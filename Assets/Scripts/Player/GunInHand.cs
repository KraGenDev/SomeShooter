using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInHand : MonoBehaviour
{
    public List<GameObject> guns;
    [Range(0,5)]public int idGunInHand = 0;

    private void Start()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetActive(false);
        }
        guns[idGunInHand].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") !=  0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (idGunInHand < guns.Count - 1)
                {
                    idGunInHand++;
                }
            }
            else
            {
                idGunInHand--;
                idGunInHand = Mathf.Clamp(idGunInHand, 0, guns.Count-1);
            }
            
            for (int i = 0; i < guns.Count; i++)
            {
                guns[i].SetActive(false);
            }
            guns[idGunInHand].SetActive(true);
        }
    }
}
