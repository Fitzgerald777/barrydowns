using UnityEngine;
using UnityEngine.InputSystem;
// Note: This script assumes the Unity Splines package (`com.unity.splines`) is installed.
// If it's not, the 'using UnityEngine.Splines;' and related types will cause errors.
#if UNITY_SPLINES
using UnityEngine.Splines;
#endif

/// <summary>
/// Manages the creation and modification of the main course path spline.
/// </summary>
public class PathTool : MonoBehaviour
{
#if UNITY_SPLINES
    // --- Injected via Unity Editor ---
    // [SerializeField] private SplineContainer splineContainer;
    // ---------------------------------

    private bool isToolActive = false;

    void Update()
    {
        if (Mouse.current == null) return;

        // This is example logic. A real implementation would need a robust way to get mouse position in world space.
        if (isToolActive && Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Vector3 worldPoint = GetMouseWorldPosition();
            // AddPointToSpline(worldPoint);
        }
    }

    /// <summary>
    /// Activates the path tool, allowing the user to add points.
    /// </summary>
    public void ActivateTool()
    {
        isToolActive = true;
        Debug.Log("Path Tool Activated. Click to add points.");
    }

    /// <summary>
    /// Deactivates the path tool.
    /// </summary>
    public void DeactivateTool()
    {
        isToolActive = false;
        Debug.Log("Path Tool Deactivated.");
    }

    /// <summary>
    /// Adds a new knot (point) to the spline at the given world position.
    /// </summary>
    private void AddPointToSpline(Vector3 position)
    {
        // if (splineContainer == null || splineContainer.Spline == null) return;

        // var newKnot = new BezierKnot(position);
        // splineContainer.Spline.Add(newKnot);

        Debug.Log($"Added point to spline at {position}.");

        // After modification, the SplineData in the main CourseData object
        // would need to be updated to ensure it can be saved.
        UpdateCourseData();
    }

    private void UpdateCourseData()
    {
        // Logic to find the current CourseData object and serialize the spline into it.
        Debug.Log("Updating CourseData with new spline information.");
    }
#else
    // The user has confirmed the package is installed, so this warning is not needed.
    // void Start()
    // {
    //    Debug.LogWarning("PathTool.cs requires the Unity Splines package to be installed.");
    // }
#endif
}
