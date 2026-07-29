---
name: no-unrequested-fallbacks
description: Mandatory skill and rule to prevent adding unrequested fallback values, default variables, or secondary fields in code unless explicitly requested by the user or confirmed beforehand.
---

# No Unrequested Fallbacks

## Core Rule
Do NOT insert unrequested fallback values, secondary field fallbacks, or default fallback logic (e.g. `fieldA || fieldB` or `value || "default"`) unless the user has explicitly requested it.

## Instructions
1. **Strict Field Usage**: Use strictly the fields and values specified in the user request.
2. **Clarify Before Adding Fallbacks**: If you think a fallback value is necessary or prudent (e.g., for safety, backwards compatibility, or edge cases), ASK the user for confirmation first before writing or offering the fallback logic.
3. **No Defensive Assumptions**: Do not assume secondary fields or default values should serve as fallbacks unless instructed.
