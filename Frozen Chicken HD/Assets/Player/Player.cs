using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement
        ;
    public IInteractable Interactable;
    public Obstacle Obstacle;
    [SerializeField] public bool isInteracting;
    [SerializeField] private GameObject PlayerArm;
    bool hasBroom;
    public bool HasBroom { get { return hasBroom; }  set { hasBroom = value; } }
    public GameObject playerArm { get => PlayerArm; }

    private void Start()
    {
        this.playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        CheckInteractableRange();
        CheckObstacleRange();
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
        else if (Input.GetButtonDown("Use"))
        {
            CleanUp();
        }
    }

    private void CheckInteractableRange()
    {
        if (Interactable != null)
            if (Vector3.Distance(transform.position, Interactable.transform.position) > Interactable.Radius)
                Interactable = null;
    }

    private void CheckObstacleRange()
    {
        if (Obstacle != null)
            if (Vector3.Distance(transform.position, Obstacle.transform.position) > Obstacle.Radius)
                Obstacle = null;
    }

    void Interact()
    {
        if (Interactable != null)
            if (Interactable.CanInteract == true)
            {
                Debug.Log("Interacting with " + Interactable.name);
                Interactable.OnInteract();
            }
    }

    void CleanUp()
    {
        if (hasBroom && this.Obstacle != null)
        {
            Debug.Log("Cleaning poop");
            this.Obstacle.CleanUp();
        }
        else if (!hasBroom)
        {
            Debug.Log("Don't have broom");
        }
        else if (this.Obstacle == null)
        {
            Debug.Log("Nothing to cleanup");
        }
    }
}
