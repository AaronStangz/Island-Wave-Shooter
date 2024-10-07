using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class Build : MonoBehaviour
{
    public GameObject wallPrefab;   // The prefab for walls
    public GameObject floorPrefab;  // The prefab for floors
    public LayerMask buildableLayer; // Layer mask for buildable area
    public float gridSize = 4.0f;   // Size of the grid in world units

    private bool basePlaced = false; // Flag to indicate if the first object has been placed
    private Vector3 basePosition;   // Position of the first placed object (the base of the grid)

    private GameObject previewObject; // The object preview to show where the player is placing the item

    void Update()
    {
        HandleBuildingInput();
    }

    void HandleBuildingInput()
    {
        // Check for input to place walls or floors
        if (Input.GetMouseButtonDown(0)) // Left click to place object
        {
            PlaceObject(floorPrefab); // Replace this with wallPrefab if placing walls
        }

        // Right-click to switch between wall and floor preview
        if (Input.GetMouseButtonDown(1))
        {
            SwitchBuildMode();
        }

        // Show preview of the object being placed
        if (basePlaced)
        {
            ShowPlacementPreview();
        }
    }

    void PlaceObject(GameObject prefab)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Raycast to find the buildable area (hit the floor/terrain/other object)
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildableLayer))
        {
            Vector3 hitPosition = hit.point;

            // If the base has been placed, align the position to the grid
            if (basePlaced)
            {
                Vector3 gridPosition = AlignToGrid(hitPosition);
                Instantiate(prefab, gridPosition, Quaternion.identity);
            }
            else
            {
                // Place the first object and set the base position
                basePosition = AlignToGrid(hitPosition);
                Instantiate(prefab, basePosition, Quaternion.identity);
                basePlaced = true;
            }
        }
    }

    Vector3 AlignToGrid(Vector3 position)
    {
        // Align the position to the grid based on the first placed object's position
        float x = Mathf.Round((position.x - basePosition.x) / gridSize) * gridSize + basePosition.x;
        float z = Mathf.Round((position.z - basePosition.z) / gridSize) * gridSize + basePosition.z;
        return new Vector3(x, basePosition.y, z);
    }

    void SwitchBuildMode()
    {
        // Switch between building wall and floor based on the current preview object
        if (previewObject != null && previewObject.CompareTag("Wall"))
        {
            Destroy(previewObject);
            previewObject = Instantiate(floorPrefab);
        }
        else if (previewObject != null && previewObject.CompareTag("Floor"))
        {
            Destroy(previewObject);
            previewObject = Instantiate(wallPrefab);
        }
        else
        {
            // Default to placing a floor if no preview object exists
            previewObject = Instantiate(floorPrefab);
        }
    }

    void ShowPlacementPreview()
    {
        if (previewObject == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildableLayer))
        {
            Vector3 hitPosition = hit.point;

            // Align the preview object to the grid
            Vector3 gridPosition = AlignToGrid(hitPosition);
            previewObject.transform.position = gridPosition;
        }
    }
}

