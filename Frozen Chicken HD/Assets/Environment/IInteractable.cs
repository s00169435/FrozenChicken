using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractable : MonoBehaviour
{
    [SerializeField] public float radius = 3f;
    [SerializeField] Player player;
    bool canInteract;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Update()
    {
        CheckPlayerBeside();
    }

    public bool CheckPlayerBeside()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= radius)
        {
            player.interactable = this;
            return true;
        }

        return false;
    }
}
