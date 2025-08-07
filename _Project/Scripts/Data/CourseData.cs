using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CourseData
{
    public string courseName;
    public string creationDate; // Stored as "MMDDYY"

    public List<CourseObjectData> placedObjects = new List<CourseObjectData>();
    public SplineData coursePath;
    public EnvironmentData environmentSettings;

    public CourseData()
    {
        placedObjects = new List<CourseObjectData>();
        coursePath = new SplineData();
        environmentSettings = new EnvironmentData();
    }
}
