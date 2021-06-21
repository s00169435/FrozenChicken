using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chore : IInteractable
{
    [SerializeField] protected int frustrationPenalty;
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
        CheckPlayerBeside();
    }

    public void DecreaseSatisfaction()
    {
        Debug.Log("satisfaction value: " + this.frustrationPenalty);
        gameManager.AdjustSatisfaction(-frustrationPenalty);
    }
}
