using System.Numerics;
using sdl2_snek_ai;
using SDL2;
using sdl2_snek_ai.utils;

namespace sdl2_snek_ai.game;

public class Game
{
  public Game(int w, int h, Vector2i initPos)
  {
    FieldWidth = w;
    FieldHeight = h;
    Rng = new Random();
    InitSnakePos = initPos;
    snake = new Snake(InitSnakePos);
    apple = new Apple(Rng);
    GameBoard = new BoardTile[Program.GameFieldWidth * Program.GameFieldHeight];
  }
    
  public int FieldWidth { get; }
  public int FieldHeight { get; }
  public Random Rng { get; set; }
  public Vector2i InitSnakePos { get; set; }
  private Snake snake { get; set; }
  private Apple apple { get; set; }
  public BoardTile[] GameBoard { get; set; } //gives type of tile in binary
  
  public enum BoardTile
  {
    NOTHING = 0b00,
    SNAKE = 0b01,
    APPLE = 0b10,
    WALL = 0b11
  }
  
  public SDL.SDL_Event e;
    
  public void Update(IntPtr renderer)
  {
    DrawBoard(renderer);
  }

  public void Draw(IntPtr renderer)
  {
    SDL.SDL_SetRenderDrawColor( renderer, 0x00, 0x00, 0x00, 0xff );
    SDL.SDL_RenderClear(renderer);
  }

  private void DrawBoard(IntPtr renderer)
  {
    SDL.SDL_SetRenderDrawColor( renderer, 0x00, 0x00, 0x00, 0xff );
    SDL.SDL_RenderClear(renderer);

    SDL.SDL_Rect fillRect = new SDL.SDL_Rect();
    fillRect.x = 0;
    fillRect.y = 0;
    fillRect.w = 32;
    fillRect.h = 32;
    SDL.SDL_SetRenderDrawColor( renderer, 0xff, 0x00, 0x00, 0xff );
    SDL.SDL_RenderFillRect(renderer, ref fillRect);
    
      
    for (int i = 0; i < GameBoard.Length; i++)
    {
      Utils.SDL_ImmutableRect(0+33*i, 0, 32, 32, ref fillRect);
      SDL.SDL_SetRenderDrawColor( renderer, 0x50, 0x00, 0x00, 0xff );
      SDL.SDL_RenderFillRect(renderer, ref fillRect);
    }
    SDL.SDL_RenderPresent(renderer);
  }
  
  public void Init()
  {
    SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
    
    // ReSharper disable once RedundantAssignment
    IntPtr window = IntPtr.Zero;
    window = SDL.SDL_CreateWindow("SnakeAI GUI",
      SDL.SDL_WINDOWPOS_CENTERED,
      SDL.SDL_WINDOWPOS_CENTERED,
      Program.WindowWidth,
      Program.WindowHeight,
      SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
      
    if (window == IntPtr.Zero)
    {
      Console.WriteLine("Window could not be created! SDL Error: %s\n", SDL.SDL_GetError());
      return;
    }

    IntPtr renderer = IntPtr.Zero;
    renderer = SDL.SDL_CreateRenderer(window,-1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
      
    if (renderer == IntPtr.Zero)
    {
      Console.WriteLine( "Renderer could not be created! SDL Error: %s\n", SDL.SDL_GetError() );
    }
    else
    {
      InitDraw(renderer);
    }
      
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
        Update(renderer);
        Draw(renderer);
      }
    }
    SDL.SDL_DestroyRenderer(renderer);
    SDL.SDL_DestroyWindow(window);
    SDL.SDL_Quit();
  }
  
  public void InitDraw(IntPtr renderer)
  {
    // TODO: Need a function for placing walls and printing current state
    InitBoard();
    DrawBoard(renderer);
  }

  private void InitBoard()
  {
    for (int j = 0; j < Program.GameFieldHeight; j++) //columns
    {
      for (int i = 0; i < Program.GameFieldWidth; i++) //rows
      {
        if (!(i == 0 || i == Program.GameFieldWidth - 1 || j == 0 || j == Program.GameFieldHeight - 1))
        {
          GameBoard[j * Program.GameFieldWidth + i] = BoardTile.NOTHING;
        } 
        else if (i == snake.Pos.X && j == snake.Pos.X)
        {
          GameBoard[j * Program.GameFieldWidth + i] = BoardTile.SNAKE;
        } 
        else if (i == apple.Pos.X && j == apple.Pos.X)
        {
          GameBoard[j * Program.GameFieldWidth + i] = BoardTile.APPLE;
        }
        else
        {
          GameBoard[j * Program.GameFieldWidth + i] = BoardTile.WALL;
        }
      }
    }
  }
}