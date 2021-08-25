using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public IInteractable interactable;
    public CarryAround carryAround;
    public Activity activity;
    public Chore chore;
    public Obstacle obstacle;

    [SerializeField] public bool isInteracting;
    [SerializeField] private GameObject PlayerArm;
    [SerializeField] public GameObject PlayerBody;
    public GameObject playerArm { get => PlayerArm; }

    private void Start()
    {
        this.playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        // CheckInteractableRange();
        CheckRange();
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
        else if (Input.GetButtonDown("Use"))
        {
            UseItem();
        }
    }

    private void CheckRange()
    {
        CheckChoreRange();
        CheckActivityRange();
        CheckObstacleRange();
        CheckActivityRange();
        CheckCarryAroundRange();
    }

    private void CheckObstacleRange()
    {
        if (obstacle != null)
            if (Vector3.Distance(transform.position, obstacle.transform.position) > obstacle.Radius)
                obstacle = null;
    }

    private void CheckActivityRange()
    {
        if (activity != null)
            if (Vector3.Distance(transform.position, activity.transform.position) > activity.Radius)
                activity = null;
    }

    private void CheckCarryAroundRange()
    {
        if (carryAround != null)
            if (Vector3.Distance(transform.position, carryAround.transform.position) > carryAround.Radius)
                carryAround = null;
    }

    private void CheckChoreRange()
    {
        if (chore != null)
            if (Vector3.Distance(transform.position, chore.transform.position) > chore.Radius)
                chore = null;
    }

    void Interact()
    {
        if (activity != null)
            activity.OnInteract();
        else if (chore != null)
            chore.OnInteract();
        else if (carryAround != null)
            carryAround.OnInteract(); 
    }

    void UseItem()
    {
        if (this.carryAround != null)
        {
            Debug.Log("Using item");
            carryAround.OnUse();
        }
    }
}
