using System.Numerics;

namespace sdl2_snek_ai.src.game;

public class Apple
{
  public Apple(Vector2 pos)
  {
    Pos = pos;
  }
  public Vector2 Pos { get; set; }
}