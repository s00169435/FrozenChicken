using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : IInteractable
{
    [SerializeField] float playTime;
    Coroutine CoroutPlay;
    // Start is called before the first frame update
    void Start()
    {

    }

    new void Update()
    {
        canInteract = CheckPlayerBeside();
    }

    public override void OnInteract()
    {
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

        this.AddSatisfaction();
        player.isInteracting = false;
        playerMovement.CanMove = true;
    }
}
