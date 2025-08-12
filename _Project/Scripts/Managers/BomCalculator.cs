using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A service class to calculate the Bill of Materials (BOM) for a given course.
/// </summary>
public static class BomCalculator
{
    /// <summary>
    /// Calculates the aggregated bill of materials for a given course.
    /// </summary>
    /// <param name="courseData">The course data containing all placed objects.</param>
    /// <param name="objectLibrary">A list of all available CourseObjectSO assets to look up materials from.</param>
    /// <returns>A dictionary where the key is the material name and the value is the total quantity.</returns>
    public static Dictionary<string, int> CalculateBOM(CourseData courseData, List<CourseObjectSO> objectLibrary)
    {
        var bom = new Dictionary<string, int>();

        if (courseData == null || courseData.placedObjects == null || objectLibrary == null)
        {
            return bom;
        }

        // Create a lookup dictionary for faster access to the library scriptable objects
        var libraryLookup = objectLibrary.ToDictionary(so => so.objectId, so => so);

        foreach (var placedObject in courseData.placedObjects)
        {
            if (libraryLookup.TryGetValue(placedObject.objectId, out var so))
            {
                if (so.materials == null) continue;

                foreach (var material in so.materials)
                {
                    if (bom.ContainsKey(material.name))
                    {
                        bom[material.name] += material.quantity;
                    }
                    else
                    {
                        bom.Add(material.name, material.quantity);
                    }
                }
            }
        }

        return bom;
    }
}
