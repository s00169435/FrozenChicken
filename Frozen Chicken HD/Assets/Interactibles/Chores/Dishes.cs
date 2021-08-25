using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dishes : Chore
{
    [SerializeField] int amountToWash;
    [SerializeField] float timeToWash;
    public override void OnInteract()
    {
        CoroutChore = StartCoroutine(CoroutWash());
    }

    void WashDishes()
    {

    }

    IEnumerator CoroutWash()
    {
        this.canInteract = false;
        for (int i = 0; i < amountToWash; i++)
        {
            yield return new WaitForSeconds(timeToWash);
            Debug.Log($"Dish {i + 1} washed");
        }

        this.DecreaseSatisfaction();
    }
}
