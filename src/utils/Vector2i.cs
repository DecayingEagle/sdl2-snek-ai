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
    if ((vec1.X == vec2.X) && (vec1.Y == vec2.Y))
    {
      return true;
    }

    return false;
  }
}