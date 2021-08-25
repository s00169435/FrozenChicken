using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : Chore
{
    public override void OnInteract()
    {
        if (gameManager.CurrentSatisfaction >= this.satisfactionReq)
        {
            this.canInteract = false;

            if (CoroutChore != null)
                StopCoroutine(CoroutChore);

            CoroutChore = StartCoroutine(TakeOutChicken());
        }
        else
        {
            Debug.Log("Not enough satisfaction.");
        }
    }

    IEnumerator TakeOutChicken()
    {
        this.DecreaseSatisfaction();

        Debug.Log("Taking out chicken");
        player.playerMovement.CanMove = false;
        yield return new WaitForSeconds(choreDuration);
        player.playerMovement.CanMove = true;

        Debug.Log("Chore ended");
    }
}