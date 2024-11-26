using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public int itemID; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player")) 
        {
            PlayerControl player = other.GetComponent<PlayerControl>();
            if (player != null)
            {
                player.PickUpItem(itemID); 
                Destroy(gameObject); 
            }
        }
    }
}
