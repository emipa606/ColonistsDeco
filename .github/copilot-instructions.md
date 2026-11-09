# GitHub Copilot Instructions for Colonists' Deco (Continued)

## Overview and Purpose
Colonists' Deco (Continued) is a RimWorld mod that enhances the gameplay experience by allowing colonists to decorate their bedrooms with various decorations. This mod leverages the improved support for Asset Bundles in RimWorld 1.6 to maintain a smaller footprint while adding numerous features. The primary goal is to increase the construction skill of colonists through the act of decorating and to provide customization options for room-specific and general decorations.

## Key Features and Systems
- **Colonist Decoration**: Colonists will decorate their bedrooms with various items, which will level up their construction skill.
- **Decoration Levels**: Players can research different decoration levels, each reflecting a different tech level.
- **Room-Specific Decorations**: Decorations that are specific to certain rooms, enhancing the thematic feel of those rooms.
- **Customizable Decorations**: Players can customize decorations to their liking.
- **Random Poster Images**: A variety of random poster images to add visual interest.
- **Placeable Items**: Additional items that can be placed around the colony.
- **UI Button**: A button to preview decorations in full size.

## Coding Patterns and Conventions
The mod follows a structured approach to coding, adhering to common practices in RimWorld modding:

- **C# Files**: The C# files are organized into specific classes and components to handle different aspects of decoration functionality. For example, `CompPawnDeco` handles the logic for colonist decoration, while `CompProperties_AttachableThing` manages the properties of attachable things.
- **XML Integration**: XML files define the game entities and their properties. Each XML file corresponds to a specific type of definition, such as `JobDef`, `ThingDef`, or `JoyDef`.
- **Harmony Patching**: The mod uses Harmony patching to modify existing game behavior without altering the original RimWorld codebase.

## XML Integration
XML files play a crucial role in defining the entities and behaviors within the mod. Here are some examples of XML files and their purposes:

- `JobDefs/JobDef.xml`: Defines job definitions that are used for colonist decoration tasks.
- `ThingDefs/Buildings/DECO_BedsideBottle.xml`: Defines a specific decoration item, such as a bedside bottle.
- `Joy/JoyDefs/JoyDef.xml`: Defines joy definitions and joy giver definitions that are associated with decorations.

## Harmony Patching
Harmony patching is a powerful tool used in RimWorld modding to modify game behavior. The mod uses Harmony patches to:

- **Modify Existing Behavior**: Change how certain game behaviors work, such as the way colonists interact with decorations.
- **Add New Functionality**: Introduce new features, such as the ability to preview decorations in full size.

## Suggestions for Copilot
To effectively assist with Colonists' Deco (Continued), GitHub Copilot should:

- **Understand XML Definitions**: Be familiar with the various XML definitions used in the mod to ensure that new definitions are correctly formatted.
- **Code C# Components**: Generate C# code for new components and classes that extend the functionality of the mod, such as new decoration systems or UI elements.
- **Harmony Patch Snippets**: Provide snippets for Harmony patches that modify existing game behavior to align with the mod's features.
- **UI Elements**: Help generate code for new UI elements, such as buttons or panels, that enhance the player experience.

By following these guidelines, GitHub Copilot can provide valuable assistance in developing and maintaining Colonists' Deco (Continued).

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

