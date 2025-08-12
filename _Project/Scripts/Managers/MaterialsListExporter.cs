using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

/// <summary>
/// Handles the exporting of the course's Bill of Materials to a PDF file.
/// </summary>
public class MaterialsListExporter : MonoBehaviour
{
    // Declare the external JavaScript function from the .jslib file.
    [DllImport("__Internal")]
    private static extern void DownloadPdfFromSvg(string svgContent, string fileName);

    /// <summary>
    /// Public method to initiate the export process.
    /// This can be called from a UI button.
    /// </summary>
    /// <param name="courseData">The current course data.</param>
    /// <param name="objectLibrary">The library of all course objects.</param>
    public void ExportMaterialsList(CourseData courseData, List<CourseObjectSO> objectLibrary)
    {
        // Step 1: Calculate the Bill of Materials.
        Dictionary<string, int> bom = BomCalculator.CalculateBOM(courseData, objectLibrary);

        // Step 2: Generate an SVG string from the BOM data.
        string svgString = GenerateSvgTable(bom);

        // Step 3: Trigger the download of the PDF.
        TriggerPdfDownload(svgString);
    }

    /// <summary>
    /// Generates an SVG string representing a table of the materials list.
    /// </summary>
    /// <param name="bom">The bill of materials data.</param>
    /// <returns>A string containing the SVG table.</returns>
    private string GenerateSvgTable(Dictionary<string, int> bom)
    {
        if (bom == null || bom.Count == 0)
        {
            return "<svg width=\"400\" height=\"100\" xmlns=\"http://www.w3.org/2000/svg\"><text x=\"10\" y=\"20\">No materials to list.</text></svg>";
        }

        var sb = new StringBuilder();
        int rowHeight = 30;
        int tableWidth = 400;
        int headerHeight = 40;
        int tableHeight = headerHeight + bom.Count * rowHeight;
        string fontFamily = "Arial, sans-serif";

        sb.AppendLine($"<svg width=\"{tableWidth}\" height=\"{tableHeight}\" xmlns=\"http://www.w3.org/2000/svg\">");

        // Add some basic styling
        sb.AppendLine("<defs>");
        sb.AppendLine("<style>");
        sb.AppendLine($".header {{ font-family: {fontFamily}; font-size: 16px; font-weight: bold; fill: #ffffff; }}");
        sb.AppendLine($".cell {{ font-family: {fontFamily}; font-size: 14px; fill: #333333; }}");
        sb.AppendLine("</style>");
        sb.AppendLine("</defs>");

        // Header Background
        sb.AppendLine($"<rect x=\"0\" y=\"0\" width=\"{tableWidth}\" height=\"{headerHeight}\" fill=\"#4a4a4a\" />");

        // Header Text
        sb.AppendLine($"<text x=\"20\" y=\"25\" class=\"header\">Material</text>");
        sb.AppendLine($"<text x=\"300\" y=\"25\" class=\"header\">Quantity</text>");

        int i = 0;
        foreach (var item in bom)
        {
            int yPos = headerHeight + i * rowHeight;
            string rowColor = i % 2 == 0 ? "#f2f2f2" : "#ffffff";

            // Row Background
            sb.AppendLine($"<rect x=\"0\" y=\"{yPos}\" width=\"{tableWidth}\" height=\"{rowHeight}\" fill=\"{rowColor}\" />");

            // Row Text
            sb.AppendLine($"<text x=\"20\" y=\"{yPos + rowHeight / 2 + 5}\" class=\"cell\">{item.Key}</text>");
            sb.AppendLine($"<text x=\"300\" y=\"{yPos + rowHeight / 2 + 5}\" class=\"cell\">{item.Value}</text>");

            i++;
        }

        sb.AppendLine("</svg>");
        return sb.ToString();
    }

    /// <summary>
    /// Calls a JavaScript function to convert the SVG to a PDF and trigger a download.
    /// </summary>
    /// <param name="svgContent">The SVG content as a string.</param>
    private void TriggerPdfDownload(string svgContent)
    {
        Debug.Log("Triggering PDF Download via JSLib...");
#if UNITY_WEBGL && !UNITY_EDITOR
        DownloadPdfFromSvg(svgContent, "MaterialsList.pdf");
#else
        Debug.LogWarning("PDF export is only available in WebGL builds. SVG content has been logged instead.");
        Debug.Log(svgContent);
#endif
    }
}
