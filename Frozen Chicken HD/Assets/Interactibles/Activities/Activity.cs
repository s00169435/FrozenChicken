using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : IInteractable
{
    [SerializeField] int satisfactionReward;
    [SerializeField] float cooldownTime;
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        CheckBeside();
    }

    protected override void CheckBeside()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= radius)
        {
            if (player.activity == null)
            {
                player.activity = this;
            }
        }
    }
    public void IncreaseSatisfaction()
    {
        Debug.Log("satisfaction value: " + this.satisfactionReward);
        gameManager.AdjustSatisfaction(this.satisfactionReward);
    }

    protected IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);

        this.canInteract = true;
        Debug.Log($"Can interact with {this.name} again");
    } 
}
