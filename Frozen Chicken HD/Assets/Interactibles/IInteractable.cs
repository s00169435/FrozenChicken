using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractable : MonoBehaviour
{
    [SerializeField] protected float radius = 3f;
    public float Radius { get { return radius; } }
    [SerializeField] protected bool canInteract;
    public bool CanInteract { get { return canInteract; } }
    [SerializeField] protected Player player; 
    [SerializeField] protected PlayerMovement playerMovement;
    [SerializeField] protected GameManager gameManager;

    protected void Start()
    {
        this.SetUpConnections();
    }

    protected void SetUpConnections()
    {
        // Debug.Log("Setting up interactables");
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    protected void Update()
    {

    }

    public void CheckPlayerBeside()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= radius)
        {
            player.interactable = this;
        }
    }

    public virtual void OnInteract()
    {

    }
    
}
