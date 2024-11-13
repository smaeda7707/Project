using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;

    //public bool isFull; // Inventory is full
    public bool isOpen; // Inventory is open

    public int maxInventorySpace = 18;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;

        PopulateSlotList();

    }

    private void PopulateSlotList() // Adds each slot to the slotList
    {
        foreach (Transform child in inventoryScreenUI.transform) // Searches for transforms in the inventoryScreenUI transform (reffered to as a child)
        {
            if (child.CompareTag("Slot")) // If the child has the "Slot" tag, add it to slotList
            { 
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {

        if (Input.GetButtonDown("Inventory") && !isOpen)
        {

            Debug.Log("inventory opened");
            inventoryScreenUI.SetActive(true);
            isOpen = true;

        }
        else if (Input.GetButtonDown("Inventory") && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            isOpen = false;
        }

        // Checking if inventory is open so that mouse state can be changed accordingly
        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void AddToInventory(string itemName)
    {
        whatSlotToEquip = FindNextEmptySlot();

        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemList.Add(itemName);
        
    }
    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList) // Go through every slot in slotList
        {
            if(slot.transform.childCount == 0) // If the slot doesn't have a child
            {
                return slot;
            }
        }
        return new GameObject(); // Should never get to this line of code
    }
    public bool CheckIfFull()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return false;
            }
        }
        return true;
    }
    

}