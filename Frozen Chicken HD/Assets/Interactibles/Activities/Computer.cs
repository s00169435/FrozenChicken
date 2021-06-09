using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Activity
{
    [SerializeField] float playTime;
    Coroutine CoroutPlay;
    // Start is called before the first frame update

    /* new void Update()
    {
        CheckPlayerBeside();
    } */

    public override void OnInteract()
    {
        this.canInteract = false;
        if (CoroutPlay != null)
            StopCoroutine(CoroutPlay);

        CoroutPlay = StartCoroutine(PlayGame());
    }

    IEnumerator PlayGame()
    {
        Debug.Log("Playing game.");
        float time = 0f;
        playerMovement.CanMove = false;

        while (time < playTime)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        this.IncreaseSatisfaction();
        Debug.Log(this.canInteract);
        //player.isInteracting = false;
        playerMovement.CanMove = true;

        StartCoroutine(this.StartCooldown());
    }
}
