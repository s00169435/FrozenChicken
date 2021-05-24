using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    new void Update()
    {
        base.Update();
        canInteract = CheckPlayerBeside();
    }

    public override void OnInteract()
    {
        base.OnInteract();
    }
}
