using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : Chore
{
    // Start is called before the first frame update

    // Update is called once per frame
    /* new void Update()
    {
        CheckPlayerBeside();
    } */

    public override void OnInteract()
    {
        if (gameManager.CurrentSatisfaction <= this.satisfactionReq)
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
        float timeElapsed = 0f;

        while (timeElapsed < this.choreDuration)
        {
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Chore ended");
    }
}