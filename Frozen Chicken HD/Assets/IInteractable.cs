using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractable : MonoBehaviour
{
    [SerializeField] protected float radius = 3f;
    public float Radius { get { return radius; } }
    protected bool canInteract;
    public bool CanInteract { get { return canInteract; } }
    [SerializeField] int satisfactionReward;
    [SerializeField] protected Player player; 
    [SerializeField] protected PlayerMovement playerMovement;
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    protected void Update()
    {

    }

    public bool CheckPlayerBeside()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= radius)
        {
            player.interactable = this;
            return true;
        }

        return false;
    }

    public virtual void OnInteract()
    {
        Debug.Log("Testing interaction.");
    }
    
    public void AddSatisfaction()
    {
        gameManager.AdjustSatisfaction(satisfactionReward);
    }
}
