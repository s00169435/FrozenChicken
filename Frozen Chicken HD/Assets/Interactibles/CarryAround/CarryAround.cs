using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryAround : IInteractable
{
    // Start is called before the first frame update
    [SerializeField] protected bool isHeld;
    [SerializeField] Transform InitialLocation;
    public bool IsHeld { get { return isHeld; } }
    new void Start()
    {
        base.Start();
        Setup();
    }

    // Update is called once per frame
    new void Update()
    {
        CheckBeside();
    }

    public override void OnInteract()
    {
        Pickup();
    }

    protected override void CheckBeside()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= radius)
        {
            if (player.carryAround == null)
            {
                player.carryAround = this;
            }
        }
    }


    public void Pickup()
    {
        if (isHeld == false)
        {
            isHeld = true;
            transform.position = player.playerArm.transform.position;
            transform.parent = player.playerArm.transform;
        }
        else
        {
            isHeld = false;
            transform.parent = InitialLocation;
            transform.position = InitialLocation.position;
        }
    }

    public void Setup()
    {
        transform.parent = InitialLocation;
        transform.position = InitialLocation.position;
        canInteract = true;
        isHeld = false;
        Debug.Log("Setting up broom");
    }

    public virtual void  OnUse()
    {

    }
}
