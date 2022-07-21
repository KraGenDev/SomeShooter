using System;
using UnityEngine;

public class TorchControll : MonoBehaviour
{
    [SerializeField] private Light light;
    private void OnEnable()
    {
        PlayerInput.Torch +=  TorchActivity;
    }
    private void OnDisable()
    {
        PlayerInput.Torch -= TorchActivity;
    }
    private void TorchActivity()
    {
        light.enabled = !light.enabled;
    }
}
