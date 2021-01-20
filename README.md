# kanim-explorer
A tool for viewing and modifying Oxygen Not Included's kanim files.

<img src="screenshot.png" height=400>

## Features

- Supports opening a set of `.png`, `build.bytes`, and `anim.bytes` files to view their contents.
- Outlines the section of the texture atlas used for a specific sprite in the animation.
- Split the texture atlas into individual image files.
- Allows flags for Bloom, "OnLight", "SnapTo", and Foreground to be toggled for each sprite.
- Integration with kanimal-se to convert the kanim files to Spriter SCML projects.
- Generating empty anim.bytes files to go with kanims that only use build.bytes. (So that the game doesn't crash when loading.)
