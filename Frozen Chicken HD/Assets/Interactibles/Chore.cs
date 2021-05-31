using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chore : IInteractable
{
    [SerializeField] int frustrationValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    new void Update()
    {
        canInteract = CheckPlayerBeside();
    }

    public override void OnInteract()
    {
        
    }
}
