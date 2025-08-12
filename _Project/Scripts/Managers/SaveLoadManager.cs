using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

/// <summary>
/// A static utility class for saving and loading course data.
/// NOTE: This is a static class and CANNOT be attached to a GameObject in the scene.
/// To use it, simply call its static methods, e.g., `SaveLoadManager.SaveCourse(data);`.
/// </summary>
public static class SaveLoadManager
{
    // In a WebGL build, we need to use a Javascript plugin to handle the file download.
    // This is a declaration of a function that exists in a .jslib file in the project.
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] data, int dataLength);

    // Public method to save the course data.
    public static void SaveCourse(CourseData data)
    {
        string courseName = data.courseName;
        string date = System.DateTime.Now.ToString("MMddyy");
        string filename = $"{courseName}_{date}.json";

        string json = JsonUtility.ToJson(data, true); // Use 'true' for pretty print for readability
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);

#if UNITY_WEBGL && !UNITY_EDITOR
        // In the WebGL build, call the Javascript function to trigger a download.
        // A GameObject in the scene would need a corresponding method to handle success/failure callbacks.
        DownloadFile("SaveLoadCallbackReceiver", "OnFileDownloaded", filename, bytes, bytes.Length);
#else
        // In the Unity Editor or on standalone builds, save directly to a file for testing.
        string path = Path.Combine(Application.persistentDataPath, filename);
        File.WriteAllBytes(path, bytes);
        Debug.Log($"File saved to: {path}");
#endif
    }

    // Loading in WebGL is typically handled by a file input element on the HTML page.
    // A Javascript function would read the file and send the contents to Unity.
    // This C# method would be called by that Javascript function.
    public static CourseData LoadCourseFromJson(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Cannot load course from empty JSON.");
            return null;
        }

        CourseData data = JsonUtility.FromJson<CourseData>(json);
        return data;
    }
}

// Example of a MonoBehaviour that could receive callbacks from the Javascript plugin.
/// <summary>
/// An example MonoBehaviour that can receive callbacks from the Javascript download plugin.
/// NOTE: An object with this script attached should exist in your scene to handle
/// callbacks from the WebGL file download process.
/// </summary>
public class SaveLoadCallbackReceiver : MonoBehaviour
{
    public void OnFileDownloaded()
    {
        Debug.Log("File download successful!");
        // You could show a UI message to the user here.
    }

    public void OnFileDownloadFailed(string error)
    {
        Debug.LogError($"File download failed: {error}");
        // You could show an error message to the user here.
    }
}
