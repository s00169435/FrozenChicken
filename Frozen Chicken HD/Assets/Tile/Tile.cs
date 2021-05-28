using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3 coords;
    GridManager gridmanager;
    [SerializeField] public bool isPlaced;
    [SerializeField] Material matWalkable;
    [SerializeField] Material matBlocked;
    [SerializeField] GameObject mesh;
    // Start is called before the first frame update
    void Awake()
    {
        gridmanager = FindObjectOfType<GridManager>();
        MeshRenderer meshRenderer = mesh.GetComponent<MeshRenderer>();
        meshRenderer.material = isPlaced ? matBlocked : matWalkable;
    }

    private void Start()
    {
        Vector2Int gridCoords = gridmanager.GetCoordinatesFromPosition(transform.position);
        gridmanager.Grid[gridCoords].isWalkable = !isPlaced;
        coords = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
