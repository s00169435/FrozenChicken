using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Activity
{
    [SerializeField] float sleepTime;

    public override void OnInteract()
    {
        StartCoroutine(Sleep());
    }
    IEnumerator Sleep()
    {
        Vector3 playerInitialPos = player.transform.position;
        Quaternion playerInitialRot = player.transform.rotation;
        Transform body = player.PlayerBody.transform;

        Debug.Log("Going to sleep");
        canInteract = false;
        player.playerMovement.CanMove = false;
        body.position = transform.position + new Vector3(0, 1, 0);
        body.rotation = Quaternion.Euler(new Vector3(-90, 90, 0));

        yield return new WaitForSeconds(sleepTime);

        Debug.Log("Mmmmm well rested");
        body.position = playerInitialPos;
        body.rotation = playerInitialRot;
        canInteract = true;
        player.playerMovement.CanMove = true;

    }
}
