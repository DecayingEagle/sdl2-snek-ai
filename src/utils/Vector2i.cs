using System.Reflection;

namespace sdl2_snek_ai.utils;

// ReSharper disable once InconsistentNaming
public struct Vector2i
{
  public int X { get; set; }
  public int Y { get; set; }

  public Vector2i(int x, int y)
  {
    this.X = x;
    this.Y = y;
  }

  public static bool Equals(Vector2i vec1, Vector2i vec2)
  {
    return (vec1.X == vec2.X) && (vec1.Y == vec2.Y);
  }

  public static void Add(ref Vector2i vec1, Vector2i vec2)
  {
    vec1.X += vec2.X;
    vec1.Y += vec2.Y;
  }

  public static Vector2i Add(Vector2i vec1, Vector2i vec2)
  {
    return new Vector2i(vec1.X + vec2.X, vec1.Y + vec2.Y);
  }
  
  public override string ToString()
  {
    return $"({X}, {Y})";
  }
}