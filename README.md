# Kanim Explorer
A tool for viewing and modifying Oxygen Not Included's kanim files.

<img src="screenshot.png" height=400>

## Features

### Inspecting build.bytes and anim.bytes file content
- Supports opening any/all of the `.png`, `build.bytes`, and `anim.bytes` files for a kanim
- Outlines the section(s) of the texture atlas used for a specific object in the build.bytes file.
- Allows flags for Bloom, OnLight, SnapTo, and Foreground to be toggled for each sprite.  
  *Make those sprites glow!*

### Animation Previews
Kanim Explorer implements an OpenGL based renderer that lets you actually play the animations and inspect them frame by frame.  
*No more loading the game to find your mistakes!*

### Wizards
Generate all sorts of useful files from a couple easy prompts:
- Placeholder graphics for buildings
- Packing a folder of sprites into a kanim build.bytes & texture atlas  
  *Just need to get a basic sprite into the game with 1-frame animations? This is very useful for Asteroids & Entities!*
- Custom tile textures

### Kanimal-SE Integration
Converting back and forth between kanim and scml is now easier with a GUI.  
You must download kanimal-cli.exe separately.

### And More!
- Split a texture atlas into individual sprite files.
- Resizing individual sprites in place & re-packing the texture atlas.  
  *You can make more space for editing the base game textures!*
- Save an empty anim.bytes file to stick in a kanim folder that doesn't have one.  
  *The game needs a file there to load, even if it has no animations.*
