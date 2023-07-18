using System.Diagnostics;
using System.Numerics;
using System.Xml;
using SDL2;
using sdl2_snek_ai.game;
using sdl2_snek_ai.utils;

namespace sdl2_snek_ai;

static class Program
{

  public const int WindowWidth = 1000;
  public const int WindowHeight = 800;
  public const int GameFieldWidth = 30;
  public const int GameFieldHeight = 20;
  public const int TileSize = 32;
  public const int MarginSize = 1;
  public static readonly Vector2i SnakeInitPos = new Vector2i(6, 4);
  
  // Game colors in rgba hex
  /* unused colors for now
  public const UInt32 SnakeBodyColor = 0xffffffff; // White
  public const UInt32 WallsColor = 0xff0000ff; // Red
  public const UInt32 AppleColor = 0x00ff00ff; // Green
  */

  static void Main(string[] args)
  {
    Game game = new Game(GameFieldWidth,GameFieldHeight,SnakeInitPos);
    game.Init();
  }

}