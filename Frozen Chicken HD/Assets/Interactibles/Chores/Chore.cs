using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chore : IInteractable
{
    [SerializeField] protected float choreDuration;
    [SerializeField] protected int satisfactionReq;
    [SerializeField] protected Coroutine CoroutChore;

    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        CheckBeside();
    }

    protected override void CheckBeside()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= radius)
            if (player.chore == null) 
                player.chore = this;
    }

    public void DecreaseSatisfaction()
    {
        Debug.Log("satisfaction value: " + this.satisfactionReq);
        gameManager.AdjustSatisfaction(-satisfactionReq);
    }

    protected bool CheckIfInteract()
    {
        if (gameManager.CurrentSatisfaction >= this.satisfactionReq)
            if (canInteract)
                return true;

        return false;
    }

    protected IEnumerator DoChore(string choreMessage)
    {
        this.DecreaseSatisfaction();
        this.canInteract = false;
        Debug.Log(choreMessage);

        float timeElapsed = 0f;

        while (timeElapsed < this.choreDuration)
        {
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        canInteract = true;
        Debug.Log(choreMessage + " finished.");
    }
}
