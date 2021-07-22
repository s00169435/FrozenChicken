using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float baseSpeed = 5f;
    [SerializeField] float currentSpeed;

    public LayerMask groundMask;
    [SerializeField] Pathfinder pathFinder;
    [SerializeField] GridManager gridManager;
    [SerializeField] List<Node> path = new List<Node>();
    [SerializeField] bool canMove;
    [SerializeField] Transform characterTransform;
    public bool CanMove { get => canMove; set => canMove = value; }

    Coroutine moveCoroutine;
    [SerializeField] GameObject IndicatorPrefab;
    GameObject Indicator;

    [SerializeField] VisualEffectAsset VFXPrefab;
    VisualEffectAsset VFXInstance;
    [SerializeField] bool isFrustrated;
    public bool IsFrustrated { get => isFrustrated; }

    private void Awake()
    {
        IndicatorPrefab.SetActive(true);
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
                StopMoving();
                path.Clear();

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 3000f, groundMask))
                {
                    Tile tile = hit.transform.parent.GetComponent<Tile>();

                    Vector2Int startsCoords = gridManager.GetCoordinatesFromPosition(transform.position);
                    Vector2Int destinationCoords = gridManager.GetCoordinatesFromPosition(hit.collider.transform.position);

                    pathFinder.SetNodes(startsCoords, destinationCoords);
                    path = pathFinder.GetNewPath();


                    if (tile && path.Count > 1)
                    {
                        IndicatorPrefab.SetActive(true);
                        IndicatorPrefab.transform.position = tile.transform.position;
                        // VFXInstance = Instantiate(VFXPrefab, tile.transform.position, Quaternion.identity);
                        moveCoroutine = StartCoroutine(FollowPath());
                        // Debug.Log("Indicator Prefab name: " + VFXPrefab.name);
                    }
                }
            }

        }
    }

    private void StopMoving()
    {
        IndicatorPrefab.SetActive(false);

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
    }

    private void IndicatePosition()
    {

    }
    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {

            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            endPosition = new Vector3(endPosition.x, transform.position.y, endPosition.z);

            float travelPercent = 0f;

            characterTransform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * currentSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }


        IndicatorPrefab.SetActive(false);
    }

    public IEnumerator SetSpeed(float speedFactor, float cooldown)
    {
        isFrustrated = true;
        Debug.Log("Stopping speed coroutine");
        StopCoroutine("SetSpeed");
        Debug.Log("Walk speed slowed");
        float timer = 0f;
        while (timer < cooldown)
        {
            currentSpeed = baseSpeed * speedFactor;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        isFrustrated = false;
        Debug.Log("Can walk normally");
        currentSpeed = baseSpeed;
    }
}
