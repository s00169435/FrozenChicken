using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float baseSpeed = 5f;
    [SerializeField] float currentSpeed;

    public LayerMask groundMask;
    [SerializeField] Pathfinder pathFinder;
    [SerializeField] GridManager gridManager;
    [SerializeField] List<Node> path = new List<Node>();
    [SerializeField] bool canMove;
    public bool CanMove { get => canMove; set => canMove = value; }

    Coroutine moveCoroutine;

    private void Awake()
    {
        pathFinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
        currentSpeed = baseSpeed;
        canMove = true;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canMove)
            {
                if (moveCoroutine != null)
                    StopCoroutine(moveCoroutine);

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
}
