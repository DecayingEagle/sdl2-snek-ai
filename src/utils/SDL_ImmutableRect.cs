using SDL2;

namespace sdl2_snek_ai.src.utils;

public class SDL_ImmutableRect
{
  public SDL_ImmutableRect(int x, int y, int w, int h)
  {
    // TODO: make this kinda SDL_Rect type idk
    this.x = x;
    this.y = y;
    this.w = w;
    this.h = h;
  }

  public int x { get; set; }
  public int y { get; set; }
  public int w { get; set; }
  public int h { get; set; }
}