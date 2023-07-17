using System.Numerics;

namespace sdl2_snek_ai.src.game;


public class Snake
{
  public Snake(Vector2 pos)
  {
    Pos = pos;
    BodyStack = new Stack<Vector2>();
    BodyStack.Push(pos);
  }

  public Vector2 Pos { get; set; }
  public Stack<Vector2> BodyStack { get; set; }
  public int Lenght { get; set; }
}
