using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().AddCarrot();
            Destroy(gameObject);
        }
    }
}

