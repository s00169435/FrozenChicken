using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractable : MonoBehaviour
{
    [SerializeField] protected float radius = 3f;
    public float Radius { get { return radius; } }
    protected bool canInteract;
    public bool CanInteract { get { return canInteract; } }
    [SerializeField] protected Player player;

    private void Start()
    {
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    protected void Update()
    {
        Debug.Log("Hello");
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
}
