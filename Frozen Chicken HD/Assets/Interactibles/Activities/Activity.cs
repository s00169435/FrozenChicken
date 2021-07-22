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
        CheckPlayerBeside();
    }

    public void IncreaseSatisfaction()
    {
        Debug.Log("satisfaction value: " + this.satisfactionReward);
        gameManager.AdjustSatisfaction(this.satisfactionReward);
    }

    protected IEnumerator StartCooldown()
    {
        float startPoint = 0f;

        while (startPoint < cooldownTime)
        {
            startPoint += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        this.canInteract = true;
        Debug.Log($"Can interact with {this.name} again");
    } 
}
