using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int maxSatisfaction;
    int currentSatisfaction;
    public int CurrentSatisfaction { get { return currentSatisfaction; } }
    public IInteractable interactable;
    // Start is called before the first frame update
    void Awake()
    {
        currentSatisfaction = maxSatisfaction;
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
                if (interactable.CanInteract)
                {
                    Debug.Log(interactable.gameObject.name);
                }
            }
        }
    }
}
