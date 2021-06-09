using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float speedFactor = 0.5f;
    [SerializeField] float cooldown = 4f;
    [SerializeField] int frustationValue;
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Player")
       {
            Hinder(other);
       }
    }

    protected virtual void Hinder(Collider other)
    {
        Debug.Log("Hit player");
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        gameManager.AdjustSatisfaction(-frustationValue);
        StartCoroutine(player.SetSpeed(speedFactor, cooldown));
    }
}
