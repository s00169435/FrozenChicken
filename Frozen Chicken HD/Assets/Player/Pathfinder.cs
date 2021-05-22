using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
        [SerializeField] Vector2Int startCoords;
        [SerializeField] Vector2Int destinationCoords;
        public Vector2Int StartCoords
        {
            get { return startCoords; }
            set { startCoords = value; }
        }
        public Vector2Int DestinationCoords
        {
            get { return destinationCoords; }
            set { destinationCoords = value; }
        }

        [SerializeField] Node startNode;
        [SerializeField] Node destinationNode;
        [SerializeField] Node currentSearchNode;

        Queue<Node> frontier = new Queue<Node>();
        Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

        Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
        GridManager gridManager;
        Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

        void Awake()
        {
            gridManager = FindObjectOfType<GridManager>();

            /* if (gridManager != null)
            {
                grid = gridManager.Grid;
                startNode = grid[startCoords];
                destinationNode = grid[destinationCoords];
            } */
        }

        void Start()
        {
            //GetNewPath();
        }

        public void SetNodes(Vector2Int startCoords, Vector2Int destinationCoords)
        {
            grid = gridManager.Grid;
            this.startCoords = startCoords;
            this.destinationCoords = destinationCoords;
            startNode = grid[startCoords];
            destinationNode = grid[destinationCoords];
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
            startNode.isWalkable = true;
            destinationNode.isWalkable = true;

            frontier.Clear();
            reached.Clear();

            bool isRunning = true;

            frontier.Enqueue(startNode);
            reached.Add(coords, grid[coords]);

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
                if (!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
                {
                    neighbour.connectedTo = currentSearchNode;
                    reached.Add(neighbour.coordinates, neighbour);
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

        public bool WillBlockPath(Vector2Int coordinates)
        {
            if (grid.ContainsKey(coordinates))
            {
                bool previousState = grid[coordinates].isWalkable;

                grid[coordinates].isWalkable = false;
                List<Node> newPath = GetNewPath();
                grid[coordinates].isWalkable = previousState;

                if (newPath.Count <= 1)
                {
                    Debug.Log("Will block path");
                    GetNewPath();
                    return true;
                }
            }

            Debug.Log("Won't block path");
            return false;
        }

        public void NotifyReceivers()
        {
            BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
        }
}