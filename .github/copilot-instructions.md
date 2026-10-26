# GitHub Copilot Instructions for RimWorld Modding Project: Supreme Melee (Continued)

## Mod Overview and Purpose
**Supreme Melee (Continued)** is an update to the original **Supreme Melee** mod by ChJees. This mod aims to enhance the melee combat experience in RimWorld by introducing superhuman reflexes that allow melee-oriented pawns to block projectiles and parry melee attacks with their weapons. The mod also enhances non-augmented pawns, giving them a parry chance against projectiles and melee attacks based on their melee skill level.

## Key Features and Systems
### Parry Mechanics
- **Projectile Parry**: Pawns gain a parry chance against projectiles, starting at 10% for melee skill level 15 and increasing to 20% at level 20.
- **Melee Parry**: Similar to projectile parry, but specifically for melee attacks.
- **Dual Wielding**: Pawns with dual weapons have two chances to parry, but their overall parry effectiveness is slightly reduced.
- **Cybernetic Enhancements**: Some bionic implants and cybernetic enhancements benefit from the parry mechanics provided by this mod.

### XML Integration
The mod utilizes XML files to define new traits and stats that are essential for the parry mechanics. These XML files are integrated into the RimWorld game framework to ensure seamless functionality.

### Harmony Patching
Harmony patching is used extensively in the mod to modify the behavior of vanilla RimWorld methods. This allows the mod to inject custom logic for parry mechanics without altering the original game code.

## Coding Patterns and Conventions
### C# Files
- **HarmonyPatches.cs**: Contains the Harmony patches that modify game methods to implement parry mechanics.
- **MathUtils.cs**: Provides mathematical utilities, such as a two-way scaling method.
- **StatPart_DualWielding.cs**: Handles the dual-wielding parry mechanics, including explanation and value transformation.
- **SupremeMeleeModSettings.cs**: Manages mod settings, such as parry magnitudes.
- **SupremeMelee.cs**: Contains the main mod class, responsible for settings and category management.

### XML Files
- **Stats_Pawns_Combat.xml**: Defines new stats related to combat, including parry-related stats.
- **Traits_SupremeMelee.xml**: Defines new traits related to the Supreme Melee mod, such as increased reflexes.
- **Patch_CONN.xml**: Contains Harmony patches for specific RimWorld methods.

## Suggestions for Copilot
### XML Integration
When working with XML files, ensure that the new definitions (e.g., StatDef, TraitDef) are correctly formatted and integrated into the RimWorld game framework. Use the provided XML files as a reference for structure and naming conventions.

### Harmony Patching
When working with Harmony patches, ensure that the patches are correctly targeted and do not interfere with other mods or vanilla game functionality. Use the provided Harmony patches as a reference for best practices.

### Coding Patterns and Conventions
Follow the established coding patterns and conventions in the mod. This includes using the provided C# files as a reference for method naming, class structure, and overall code organization.

## Additional Information
- **Compatibility**: Ensure that the mod is compatible with other mods, especially those that affect combat mechanics.
- **Error Reporting**: If you encounter errors, use the Discord channel for error reporting and provide logs using the Log Uploader.
- **Solution Sharing**: If you find a solution to a problem, share it in the GitHub repository.

By following these instructions, you can effectively use GitHub Copilot to enhance and maintain the Supreme Melee (Continued) mod for RimWorld.

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

