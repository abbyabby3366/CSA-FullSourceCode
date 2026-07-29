# Project Agent Guidelines

## No Unrequested Fallbacks Rule
- Do NOT insert unrequested fallback values, secondary field fallbacks, or default fallback logic (e.g. `fieldA || fieldB` or `value || "default"`) unless explicitly specified by the user.
- If you think a fallback value is necessary or beneficial, ALWAYS ask the user for permission before adding it.

## File Length Limit
- **Max 600 Lines Per File**: Pre-existing files that already exceed 600 lines do not need to be refactored. However, your own additions/edits must not push any file over 600 lines. If your edits push a file over 600 lines, refactor your added logic into modular helper files, sub-components, or services.
