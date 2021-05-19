using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float baseSpeed = 5f;
    [SerializeField] float currentSpeed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public CharacterController controller;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    [SerializeField] Pathfinder pathFinder;
    [SerializeField] GridManager gridManager;
    [SerializeField] List<Node> path = new List<Node>();
    [SerializeField] bool isMoving;

    private void Awake()
    {
        pathFinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
        currentSpeed = baseSpeed;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            path.Clear();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 3000f, groundMask))
            {
                Vector2Int startsCoords = gridManager.GetCoordinatesFromPosition(transform.position);
                Vector2Int tileCoords = gridManager.GetCoordinatesFromPosition(hit.collider.transform.position);
                pathFinder.SetNodes(startsCoords, tileCoords);
                pathFinder.StartCoords = startsCoords;
                pathFinder.DestinationCoords = tileCoords;
                path = pathFinder.GetNewPath(startsCoords);

                Tile tile = hit.transform.parent.GetComponent<Tile>();

                if (tile && path.Count > 1)
                {
                    StartCoroutine(FollowPath());
                }
            }
        }
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {

            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            endPosition = new Vector3(endPosition.x, transform.position.y, endPosition.z);

            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * currentSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

    }

    public IEnumerator SetSpeed(float speedFactor, float cooldown)
    {
        Debug.Log("Stopping speed coroutine");
        StopCoroutine("SetSpeed");
        Debug.Log("Starting speed coroutine");
        float timer = 0f;
        while (timer < cooldown)
        {
            currentSpeed = baseSpeed * speedFactor;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Coroutine ended");
        currentSpeed = baseSpeed;
    }

    private void PlayerWalk()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        Vector3 move = transform.right * x + transform.forward * z;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(move * baseSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
