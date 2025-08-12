using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Manages the primary logic for the 2D course editor, including object placement and selection.
/// </summary>
public class CourseBuilder : MonoBehaviour
{
    // --- Injected from Unity Editor ---
    // [Tooltip("The camera used for raycasting into the 2D scene.")]
    // public Camera editorCamera;
    // ---------------------------------

    private CourseObjectSO selectedObjectToBuild;
    private GameObject currentlySelectedInstance;

    // This would be populated from the current CourseData object
    private List<GameObject> placedCourseObjects = new List<GameObject>();

    void Update()
    {
        // Ensure the pointer is valid before using it
        if (Pointer.current == null) return;

        // Example logic for placing an object
        if (selectedObjectToBuild != null && Pointer.current.leftButton.wasPressedThisFrame && !IsPointerOverUI())
        {
            // Raycast from mouse position to the ground plane to get the world position
            // Vector3 placePosition = GetMouseWorldPosition();
            // PlaceObject(selectedObjectToBuild, placePosition);
        }

        // Example logic for selecting an object
        if (Pointer.current.leftButton.wasPressedThisFrame && !IsPointerOverUI())
        {
            // Raycast to see if the user clicked on an existing object
            // GameObject clickedObject = GetClickedObject();
            // SelectObject(clickedObject);
        }
    }

    /// <summary>
    /// Called from the Build Menu UI when a new object is selected for building.
    /// </summary>
    public void SetObjectToBuild(CourseObjectSO courseObject)
    {
        selectedObjectToBuild = courseObject;
        Debug.Log($"Selected {courseObject.displayName} for building.");
    }

    private void PlaceObject(CourseObjectSO objectSO, Vector3 position)
    {
        // In a real implementation, we would instantiate the objectSO.prefab2D
        // GameObject newInstance = Instantiate(objectSO.prefab2D, position, Quaternion.identity);
        // placedCourseObjects.Add(newInstance);

        // Here, you would also create a new CourseObjectData and add it to the currentCourse.placedObjects list.

        Debug.Log($"Placed {objectSO.displayName} at {position}.");

        // Deselect from build menu to prevent accidental multi-placing
        selectedObjectToBuild = null;
    }

    private void SelectObject(GameObject instance)
    {
        if (currentlySelectedInstance == instance) return;

        currentlySelectedInstance = instance;
        Debug.Log($"Selected instance: {instance.name}");

        // This is where you would activate the Transform Gizmo on the selected object.
        // e.g., transformGizmo.Attach(currentlySelectedInstance.transform);
    }

    private bool IsPointerOverUI()
    {
        // Simple check to prevent placing objects when clicking on UI buttons.
        return EventSystem.current.IsPointerOverGameObject();
    }
}
