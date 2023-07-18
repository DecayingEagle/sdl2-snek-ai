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
    Direction = 0b00;
    MoveTime = 200; //in ms
    EventPollTime = 10; //in ms
  }
    
  public int FieldWidth { get; }
  public int FieldHeight { get; }
  public Random Rng { get; set; }
  public Vector2i InitSnakePos { get; set; }
  private Snake snake { get; set; }
  private Apple apple { get; set; }
  public BoardTile[] GameBoard { get; set; } //gives type of tile in binary
  
  public byte Direction { get; set; }
  
  private UInt32 GameTicks { get; set; }
  public uint MoveTime { get; set; }
  public uint EventPollTime { get; set; }
  public Vector2i[] SnakeBody { get; set; }
  
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
    int x = snake.Pos.X;
    int y = snake.Pos.Y;
    switch (Direction)
    {
      case 0b00:
        x--;
        break;
      case 0b01:
        y--;
        break;
      case 0b10:
        y++;
        break;
      case 0b11:
        x++;
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
    snake.UpdatePos(new Vector2i(x, y), snake.Pos, apple, snake.BodyStack);
    snake.Pos = new Vector2i(x, y);
    
    
    if (Vector2i.Equals(apple.Pos, snake.Pos))
    {
      apple.GenNewPos();
    }

    UpdateBoard();
  }

  public void Draw(IntPtr renderer)
  {
    SDL.SDL_SetRenderDrawColor( renderer, 0x00, 0x00, 0x00, 0xff );
    SDL.SDL_RenderClear(renderer);
    DrawBoard(renderer);
    SDL.SDL_RenderPresent(renderer);
  }

  private void DrawBoard(IntPtr renderer)
  {
    SDL.SDL_Rect fillRect = new SDL.SDL_Rect();
      
    for (int j = 0; j < Program.GameFieldHeight; j++) //columns
    {
      for (int i = 0; i < Program.GameFieldWidth; i++) //rows
      {
        switch (GameBoard[i+j*Program.GameFieldWidth])
        {
          case BoardTile.WALL:
            Utils.SDL_ImmutableRect(5+(Program.TileSize+Program.MarginSize)*i,
              5+(Program.TileSize+Program.MarginSize)*j,
              Program.TileSize,
              Program.TileSize,
              ref fillRect);
            SDL.SDL_SetRenderDrawColor( renderer, 0xff, 0x00, 0x00, 0xff );
            SDL.SDL_RenderFillRect(renderer, ref fillRect);
            break;
          case BoardTile.SNAKE:
            Utils.SDL_ImmutableRect(5+(Program.TileSize+Program.MarginSize)*i,
              5+(Program.TileSize+Program.MarginSize)*j,
              Program.TileSize,
              Program.TileSize,
              ref fillRect);
            SDL.SDL_SetRenderDrawColor( renderer, 0xff, 0xff, 0xff, 0xff );
            SDL.SDL_RenderFillRect(renderer, ref fillRect);
            break;
          case BoardTile.APPLE:
            Utils.SDL_ImmutableRect(5+(Program.TileSize+Program.MarginSize)*i,
              5+(Program.TileSize+Program.MarginSize)*j,
              Program.TileSize,
              Program.TileSize,
              ref fillRect);
            SDL.SDL_SetRenderDrawColor( renderer, 0x00, 0xff, 0x00, 0xff );
            SDL.SDL_RenderFillRect(renderer, ref fillRect);
            break;
        }
      }
    }
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
    renderer = SDL.SDL_CreateRenderer(window,-1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED^SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
      
    if (renderer == IntPtr.Zero)
    {
      Console.WriteLine( "Renderer could not be created! SDL Error: %s\n", SDL.SDL_GetError() );
    }
    else
    {
      InitDraw(renderer);
    }
    Draw(renderer);
    
    uint localLastTimeUpdated = 0;
    uint localLastTimeEventsPolled = 0;
    bool quit = false;
    
    while (!quit)
    {
      GameTicks = SDL.SDL_GetTicks();
      if (GameTicks - localLastTimeEventsPolled <= EventPollTime) continue;
      while (SDL.SDL_PollEvent(out e) != 0 && e.type != SDL.SDL_EventType.SDL_MOUSEMOTION && e.type is SDL.SDL_EventType.SDL_QUIT or SDL.SDL_EventType.SDL_KEYDOWN)
      {
        #region MAINOPERATIONHANDLING
        switch (e.type)
        {
          case SDL.SDL_EventType.SDL_QUIT:
            quit = true;
            break;
          case SDL.SDL_EventType.SDL_KEYDOWN:
            DoKeyDown(e.key);
            break;
        }
        #endregion MAINOPERATIONHANDLING
      }
      
      if (GameTicks - localLastTimeUpdated <= MoveTime) continue;
      Update(renderer);
      Draw(renderer);
      Console.WriteLine(string.Join(", ", snake.BodyStack.ToArray()));
      localLastTimeUpdated = SDL.SDL_GetTicks();
    }
    SDL.SDL_DestroyRenderer(renderer);
    SDL.SDL_DestroyWindow(window);
    SDL.SDL_Quit();
  }

  public void DoKeyDown(SDL.SDL_KeyboardEvent e)
  {
    if (e.repeat == 0)
    {
      switch (e.keysym.scancode)
      {
        case SDL.SDL_Scancode.SDL_SCANCODE_LEFT:
          Direction = 0b00;
          break;
        case SDL.SDL_Scancode.SDL_SCANCODE_UP:
          Direction = 0b01;
          break;
        case SDL.SDL_Scancode.SDL_SCANCODE_DOWN:
          Direction = 0b10;
          break;
        case SDL.SDL_Scancode.SDL_SCANCODE_RIGHT:
          Direction = 0b11;
          break;
      }
    }
  }

  public void InitDraw(IntPtr renderer)
  {
    UpdateBoard();
    DrawBoard(renderer);
  }

  private void UpdateBoard()
  {
    for (int j = 0; j < Program.GameFieldHeight; j++) //columns
    {
      for (int i = 0; i < Program.GameFieldWidth; i++) //rows
      {
        if (i == 0 || i == Program.GameFieldWidth - 1 || j == 0 || j == Program.GameFieldHeight - 1)
        {
          GameBoard[i+j*Program.GameFieldWidth] = BoardTile.WALL;
        } 
        else if (i == snake.Pos.X && j == snake.Pos.Y)
        {
          GameBoard[i+j*Program.GameFieldWidth] = BoardTile.SNAKE;
        } 
        else if (i == apple.Pos.X && j == apple.Pos.Y)
        {
          GameBoard[i+j*Program.GameFieldWidth] = BoardTile.APPLE;
        }
        else
        {
          GameBoard[j * Program.GameFieldWidth + i] = BoardTile.NOTHING;
        }

        foreach (var part in snake.BodyStack)
        {
          GameBoard[part.X + part.Y * Program.GameFieldWidth] = BoardTile.SNAKE;
        }
      }
    }
  }
}