# Woven Nest Layer Pass 01

Purpose: replace the flat Woven Nest background/tilemap read with parallax-ready background and prop layers generated through `sprite-gen gen`.

Source reference:
- `src/Miji/Assets/Art/Environment/Backgrounds/BG_WovenNest.png`

Canvas:
- 688x384 px
- Unity PPU 32
- Side-view 2D metroidvania room background

Layer contract:
- `far_fog` is an opaque full-canvas backdrop.
- All other layers are transparent PNGs generated on magenta chroma and cut to alpha by sprite-gen.
- Layers are visual only. Collision remains on tilemaps or invisible physics shapes.

