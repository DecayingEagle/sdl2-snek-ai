using System.Numerics;
using sdl2_snek_ai.utils;

namespace sdl2_snek_ai.game;


public class Snake
{
  public Snake(Vector2i pos)
  {
    Pos = pos;
    BodyStack = new Stack<Vector2i>();
  }

  public Vector2i Pos { get; set; }
  public Stack<Vector2i> BodyStack { get; set; }
  public int Lenght { get; set; }

  // ReSharper disable once InconsistentNaming
  public void UpdatePos(Vector2i snakeHead, Vector2i oldSnakeHead, Apple apple, Stack<Vector2i> stack)
  {
    // need to implement a Last in First out
  }
}
