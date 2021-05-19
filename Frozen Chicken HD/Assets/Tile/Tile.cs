using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3 coords;
    GridManager gridmanager;
    [SerializeField] public bool isPlaced;
    // Start is called before the first frame update
    void Awake()
    {
        gridmanager = FindObjectOfType<GridManager>();
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
