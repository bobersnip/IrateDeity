using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character c = collision.GetComponent<Character>();
        if(c != null)
        {
            GetComponent<IPickupObject>().OnPickup(c);
            Destroy(gameObject);
        }
    }
}
