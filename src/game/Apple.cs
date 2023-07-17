using System.Numerics;
using System.Security.Cryptography;
using sdl2_snek_ai.utils;

namespace sdl2_snek_ai.game;

public class Apple
{
  public Apple(Random rng)
  {
    Rng = rng;
    Pos = new Vector2i(Rng.Next(1, Program.GameFieldWidth), Rng.Next(1, Program.GameFieldHeight));
  }
  public Vector2i Pos { get; set; }

  private Random Rng { get; set; }

  public void IfEaten()
  {
    Pos = new Vector2i(Rng.Next(1, Program.GameFieldWidth), Rng.Next(1, Program.GameFieldHeight));
  }
}