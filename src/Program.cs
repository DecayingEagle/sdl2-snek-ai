using System.Diagnostics;
using System.Numerics;
using System.Xml;
using SDL2;
using sdl2_snek_ai.src.game;
using sdl2_snek_ai.src.utils;

namespace sdl2_snek_ai.src;

static class Program
{
  public const int Width = 12;
  public const int Height = 8;
  public static readonly Vector2i SnakeInitPos = new Vector2i(6, 4);

  static void Main(string[] args)
  {
    Game game = new Game(Width,Height,SnakeInitPos);
    game.Init();
  }

}