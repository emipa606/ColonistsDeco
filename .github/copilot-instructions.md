# GitHub Copilot Instructions for Colonists' Deco (Continued) Mod

## Mod Overview and Purpose
**Mod Name:** Colonists' Deco (Continued)

**Description:** Colonists' Deco is a mod that enriches the RimWorld experience by allowing colonists to use various decorative items to embellish their living spaces, particularly bedrooms. As colonists engage in decoration, they enhance their construction skills. The mod provides a deeper level of immersion and customization, allowing players to introduce a personal touch to colonist rooms.

**Important Note:** The mod relies exclusively on Asset Bundles to minimize its size after RimWorld's 1.6 update. It offers compatibility with end-tables without pre-existing lamps.

## Key Features and Systems
- **Decorative Actions:** Colonists decorate their bedrooms, boosting their construction skill.
- **Researchable Levels:** Decorations can be unlocked through research, corresponding to different technological levels.
- **Room-Specific Decorations:** Customize versus generic room enhancements.
- **Customizable Decorations:** Players can select and modify decorative items.
- **Random Poster Images:** Dynamically generate poster content.
- **Enhanced Item Selection:** Greater variety of placeable items.
- **Preview UI:** A button feature to visualize decoration in full size.
- **Camera Compatibility:** For optimal decorative viewing, the Camera+ mod is recommended.

## Coding Patterns and Conventions
- **Class Access Modifiers:** Use of the `internal` modifier for classes within the mod, except when public exposure is necessary, such as interaction with the game’s core systems (`public class`).
- **Methods Organization:** Grouped by functional components within respective classes, following private methods (`private void methodName()`) for internal logic and public methods (`public void methodName()`) for broader access.
- **Naming Conventions:** Adheres to CamelCase for method names and PascalCase for class names, ensuring clarity and consistency.

## XML Integration
RimWorld mods often require XML files to define game objects, settings, and configurations. For Colonists' Deco:
- Utilize XML to define new decorative items, research projects, and integration with existing RimWorld assets.
- Ensure XML files are placed in appropriate folders like `Defs`, following RimWorld's conventions.

## Harmony Patching
**Harmony:** A library for patching methods at runtime, which Colonists' Deco uses to integrate seamlessly with RimWorld’s systems.
- **HarmonyPatches.cs:** Contains static classes for patching methods pertinent to decorative logic and beauty recalculation (`public static class HarmonyPatches`).
- Use Harmony annotations to target methods for prefixes, postfixes, or transpilers to modify behavior without altering original codebases directly.

## Suggestions for Copilot
- **Automation of Repetitive Tasks:** Utilize Copilot to generate boilerplate code for new decoration definitions or research tech levels.
- **Harmony Patches:** Leverage Copilot to draft new Harmony patches by referencing existing documentation and examples automatically.
- **Error Handling:** Implement comprehensive error checks within methods, as suggested by Copilot, to prevent runtime crashes.
- **Enhanced User Interaction:** Integrate Copilot to suggest optimization for UI components, enhancing the mod's user interface experiences.
- **Method Optimization:** Use Copilot to propose code improvement opportunities or refactor functions for better performance and maintainability.

This instruction document should guide developers in maintaining the Colonists' Deco mod efficiently while leveraging GitHub Copilot's capabilities to streamline development tasks.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.
