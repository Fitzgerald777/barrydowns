using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages the switching between 2D (orthographic) and 3D (perspective) views.
/// </summary>
public class ViewModeManager : MonoBehaviour
{
    // --- Injected via Unity Editor ---
    // [Header("Cameras")]
    // public Camera camera2D;
    // public Camera camera3D;
    // ---------------------------------

    public enum ViewMode { Mode2D, Mode3D }
    public ViewMode currentMode { get; private set; }

    // This would be populated by the CourseBuilder as objects are created.
    // It maps a 2D prefab instance to its corresponding 3D prefab instance.
    private readonly Dictionary<GameObject, GameObject> instanceMap = new Dictionary<GameObject, GameObject>();

    void Start()
    {
        // Default to 2D mode on start.
        SwitchTo2D();
    }

    /// <summary>
    /// Toggles between the 2D and 3D view modes.
    /// </summary>
    public void ToggleViewMode()
    {
        if (currentMode == ViewMode.Mode2D)
        {
            SwitchTo3D();
        }
        else
        {
            SwitchTo2D();
        }
    }

    public void SwitchTo2D()
    {
        currentMode = ViewMode.Mode2D;
        // camera3D.gameObject.SetActive(false);
        // camera2D.gameObject.SetActive(true);

        foreach (var pair in instanceMap)
        {
            pair.Key.SetActive(true);   // Show 2D representation
            pair.Value.SetActive(false); // Hide 3D representation
        }
        Debug.Log("Switched to 2D View.");
    }

    public void SwitchTo3D()
    {
        currentMode = ViewMode.Mode3D;
        // camera2D.gameObject.SetActive(false);
        // camera3D.gameObject.SetActive(true);

        foreach (var pair in instanceMap)
        {
            pair.Key.SetActive(false); // Hide 2D representation
            pair.Value.SetActive(true);  // Show 3D representation
        }
        Debug.Log("Switched to 3D View.");
    }

    /// <summary>
    /// Registers a pair of 2D and 3D object instances. Called when a new object is placed.
    /// </summary>
    public void RegisterObjectPair(GameObject instance2D, GameObject instance3D)
    {
        if (instance2D == null || instance3D == null) return;

        instanceMap[instance2D] = instance3D;

        // Ensure the new object's visibility matches the current view mode.
        instance2D.SetActive(currentMode == ViewMode.Mode2D);
        instance3D.SetActive(currentMode == ViewMode.Mode3D);
    }
}
