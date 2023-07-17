using System.Diagnostics.CodeAnalysis;
using SDL2;

namespace sdl2_snek_ai.utils;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class Utils
{
  public static SDL.SDL_Rect SDL_ImmutableRect(int x, int y, int w, int h, ref SDL.SDL_Rect r)
  {
    r.x = x;
    r.y = y;
    r.w = w;
    r.h = h;
    return r;
  }
}