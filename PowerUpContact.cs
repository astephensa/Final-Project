using UnityEngine;
using System.Collections;

public class PowerUpContact : MonoBehaviour
{


    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            if (gameObject.name == "PowerUp(Clone)")
            { 
                
                other.GetComponent<PlayerController>().powerUp = 300;

               
            }

            else if (gameObject.name == "PowerUp2(Clone)")
            {
                other.GetComponent<PlayerController>().shield = 300;
            }
                Destroy(gameObject);
        }

        
    }
}