using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;

    public string itemName;

    public string GetItemName()
    {
        return itemName;
    }

    void Update()
    {
        if(Input.GetButtonDown("Pickup") && playerInRange && SelectionManager.instance.cursorOnObject) // Pick up item if item is clicked and player is in range
        {
            if (InventorySystem.Instance.CheckIfFull())
            {
                Debug.Log("Inventory is full");
            }
            else
            {
                InventorySystem.Instance.AddToInventory(itemName);

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}