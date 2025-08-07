using UnityEngine;
using System;

/// <summary>
/// Controls the scene's lighting and skybox based on time, date, and geographic location.
/// </summary>
public class TimeAndLocationController : MonoBehaviour
{
    // --- Injected via Unity Editor ---
    // [SerializeField] private Light sunLight;
    // ---------------------------------

    // --- Controllable Properties ---
    [Header("Time & Date")]
    [Range(0, 24)] public float TimeOfDay = 12f; // In hours, e.g., 14.5 is 2:30 PM
    [Range(1, 365)] public int DayOfYear = 180;

    [Header("Location")]
    [Range(-90, 90)] public float Latitude = 51.5f;   // London
    [Range(-180, 180)] public float Longitude = -0.12f; // London
    // -----------------------------

    // This would be called by UI sliders/input fields.
    public void SetTimeOfDay(float newTime) { TimeOfDay = newTime; UpdateSunPosition(); }
    public void SetDayOfYear(int newDay) { DayOfYear = newDay; UpdateSunPosition(); }
    public void SetLatitude(float newLat) { Latitude = newLat; UpdateSunPosition(); }
    public void SetLongitude(float newLon) { Longitude = newLon; UpdateSunPosition(); }


    void Update()
    {
        // Continuously update the sun in the editor for real-time feedback.
        // In a build, you might only call this when a value changes.
        if (Application.isEditor)
        {
            UpdateSunPosition();
        }
    }

    /// <summary>
    /// Calculates and applies the sun's rotation based on the public properties.
    /// This uses a simplified model for astronomical calculations.
    /// </summary>
    private void UpdateSunPosition()
    {
        // The general principle is to convert Latitude, Longitude, and Time into a direction vector for the sun.

        // 1. Time equation and solar declination
        float solarTime = TimeOfDay + (Longitude / 15f); // Adjust time for longitude
        float declination = -23.45f * Mathf.Cos((2 * Mathf.PI / 365f) * (DayOfYear + 10));

        // 2. Hour angle
        float hourAngle = 15f * (solarTime - 12f);

        // 3. Convert all to radians for trigonometric functions
        float latRad = Latitude * Mathf.Deg2Rad;
        float declRad = declination * Mathf.Deg2Rad;
        float hourAngleRad = hourAngle * Mathf.Deg2Rad;

        // 4. Calculate sun's altitude (vertical angle) and azimuth (horizontal angle)
        float altitude = Mathf.Asin(Mathf.Sin(latRad) * Mathf.Sin(declRad) + Mathf.Cos(latRad) * Mathf.Cos(declRad) * Mathf.Cos(hourAngleRad));
        float azimuth = Mathf.Atan2(
            -Mathf.Sin(hourAngleRad),
            Mathf.Cos(latRad) * Mathf.Sin(declRad) - Mathf.Sin(latRad) * Mathf.Cos(declRad) * Mathf.Cos(hourAngleRad)
        );

        // 5. Convert altitude and azimuth to a Quaternion rotation
        Quaternion sunRotation = Quaternion.Euler(altitude * Mathf.Rad2Deg, azimuth * Mathf.Rad2Deg, 0);

        // Apply the rotation to the directional light
        // if (sunLight != null)
        // {
        //     sunLight.transform.rotation = sunRotation;
        // }

        // Here, you would also update the properties of the skybox material to match the time of day.
        // For example: RenderSettings.skybox.SetFloat("_AtmosphereThickness", 1.0f - (sunLight.transform.forward.y));
    }
}
