# Kanim Explorer
[![Build Kanim Explorer](https://github.com/romen-h/kanim-explorer/actions/workflows/release-win-x64.yml/badge.svg?branch=main)](https://github.com/romen-h/kanim-explorer/actions/workflows/release-win-x64.yml)

A tool for viewing and modifying Oxygen Not Included's kanim files.

<img src="screenshot.png" height=400>

## Requirements to Build & Run

- **Visual Studio 2022**
- .NET 8.0 Runtime
- An OpenGL 3.3 capable GPU (for the Animation Viewer)

## Features

### Inspecting build.bytes and anim.bytes file content
- Supports opening any/all of the `.png`, `build.bytes`, and `anim.bytes` files for a kanim
- Outlines the section(s) of the texture atlas used for a specific object in the build.bytes file.
- Allows flags for Bloom, OnLight, SnapTo, and Foreground to be toggled for each sprite.  
  *Make those sprites glow!*
  
### Importing SCML Files
- Supports opening a Spriter `.scml` file directly to convert it to the equivalent Kanim.
- Supports bones and curves!

### Exporting SCML Files
Parts of [Kanimal-SE](https://github.com/skairunner/kanimal-SE) by skairunner have been integrated into Kanim Explorer.  
Any bugs with Kanim to SCML conversion should be reported as an issue here instead of on that repo.

### Animation Previews
Kanim Explorer implements an OpenGL based renderer that lets you actually play the animations and inspect them frame by frame.  
*No more loading the game to find your mistakes!*

### Wizards
Generate all sorts of useful files from a couple easy prompts:
- Placeholder graphics for buildings
- Packing a folder of sprites into a kanim build.bytes & texture atlas  
  *Just need to get a basic sprite into the game with 1-frame animations? This is very useful for Asteroids & Entities!*
- Custom tile textures

### And More!
- Split a texture atlas into individual sprite files.
- Resizing individual sprites in place & re-packing the texture atlas.  
  *You can make more space for editing the base game textures!*
- Save an empty anim.bytes file to stick in a kanim folder that doesn't have one.  
  *The game needs a file there to load, even if it has no animations.*
 
## Credits

- Thanks to loodakrawa for the C# [SpriterDotNet](https://github.com/loodakrawa/SpriterDotNet) library.  
SpriterDotNet Copyright (c) Luka "loodakrawa" Sverko (Zlib License)

- Thanks to skairunner for developing [kanimal-SE](https://github.com/skairunner/kanimal-SE), Kanim Explorer started as a simple UI to run that tool and grew into what it is now.
kanimal-se Copyright (c) 2019 Skye Im (MIT License)
