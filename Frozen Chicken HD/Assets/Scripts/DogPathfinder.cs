using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoords;
    [SerializeField] Transform characterTransform;
    [SerializeField] float speed;

    Coroutine CoroutFindPath;
    Coroutine CoroutFollowPath;
    public Vector2Int StartCoords
    {
        get { return startCoords; }
        set { startCoords = value; }
    }
    [SerializeField] Vector2Int destinationCoords;
    public Vector2Int DestinationCoords
    {
        get { return destinationCoords; }
        set { destinationCoords = value; }
    }
    [SerializeField] List<Node> path = new List<Node>();
    [SerializeField] bool reached = false;
    Coroutine CoroutPath;

    [SerializeField] Node startNode;
    [SerializeField] Node destinationNode;
    [SerializeField] Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reachedDict = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        grid = gridManager.Grid;
    }

    private void Start()
    {
        // Debug.Log("Starting Coroutine");
        CoroutFindPath = StartCoroutine(FindPath());

    }

    IEnumerator FindPath()
    {
        if (CoroutFollowPath != null)
            StopCoroutine(CoroutFollowPath);

        if (!reached)
        {
            SetNodes(gridManager.GetCoordinatesFromPosition(transform.position));
            path.Clear();
            path = GetNewPath();

            // Debug.Log("Following path");
            yield return new WaitForEndOfFrame();
        }
            
        CoroutFollowPath = StartCoroutine(FollowPath());

        // Debug.Log("Breaking out of coroutine");
    }

    public void SetNodes(Vector2Int startCoords)
    {
        this.startCoords = startCoords;
        int randomX = Random.Range(startCoords.x - 3, startCoords.x + 3);
        int randomY = Random.Range(startCoords.y - 3, startCoords.y + 3);
        // Debug.Log("First Random: " + randomX);
        // Debug.Log("Second Random: " + randomY);
        this.destinationCoords = startCoords + new Vector2Int(randomX, randomY);

        while (!grid.ContainsKey(destinationCoords))
        {
            // Debug.Log("Grid Doesn't Contain Coords: " + destinationCoords);
            this.destinationCoords = startCoords + new Vector2Int(Random.Range(-3, 3), Random.Range(-3, 3));
        }

        // Debug.Log("Grid Destination Coords: " + destinationCoords);
        startNode = grid[startCoords];
        destinationNode = grid[destinationCoords];
    }

    IEnumerator FollowPath()
    {

        for (int i = 1; i < path.Count; i++)
        {
            // Debug.Log("Following path tile " + i);

            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            endPosition = new Vector3(endPosition.x, transform.position.y, endPosition.z);

            float travelPercent = 0f;

            characterTransform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            reached = true;
        }

        new WaitForSeconds(5);

        reached = false;

        if (CoroutFindPath != null)
        {
            StopCoroutine(CoroutFindPath);
            CoroutFindPath = StartCoroutine(FindPath());
        }
        else
            CoroutFindPath = StartCoroutine(FindPath());
    }
    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoords);
    }

    public List<Node> GetNewPath(Vector2Int currentCoords)
    {
        gridManager.ResetNodes();
        BreathFirstSearch(currentCoords);
        return BuildPath();
    }

    void BreathFirstSearch(Vector2Int coords)
    {
        if (!destinationNode.isWalkable)
            return;

        //startNode.isWalkable = true;
        //destinationNode.isWalkable = true;

        frontier.Clear();
        reachedDict.Clear();

        bool isRunning = true;

        frontier.Enqueue(startNode);
        reachedDict.Add(coords, grid[coords]);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbours();

            if (currentSearchNode.coordinates == destinationCoords)
            {
                isRunning = false;
            }
        }
    }

    void ExploreNeighbours()
    {
        List<Node> neighbours = new List<Node>();
        // Get nodes around current node
        for (int i = 0; i < directions.Length; i++)
        {
            Vector2Int neighbourCoordinates = currentSearchNode.coordinates + directions[i];

            if (grid.ContainsKey(neighbourCoordinates))
            {
                neighbours.Add(grid[neighbourCoordinates]);
            }
        }

        // Add node to list of explored nodes if it isn't already added
        foreach (Node neighbour in neighbours)
        { 
            if (!reachedDict.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                neighbour.connectedTo = currentSearchNode;
                reachedDict.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();
        return path;
    }
}
