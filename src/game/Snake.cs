using System.Net;
using System.Numerics;
using sdl2_snek_ai.utils;

namespace sdl2_snek_ai.game;


public class Snake
{
  public Snake(Vector2i pos)
  {
    Pos = pos;
    BodyQueue = new Queue<Vector2i>();
    BodyQueue.Enqueue(pos);
  }

  public Vector2i Pos { get; set; }
  public Queue<Vector2i> BodyQueue { get; set; }
  public int Lenght { get; set; }

  // ReSharper disable once InconsistentNaming
  public void UpdatePos(Vector2i snakeHead, Apple apple, Queue<Vector2i> queue)
  {
    Vector2i dummyVar;
    queue.Enqueue(snakeHead);
    if (!apple.Eaten)
    {
      queue.TryDequeue(out dummyVar);
      dummyVar = Vector2i.Zero();
    }
    else
    {
      apple.Eaten = false;
    }
  }
}
