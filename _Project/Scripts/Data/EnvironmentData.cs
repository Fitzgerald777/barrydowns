using UnityEngine;

[System.Serializable]
public class EnvironmentData
{
    // Represents the hour of the day, from 0 to 24.
    public float timeOfDay;

    // Stored as "YYYY-MM-DD" for easy parsing.
    public string date;

    // Geographic coordinates for accurate sun positioning.
    public float latitude;
    public float longitude;
}
