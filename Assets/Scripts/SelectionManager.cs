using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class SelectionManager : MonoBehaviour
{

    public static SelectionManager instance { get; private set; } // Make SelectionManager a singleton which allows other classes to access it (Also include Awake() Method)

    private void Awake() // USED IN MAKING A SINGLETON
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public bool cursorOnObject;

    public GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_text;

    private void Start()
    {
        cursorOnObject = false;
        interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
    }




    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // If an object is hit
        {
            var selectionTransform = hit.transform;
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>(); // Create variable interactable to shorten code lines

            if (interactable && interactable.playerInRange) // If an object is hit and InteractableObject script is found and player is in range
            {
                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
                cursorOnObject = true;
            }
            else // If an object is hit but no InteractableObject script is found
            {
                interaction_Info_UI.SetActive(false);
                cursorOnObject = false;
            }

        }
        else // If no object is hit at all
        {
            interaction_Info_UI.SetActive(false);
            cursorOnObject= false;
        }
    }
}
