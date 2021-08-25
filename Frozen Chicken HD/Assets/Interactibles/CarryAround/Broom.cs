using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : CarryAround
{
    [SerializeField] float cleaningInterval;
    [SerializeField] bool isPressed;
    public override void OnUse()
    {
        if (player.obstacle != null && player.obstacle.GetType() == typeof(Dust))
        {
            isPressed = true;
            Debug.Log("Cleaning");
            Dust dust = player.obstacle as Dust;
            StartCoroutine(Clean(dust));
        }
        else
        {
            Debug.Log("No dust to clean");
        }
    }

    IEnumerator Clean(Dust dust)
    {
        float timeElapsed = 0;

        while (isPressed)
        {
            player.playerMovement.CanMove = false;
            timeElapsed += Time.deltaTime;

            if (timeElapsed > cleaningInterval)
            {
                RemoveDust(dust);
                timeElapsed = 0;
            }

            isPressed = Input.GetButton("Use");

            yield return new WaitForEndOfFrame();
        }

        player.playerMovement.CanMove = true;
        Debug.Log("Stopping cleaning");
    }

    void RemoveDust(Dust dust)
    {
        if (dust.transform.childCount != 0)
        {
            Debug.Log("Cleaning dust");
            int dustParticle = Random.Range(0, dust.transform.childCount);
            Destroy(dust.transform.GetChild(dustParticle).gameObject);

            if (dust.transform.childCount == 1)
                Destroy(dust.gameObject);
        }
    }
}
