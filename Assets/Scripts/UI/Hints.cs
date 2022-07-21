using UnityEngine;

public class Hints : MonoBehaviour
{
     [SerializeField] private GameObject hint_pressE;

     private void OnTriggerEnter(Collider other)
     {
          hint_pressE.SetActive(true);
     }

     private void OnTriggerExit(Collider other)
     {
          hint_pressE.SetActive(false);
     }
}
