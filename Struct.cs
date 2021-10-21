using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace LearningGame
{
    public enum SpriteAnimation
    {
        WalkRight = 0, Stand = 1, WalkLeft = 2
    }
    public enum SpriteDirection
    {
        MoveDown = 0, MoveLeft = 1, MoveRight = 2, MoveUp = 3
    }
    public class SpriteCharacter
    {
        public Texture2D SpriteSource;
        public Rectangle SourceRectangle;
        public Rectangle DestinationRectangle;
        public Rectangle CollisionRectangle;
        public Vector2 SpritePosition;
        public SpriteAnimation Animation;
        public SpriteDirection Direction;
        public int SpriteCols;
        public int SpriteRows;
        public int OriginX;
        public int OriginY;
        public Point CurrentPosition;
        public Point TopOrigin
        {
            get
            {
                return new Point((CollisionRectangle.X+CollisionRectangle.Width)/2, CollisionRectangle.Y);
            }
        }
        public Point RightOrigin
        {
            get
            {
                return new Point(CollisionRectangle.X + CollisionRectangle.Width, (CollisionRectangle.Y + CollisionRectangle.Height)/2);
            }
        }
        public Point LeftOrigin
        {
            get
            {
                return new Point(CollisionRectangle.X, (CollisionRectangle.Y + CollisionRectangle.Height) / 2);
            }
        }
        public Point BottomOrigin
        {
            get
            {
                return new Point((CollisionRectangle.X + CollisionRectangle.Width)/2, CollisionRectangle.Y + CollisionRectangle.Height);
            }
        }
        public int WalkSpeed;
        public int RunSpeed;
        public int AnimationSpeed;
        public int Width;
        public int Height;
        public bool IsMove;
        public bool IsAnimated;
        public bool IsPlayer;
        public bool OnCollide;
        public void ChangeAnimation()
        {
            switch (this.Animation)
            {
                case SpriteAnimation.Stand:
                    this.Animation = SpriteAnimation.WalkLeft;
                    break;
                case SpriteAnimation.WalkLeft:
                    this.Animation = SpriteAnimation.WalkRight;
                    break;
                case SpriteAnimation.WalkRight:
                    this.Animation = SpriteAnimation.Stand;
                    break;
            }
        }
        public void ChangeDirection()
        {
            switch (this.Direction)
            {
                case SpriteDirection.MoveUp:
                    this.Direction = SpriteDirection.MoveRight;
                    break;
                case SpriteDirection.MoveRight:
                    this.Direction = SpriteDirection.MoveDown;
                    break;
                case SpriteDirection.MoveDown:
                    this.Direction = SpriteDirection.MoveLeft;
                    break;
                case SpriteDirection.MoveLeft:
                    this.Direction = SpriteDirection.MoveUp;
                    break;
            }
        }
        public void MoveUp()
        {
            this.IsMove = true;
            if (OnCollide) return;
            this.Direction = SpriteDirection.MoveUp;            
            this.SpritePosition.Y -= this.WalkSpeed;
        }
        public void MoveDown()
        {
            this.IsMove = true;
            if (OnCollide) return;
            this.Direction = SpriteDirection.MoveDown;            
            this.SpritePosition.Y += this.WalkSpeed;
        }
        public void MoveLeft()
        {
            this.IsMove = true;
            if (OnCollide) return;
            this.Direction = SpriteDirection.MoveLeft;            
            this.SpritePosition.X -= this.WalkSpeed;
        }
        public void MoveRight()
        {
            this.IsMove = true;
            if (OnCollide) return;
            this.Direction = SpriteDirection.MoveRight;
            this.SpritePosition.X += this.WalkSpeed;
        }
        public void Run()
        {
            this.WalkSpeed += this.RunSpeed;
        }
        public void Update(int timeFrame)
        {
            if (this.IsMove)
            {
                this.ChangeAnimation();
                this.IsMove = false;
            }
            else
            {
                if (this.IsAnimated)
                {
                    if (timeFrame % AnimationSpeed == 0)
                    {
                        this.ChangeAnimation();
                    }
                }
                else
                {
                    this.Animation = SpriteAnimation.Stand;
                }
            }

            int spriteWidth = (int)(this.SpriteSource.Width / this.SpriteCols);
            int spriteHeight = (int)(this.SpriteSource.Height / this.SpriteRows);

            if (Width == 0) Width = spriteWidth;
            if (Height == 0) Height = spriteHeight;

            int posX = spriteWidth * (int)this.Animation;
            int posY = spriteHeight * (int)this.Direction;

            this.SourceRectangle = new Rectangle(
                this.OriginX + posX, this.OriginY + posY,
                spriteWidth, spriteHeight
                );
        }
        public void Draw(Viewport vp, SpriteBatch spriteBatch, Vector2 position, int limitLeft = 0, int limitRight = 0, int limitUp = 0, int limitDown = 0)
        {
            int spaceWidth = (int)(vp.Width - Width);
            int spaceHeight = (int)(vp.Height - Height);

            int calPositionX = (int)position.X + (int)this.SpritePosition.X;
            int calPositionY = (int)position.Y + (int)this.SpritePosition.Y;

            CurrentPosition.X = MathHelper.Clamp(calPositionX, limitLeft, spaceWidth - limitRight);
            CurrentPosition.Y = MathHelper.Clamp(calPositionY, limitUp, spaceHeight - limitDown);

            this.DestinationRectangle = new Rectangle(
                CurrentPosition.X, CurrentPosition.Y,
                Width,
                Height
                );

            spriteBatch.Draw(this.SpriteSource, this.DestinationRectangle, this.SourceRectangle, Color.White);
        }

        public void DrawCenter(Viewport vp, SpriteBatch spriteBatch, int limitLeft = 0, int limitRight = 0, int limitUp = 0, int limitDown = 0)
        {
            int spaceWidth = (int)(vp.Width - Width);
            int spaceHeight = (int)(vp.Height - Height);

            int calPositionX = (spaceWidth / 2) + (int)this.SpritePosition.X;
            int calPositionY = (spaceHeight / 2) + (int)this.SpritePosition.Y;

            CurrentPosition.X = MathHelper.Clamp(calPositionX, limitLeft, spaceWidth - limitRight);
            CurrentPosition.Y = MathHelper.Clamp(calPositionY, limitUp, spaceHeight - limitDown);

            DestinationRectangle = new Rectangle(
                CurrentPosition.X, CurrentPosition.Y,
                Width,
                Height
                );

            spriteBatch.Draw(this.SpriteSource, DestinationRectangle, this.SourceRectangle, Color.White);
        }
        public bool IsCollide(SpriteCharacter compareTo)
        {
            bool isCollide= compareTo.CollisionRectangle.Intersects(this.CollisionRectangle);
            if (isCollide)
            {
                OnCollide = true;
            }
            else
            {
                OnCollide = false;
            }
            return isCollide;
        }
    }
    public class Tile
    {
        public int Width, Height, X, Y;
        public Vector2 Position;
        public Tile()
        {
        }
        public Tile(int x, int y)
        {
            X = x;
            Y = y;         
        }
        public Tile(int x,int y,int w,int h) : this(x,y)
        {
            Width = w;
            Height = h;
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle(
                X,Y,Width,Height
                );
        }
    }
    public class TileMap
    {
        Tile[,] tiles;
        public int Rows;
        public int Cols;
        public int FrameWidth;
        public int FrameHeight;
        public TileMap(int rows,int cols)
        {
            Rows = rows;
            Cols = cols;
            tiles = new Tile[rows, cols];
        }
        public Tile[,] GetTiles()
        {
            return tiles;
        }
        public Tile GetTile(int row,int col)
        {
            return tiles[row, col];
        }
        public void ClearTiles()
        {
            tiles = new Tile[Rows, Cols];
        }
        public void FillMap(Tile tile)
        {
            ClearTiles();
            for(int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    int posX = (int)col * FrameWidth;
                    int posY = (int)row * FrameHeight;
                    tile.Position = new Vector2(posX, posY);
                    SetTile(row, col, tile);
                }
            }
        }
        public void SetTiles(List<Cell> cells,Tile tile)
        {
            foreach(Cell cell in cells)
            {
                if (cell.Row < Rows)
                {
                    if(cell.Col< Cols)
                    {
                        SetTile(cell.Row, cell.Col, tile);
                    }
                }
            }
        }
        public void SetTile(int row,int col,Tile tile)
        {
            Tile t = new Tile();
            t.Width = tile.Width;
            t.Height = tile.Height;
            t.X = tile.X;
            t.Y = tile.Y;
            t.Position =new Vector2(tile.Position.X,tile.Position.Y);
            tiles[row, col] = t;
        }
    }
    public class TileSet
    {
        public Texture2D SpriteSource;
        public TileMap TileMap;
        public TileSet(Texture2D source)
        {
            SpriteSource = source;
        }
        public void CreateMap(int rows,int cols)
        {
            TileMap = new TileMap(rows, cols);
        }
        public void DrawObjects(SpriteBatch spriteBatch,Point[] points,int Wide,int High,Tile tile)
        {
            foreach(Point point in points)
            {
                Rectangle frame = new Rectangle(
                    point, new Point(Wide, High)
                    );
                spriteBatch.Draw(SpriteSource, frame, tile.GetRectangle(), Color.White);
            }
        }
        public void DrawObject(SpriteBatch spriteBatch, int xDest, int yDest, int Wide, int High, Tile tile)
        {
            Rectangle frame = new Rectangle(
                new Point(xDest, yDest), new Point(Wide, High)
            );
            spriteBatch.Draw(SpriteSource, frame, tile.GetRectangle(), Color.White);
        }
        public void DrawFloor(SpriteBatch spriteBatch)
        {
            for(int r = 0; r < TileMap.Rows; r++)
            {
                for (int c = 0; c < TileMap.Cols; c++)
                {
                    Tile tile = TileMap.GetTile(r, c);

                    Rectangle frame = new Rectangle(
                        (int)tile.Position.X,
                        (int)tile.Position.Y,
                        TileMap.FrameWidth,
                        TileMap.FrameHeight
                        );

                    spriteBatch.Draw(SpriteSource,frame,tile.GetRectangle(), Color.White);
                }
            }
        }
    }
    public class Cell
    {
        public int Row;
        public int Col;
        public object Value;
    }
}