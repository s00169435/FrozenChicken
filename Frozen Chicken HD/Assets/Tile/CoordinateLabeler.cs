using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.cyan;
    [SerializeField] TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Tile tile;
    // Start is called before the first frame update
    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        tile = GetComponentInParent<Tile>();
        DisplayCoordinates();
        UpdateName();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateName();
            label.enabled = true;
        }
        ToggleLabels();
    }


    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
            Debug.Log("Seeing labels.");
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        if (label)
        {
            label.text = coordinates.x + "," + coordinates.y;
        }

        if (tile.isPlaced)
        {
            label.color = blockedColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void UpdateName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
