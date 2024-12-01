using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBlock : MonoBehaviour
{
    public int itemID;
    public float interactionRange = 3f;
    private PlayerControl playerControl;

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && IsPlayerInRange())
        {
            Interact();
        }
    }

    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(playerControl.transform.position, transform.position);
        return distance <= interactionRange;
    }

    private void Interact()
    {
        playerControl.PickUpItem(itemID);
    }
}
