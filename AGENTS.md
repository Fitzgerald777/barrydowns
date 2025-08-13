# Agent Instructions for Course Designer Project

This document provides guidance for AI agents working on this Unity project.

## 1. Project Overview

This is a Unity project for designing equestrian show jumping courses. It includes features for 2D course layout, object placement, and exporting course designs.

## 2. Coding Conventions & Key Systems

### Input System
This project uses the **new Unity Input System** (`UnityEngine.InputSystem`). All new input-related code **must** use this system.

- **Do not use** the old `UnityEngine.Input` API (e.g., `Input.GetMouseButtonDown`, `Input.GetAxis`). Using it will cause `InvalidOperationException` errors at runtime.
- For mouse-specific input, use the `Mouse` class. The generic `Pointer` class does not have button-specific properties.
  - **Example (Mouse Click):** `Mouse.current.leftButton.wasPressedThisFrame`
  - **Example (Mouse Position):** `Mouse.current.position.ReadValue()`
  - **Example (Mouse Delta):** `Mouse.current.delta.ReadValue()`
- Remember to add `using UnityEngine.InputSystem;` to your scripts.
- Always check if `Mouse.current` is null before accessing it, especially in `Update()` loops.

### Static vs. MonoBehaviour Managers
The project uses two types of manager classes:
- **Static Managers** (e.g., `SaveLoadManager`, `BomCalculator`): These are static utility classes that do not hold state and provide global functionality. They **cannot** be attached to GameObjects. You call them directly, e.g., `BomCalculator.CalculateBOM(...)`.
- **MonoBehaviour Managers** (e.g., `CourseBuilder`, `MaterialsListExporter`): These are standard `MonoBehaviour` scripts that are attached to GameObjects in a scene. They are used for logic that needs to interact with the scene or run on the game loop (`Update`, `Start`, etc.).

## 3. Key Feature Implementations

### Materials List PDF Export
This feature allows users to export the Bill of Materials (BOM) for a course as a PDF.
- **Data:** To define the materials for an obstacle, edit its corresponding `CourseObjectSO` asset and add items to the `materials` list.
- **Calculation:** The `BomCalculator.cs` class handles the logic of aggregating all materials.
- **Export Pipeline (WebGL):** The export process is handled by `MaterialsListExporter.cs` and works as follows:
    1. C# calculates the BOM.
    2. C# generates a styled **SVG** string representing the materials table.
    3. C# calls the `DownloadPdfFromSvg` JavaScript function located in `_Project/Plugins/WebGL/MaterialsListExporter.jslib`.
    4. The JavaScript function uses the **jsPDF** and **svg2pdf.js** libraries to convert the SVG into a PDF.
    5. The JavaScript triggers a file download in the user's browser.
- **Dependencies:** This feature requires the `jsPDF` and `svg2pdf.js` JavaScript libraries to be loaded in the final WebGL build's HTML page.

## 4. Project Structure
The core scripts are organized as follows:
- `_Project/Scripts/Data/`: Contains plain C# classes that define the data structures for the application (e.g., `CourseData`).
- `_Project/Scripts/Managers/`: Contains the main logic-handling classes for different features.
- `_Project/Scripts/ScriptableObjects/`: Contains definitions for Scriptable Objects, which are used as data assets (e.g., `CourseObjectSO`).
- `_Project/Scripts/UI/`: Contains scripts related to user interface elements and interactions (e.g., `TransformGizmo`).
- `_Project/Plugins/WebGL/`: Contains JavaScript libraries (`.jslib`) for interacting with browser APIs in WebGL builds.
