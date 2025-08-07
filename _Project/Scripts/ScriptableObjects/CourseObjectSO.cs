using UnityEngine;

/// <summary>
/// Defines a ScriptableObject that holds the data for a single type of course object (e.g., a specific jump or decoration).
/// These can be created as assets in the Unity Editor to easily manage the library of available objects.
/// </summary>
[CreateAssetMenu(fileName = "NewCourseObject", menuName = "Course Design/Course Object")]
public class CourseObjectSO : ScriptableObject
{
    [Tooltip("A unique identifier for this object type.")]
    public string objectId;

    [Tooltip("The user-facing name for this object.")]
    public string displayName;

    [Tooltip("The icon shown in the build menu UI.")]
    public Sprite icon;

    [Tooltip("The prefab to instantiate for the 2D top-down view.")]
    public GameObject prefab2D;

    [Tooltip("The prefab to instantiate for the 3D perspective view.")]
    public GameObject prefab3D;
}
