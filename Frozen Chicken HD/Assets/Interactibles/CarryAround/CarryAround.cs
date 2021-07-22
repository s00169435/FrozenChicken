using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryAround : IInteractable
{
    // Start is called before the first frame update
    protected bool isHeld;
    [SerializeField] Transform Closet;
    public bool IsHeld { get { return isHeld; } }
    new void Start()
    {
        base.Start();
        Setup();
    }

    // Update is called once per frame
    new void Update()
    {
        CheckPlayerBeside();
    }

    public override void OnInteract()
    {
        Pickup();
    }

    public void Pickup()
    {
        if (isHeld == false)
        {
            isHeld = true;
            transform.position = player.playerArm.transform.position;
            transform.parent = player.playerArm.transform;
            Debug.Log("Carrying Broom");
        }
        else
        {
            isHeld = false;
            transform.parent = Closet;
            transform.position = Closet.position;
            Debug.Log("Dropping Broom");
        }

        player.HasBroom = isHeld;
    }

    public void Setup()
    {
        transform.parent = Closet;
        transform.position = Closet.position;
        canInteract = true;
        isHeld = false;
    }
}
