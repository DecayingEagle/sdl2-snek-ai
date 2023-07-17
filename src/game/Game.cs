using System.Numerics;
using SDL2;
using sdl2_snek_ai.src.utils;

namespace sdl2_snek_ai.src.game;

public class Game
{
  public Game(int w, int h, Vector2i initPos)
  {
    Width = w;
    Height = h;
    Rng = new Random();
    InitSnakePos = initPos;
  }
  public SDL.SDL_Event e;
    
  public int Width { get; }
  public int Height { get; }
  public Random Rng { get; set; }
  public Vector2i InitSnakePos { get; set; }

  public void Init()
  {
    SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

    // ReSharper disable once RedundantAssignment
    IntPtr window;
    window = SDL.SDL_CreateWindow("SnakeAI GUI",
      SDL.SDL_WINDOWPOS_CENTERED,
      SDL.SDL_WINDOWPOS_CENTERED,
      1000,
      800,
      SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
      
    bool quit = false;
    while (!quit)
    {
      while (SDL.SDL_PollEvent(out e) != 0)
      {
        #region MAINOPERATIONHANDLING
        switch (e.type)
        {
         case SDL.SDL_EventType.SDL_QUIT:
           quit = true;
           break;
        }
        #endregion MAINOPERATIONHANDLING
        Update();
      }
    }
    
    SDL.SDL_DestroyWindow(window);
    SDL.SDL_Quit();
  }

  public void Update()
    {
        // TODO: Not implemented anything yet
    }
}