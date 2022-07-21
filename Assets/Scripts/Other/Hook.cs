using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private float hookSpeed;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                gameObject.transform.DOMove(hit.point, hookSpeed);
            }
        }
    }
}
