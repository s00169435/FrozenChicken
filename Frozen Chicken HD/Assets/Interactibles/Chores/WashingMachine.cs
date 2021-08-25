using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachine : Chore
{
    public override void OnInteract()
    {
        if (player.carryAround.GetType() == typeof(WashBasket))
        {
            Debug.Log(player.carryAround.GetType());
            if (CheckIfInteract())
            {
                if (CoroutChore != null) 
                StopCoroutine(CoroutChore);
                
                CoroutChore = StartCoroutine(DoChore("Washing clothes"));
            }
            else
            {
                Debug.Log("Can't wash clothes");
            }
        }
        else
        {
            Debug.Log("Don't have basket.");
        }
    }

    IEnumerator WashClothes()
    {
        this.DecreaseSatisfaction();
        canInteract = false;
        Debug.Log("Washing clothes");
        float timeElapsed = 0f;

        while (timeElapsed < this.choreDuration)
        {
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        canInteract = true;
        Debug.Log("Clothes finished");
    }
}
