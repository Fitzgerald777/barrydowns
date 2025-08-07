using UnityEngine;
// Note: In a real project, you would need to import the correct UI package,
// e.g., using UnityEngine.UI; for UGUI or using UnityEngine.UIElements; for UI Toolkit.
// You would also need TextMeshPro for the input field, so: using TMPro;

/// <summary>
/// Manages the main menu UI, including the save dialog.
/// In the Unity Editor, you would drag references to the UI elements onto the public fields of this script.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    // --- Injected via Unity Editor ---
    // [Header("Main Menu Buttons")]
    // public Button newCourseButton;
    // public Button loadCourseButton;
    // public Button showSaveDialogButton;

    // [Header("Save Dialog")]
    // public GameObject saveDialogPanel;
    // public TMP_InputField courseNameInputField;
    // public Button saveButton;
    // public Button cancelButton;
    // ---------------------------------

    private CourseData currentCourse;

    void Start()
    {
        // In a real implementation, you'd add listeners to the buttons here.
        // For example: saveButton.onClick.AddListener(HandleSave);

        // The save dialog should be hidden by default.
        // saveDialogPanel.SetActive(false);
    }

    public void HandleNewCourse()
    {
        Debug.Log("Starting new course...");
        currentCourse = new CourseData();
        // Further logic would clear the scene of any existing course objects.
    }

    public void HandleLoadCourse()
    {
        // This would trigger the browser's file upload dialog via Javascript.
        // The browser would then call back to `OnFileLoadedFromBrowser` with the file content.
        Debug.Log("Load course button clicked. Awaiting file upload from browser...");
    }

    public void HandleDuplicateCourse()
    {
        if (currentCourse == null)
        {
            Debug.LogError("No course loaded to duplicate.");
            return;
        }

        // Create a deep copy by serializing and then deserializing the data.
        // This is a simple and effective way to duplicate complex objects with lists.
        string json = JsonUtility.ToJson(currentCourse);
        currentCourse = JsonUtility.FromJson<CourseData>(json);

        // Clear the name so the user is prompted to enter a new one when saving.
        currentCourse.courseName = string.Empty;

        Debug.Log("Current course has been duplicated. You can now modify it and save it as a new file.");
        // The user can now open the save dialog, enter a new name, and save the duplicated course.
    }

    public void ShowSaveDialog()
    {
        // saveDialogPanel.SetActive(true);
        Debug.Log("Showing save dialog.");
    }

    public void HandleSave()
    {
        // string courseName = courseNameInputField.text;
        string courseName = "ExampleCourseName"; // Placeholder for editor script

        if (currentCourse != null && !string.IsNullOrWhiteSpace(courseName))
        {
            currentCourse.courseName = courseName;
            SaveLoadManager.SaveCourse(currentCourse);
            // saveDialogPanel.SetActive(false);
            Debug.Log($"Save button clicked. Course name: {courseName}.");
        }
        else
        {
            Debug.LogError("Cannot save. Course name is empty or no course is active.");
        }
    }

    public void HandleCancelSave()
    {
        // saveDialogPanel.SetActive(false);
        Debug.Log("Hiding save dialog.");
    }

    /// <summary>
    /// This method would be called from Javascript after a file is selected by the user in the browser.
    /// </summary>
    /// <param name="jsonContent">The string content of the loaded .json file.</param>
    public void OnFileLoadedFromBrowser(string jsonContent)
    {
        currentCourse = SaveLoadManager.LoadCourseFromJson(jsonContent);
        if (currentCourse != null)
        {
            Debug.Log($"Successfully loaded course: {currentCourse.courseName}");
            // Logic to reconstruct the scene from the loaded data would go here.
        }
    }
}
