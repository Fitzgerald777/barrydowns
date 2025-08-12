using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages the logic for a runtime transform gizmo that can rotate and scale an object.
/// This script would be attached to a GameObject that has visual handles for rotation and scaling.
/// </summary>
public class TransformGizmo : MonoBehaviour
{
    private Transform target;
    private Camera mainCamera;

    private enum GizmoMode { None, Rotating, ScalingWidth }
    private GizmoMode currentMode = GizmoMode.None;

    void Awake()
    {
        mainCamera = Camera.main;
        // The gizmo should be inactive by default
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (target == null || Pointer.current == null) return;

        // Follow the target's position
        transform.position = target.position;

        // --- Example Input Handling ---
        // This is a simplified representation of how the logic would work.
        // A real implementation would involve raycasting to the specific gizmo handles.

        if (Pointer.current.leftButton.wasPressedThisFrame)
        {
            // Here you would check if the mouse is over a specific handle (e.g., rotation or scale)
            // For example: if (IsMouseOverRotationHandle()) currentMode = GizmoMode.Rotating;
        }

        if (Pointer.current.leftButton.wasReleasedThisFrame)
        {
            currentMode = GizmoMode.None;
        }

        switch (currentMode)
        {
            case GizmoMode.Rotating:
                HandleRotation();
                break;
            case GizmoMode.ScalingWidth:
                HandleScaling();
                break;
        }
    }

    /// <summary>
    /// Attaches the gizmo to a target transform and makes it visible.
    /// </summary>
    public void Attach(Transform newTarget)
    {
        target = newTarget;
        gameObject.SetActive(true);
        Debug.Log($"Gizmo attached to {target.name}.");
    }

    /// <summary>
    /// Detaches the gizmo from the current target and hides it.
    /// </summary>
    public void Detach()
    {
        target = null;
        gameObject.SetActive(false);
    }

    private void HandleRotation()
    {
        if (Pointer.current == null) return;
        // Example: Rotate based on mouse movement around the object
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        Vector3 dir = mousePos - target.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        target.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void HandleScaling()
    {
        if (Pointer.current == null) return;
        // Example: Scale width based on horizontal mouse movement
        float mouseDeltaX = Pointer.current.delta.ReadValue().x;
        Vector3 currentScale = target.localScale;
        currentScale.x += mouseDeltaX;
        target.localScale = currentScale;
    }
}
