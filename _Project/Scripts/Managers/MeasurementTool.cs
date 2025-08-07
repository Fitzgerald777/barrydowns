using UnityEngine;
// Note: This script assumes the Unity Splines package (`com.unity.splines`) is installed.
#if UNITY_SPLINES
using UnityEngine.Splines;
#endif

/// <summary>
/// Provides tools for taking measurements within the course editor.
/// </summary>
public class MeasurementTool : MonoBehaviour
{
#if UNITY_SPLINES
    // --- Injected via Unity Editor ---
    // [SerializeField] private SplineContainer coursePathSpline;
    // ---------------------------------

    private bool isMeasuringP2P = false;
    private Vector3 firstPoint;

    void Update()
    {
        if (isMeasuringP2P && Input.GetMouseButtonDown(0))
        {
            HandleP2PMeasurement();
        }
    }

    /// <summary>
    /// Calculates and returns the total length of the main course path spline.
    /// </summary>
    public float GetTotalSplineLength()
    {
        // if (coursePathSpline == null || coursePathSpline.Spline == null)
        // {
        //     Debug.LogError("Course Path Spline is not assigned.");
        //     return 0f;
        // }
        //
        // float length = coursePathSpline.Spline.GetLength();

        float length = 123.45f; // Placeholder for script-only execution
        Debug.Log($"Total course path length: {length} meters.");
        return length;
    }

    /// <summary>
    /// Activates the point-to-point measurement mode.
    /// </summary>
    public void ActivatePointToPointMode()
    {
        isMeasuringP2P = true;
        firstPoint = Vector3.positiveInfinity; // Use an unlikely value to check if the first point is set
        Debug.Log("Point-to-Point measurement mode activated. Click two points in the scene.");
    }

    private void HandleP2PMeasurement()
    {
        // Vector3 clickedPoint = GetMouseWorldPosition(); // Helper function to get mouse pos in world
        Vector3 clickedPoint = new Vector3(Random.Range(0,10), 0, Random.Range(0,10)); // Placeholder

        if (firstPoint == Vector3.positiveInfinity)
        {
            firstPoint = clickedPoint;
            Debug.Log($"First point selected at {firstPoint}.");
        }
        else
        {
            float distance = Vector3.Distance(firstPoint, clickedPoint);
            Debug.Log($"Second point selected at {clickedPoint}. Measured distance: {distance:F2} meters.");

            // Deactivate mode after measurement is complete
            isMeasuringP2P = false;
        }
    }
#else
    void Start()
    {
        Debug.LogWarning("MeasurementTool.cs requires the Unity Splines package to be installed.");
    }
#endif
}
