using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public IInteractable interactable;
    [SerializeField] bool isFrustrated;
    [SerializeField] public bool isInteracting;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Interact();
        if (interactable != null)
        {
            if (Vector3.Distance(transform.position, interactable.transform.position) > interactable.Radius)
            {
                interactable = null;
            }
        }
    }

    void Interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (interactable != null)
            {
                if (interactable.CanInteract == true && !isInteracting)
                {
                    isInteracting = true;
                    Debug.Log("Interacting with " + interactable.name);
                    interactable.OnInteract();
                }
            }
        }
    }
}
