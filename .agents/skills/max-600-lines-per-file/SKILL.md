---
name: max-600-lines-per-file
description: Limit file growth to a maximum of 600 lines. Pre-existing files that already exceed 600 lines do not require refactoring, but edits added by the agent must not push a file over 600 lines.
---

# Max 600 Lines Per File

## Core Rule
Do NOT allow your own code edits or newly created files to push a file over 600 lines.

## Guidelines & Scope
1. **Pre-Existing Files Exceeding 600 Lines**: If an original file already exceeds 600 lines prior to your edits, you do NOT need to refactor or rewrite the pre-existing file content to bring it under 600 lines.
2. **Agent Responsibility**: If your modifications cause a file to exceed 600 lines, you MUST refactor your additions into modular helper files, sub-components, or separate services to keep the file compliant.
3. **New Files**: All new files created by the agent must strictly stay under 600 lines.
