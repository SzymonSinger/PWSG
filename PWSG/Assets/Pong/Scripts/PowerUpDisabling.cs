using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDisabling : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            gameObject.SetActive(false);
        }
    }

}
