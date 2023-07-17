using System.Numerics;
using sdl2_snek_ai.utils;

namespace sdl2_snek_ai.game;


public class Snake
{
  public Snake(Vector2i pos)
  {
    Pos = pos;
    BodyStack = new Stack<Vector2i>();
    BodyStack.Push(pos);
  }

  public Vector2i Pos { get; set; }
  public Stack<Vector2i> BodyStack { get; set; }
  public int Lenght { get; set; }

  public void UpdatePos()
  {
    // add updating position script that also goes through the stack and pulls last and pushes new unless an apple is eaten
  }
}
