using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float speedFactor = 0.5f;
    [SerializeField] float cooldown = 4f;
    [SerializeField] int frustationValue;
    [SerializeField] GameManager gameManager;
    [SerializeField] DogPooper dogPooper;
    [SerializeField] protected Player player;
    [SerializeField] protected float radius = 3f;
    public float Radius { get { return radius; } }

    // Start is called before the first frame update
    protected void Start()
    {
        dogPooper = FindObjectOfType<DogPooper>();
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerBeside();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Player")
       {
            if (!player.playerMovement.IsFrustrated)
                Hinder(other);
       }
    }

    protected virtual void Hinder(Collider other)
    {
        Debug.Log("Hit player");
        gameManager.AdjustSatisfaction(-frustationValue);
        StartCoroutine(player.playerMovement.SetSpeed(speedFactor, cooldown));
    }

    public void CheckPlayerBeside()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= radius)
        {
            if (player.obstacle == null)
            {
                player.obstacle = this;
            }
        }
    }
    
    public void CleanUp()
    {
        Debug.Log("Cleaing up poop");
        dogPooper.PoopList.Remove(this);
        Destroy(this.gameObject);
    }
}
