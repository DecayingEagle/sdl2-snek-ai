using System.Diagnostics;
using System.Numerics;
using System.Xml;
using SDL2;
using sdl2_snek_ai.src.game;
using sdl2_snek_ai.src.utils;

namespace sdl2_snek_ai;

static class Program
{

  public const int WindowWidth = 1000;
  public const int WindowHeight = 800;
  public const int GameFieldWidth = 12;
  public const int GameFieldHeight = 8;
  public const int TileSize = 60;
  public const int MarginSize = 5;
  public static readonly Vector2i SnakeInitPos = new Vector2i(6, 4);
  
  // Game colors in rgba hex
  public const UInt32 SnakeBodyColor = 0xffffffff; // White
  public const UInt32 WallsColor = 0xff0000ff; // Red
  public const UInt32 AppleColor = 0x00ff00ff; // Green

  static void Main(string[] args)
  {
    Game game = new Game(GameFieldWidth,GameFieldHeight,SnakeInitPos);
    game.Init();
  }

}