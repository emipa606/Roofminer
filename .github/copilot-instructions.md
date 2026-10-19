# GitHub Copilot Instructions for the Roofminer Mod

## Mod Overview and Purpose

The **Roofminer (Continued)** mod is an update of Sparr's original mod, designed to enhance the mining operations in the game RimWorld. The primary objective of the mod is to facilitate mining in mountain areas, allowing players to mine tunnels and rooms without triggering infestations. This is achieved by enabling a designator tool that mines a connected set of mountain tiles sharing the same roof type (unroofed, constructed, thin rock, overhead mountain, etc.). This feature is particularly useful for players aiming to create safe, expansive underground bases.

## Key Features and Systems

- **Roof-Type Tooltip:** A user-friendly tooltip that displays the current roof type above the selected tiles, providing clear visual information to guide the player's mining decisions.
  
- **Connected Roof Mining:** A designator tool that allows players to mine all connected mountain tiles that share the same roof type, streamlining the process of creating large underground spaces.

- **Integration and Compatibility:** This mod is based on and maintains compatibility with the original Veinminer mod, ensuring a seamless integration into the game.

- **Error Reporting and Support:** Guidelines for troubleshooting and community support are provided, including usage of the Log Uploader and Discord channel for error reporting.

## Coding Patterns and Conventions

- **C# Conventions:** Adheres to standard C# naming conventions with PascalCase for type names and methods (e.g., `DesignateSingleCell`, `Designator_Roofmine`), and camelCase for variables.

- **Modular Design:** Ensures that classes and methods are well-organized, with a focus on separation of concerns to maintain readability and ease of maintenance.

- **Code Documentation:** Inline comments are recommended to explain complex logic and enhance code readability. Ensure XML documentation comments are used for public-facing methods and classes where applicable.

## XML Integration

- **DesignationCategories.xml:** This XML file defines custom designation categories for integration within the game's user interface. The file should maintain proper structure and indentation for readability. Ensure all XML tags are correctly matched and attributes are properly defined to enable seamless integration with the game's UI.

## Harmony Patching

Harmony is used for modifying game behavior without altering original game code. When implementing Harmony patches:

- **Prefix/Suffix Patches:** Use prefix methods to execute code before the original method and suffix methods to run code after the original method. This ensures compatibility and the least interference with base game mechanics.

- **Error Handling:** Implement try-catch blocks in patches to handle exceptions gracefully and avoid crashing the game.

- **Patch Targeting:** Be precise in targeting the correct methods to patch. Use descriptive method signatures in your Harmony annotations to improve clarity and maintainability.

## Suggestions for Copilot

When using Copilot for your RimWorld modding tasks, consider the following suggestions:

- **Code Completion:** Leverage Copilot's ability to autocomplete common patterns and methods related to RimWorld mod development.

- **Predefined Snippets:** Create a set of predefined snippets for common tasks such as Harmony patches, XML file structure, and RimWorld-specific method conventions to expedite development.

- **Testing Automation:** Use Copilot to assist in writing unit and integration tests for your mod, ensuring robustness and reducing the likelihood of bugs.

- **Documentation Assistance:** Utilize Copilot to draft initial documentation for new features or methods, streamlining the process of maintaining descriptive and accurate project documentation.

By adhering to these guidelines, you can utilize GitHub Copilot effectively to enhance your mod development process, making Roofminer (Continued) a more robust and player-friendly enhancement for RimWorld.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

