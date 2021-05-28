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

        while (time < playTime)
        {
            time += Time.deltaTime;
            playerMovement.CanMove = false;
            yield return new WaitForEndOfFrame();
        }

        this.AddSatisfaction();
        playerMovement.CanMove = true;
    }
}
