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
        NoMove=-1,MoveDown = 0, MoveLeft = 1, MoveRight = 2, MoveUp = 3
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
        public int SpriteCols,SpriteRows;
        public int OriginX,OriginY;
        public Point CurrentPosition;
        public int WalkSpeed,RunSpeed,AnimationSpeed;
        public int Width,Height;
        public int AdjustX,AdjustY;
        public int AdjustWidth,AdjustHeight;
        public bool IsMove,IsAnimated;
        public bool IsPlayer;
        public bool ShowCollision = false;
        public string Name;
        public string InteractionTo;
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
        public void ChangeDirection(SpriteDirection direction)
        {
            this.Direction = direction;
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
            this.Direction = SpriteDirection.MoveUp;            
            this.SpritePosition.Y -= this.WalkSpeed;
        }
        public void MoveDown()
        {
            this.IsMove = true;
            this.Direction = SpriteDirection.MoveDown;            
            this.SpritePosition.Y += this.WalkSpeed;
        }
        public void MoveLeft()
        {
            this.IsMove = true;
            this.Direction = SpriteDirection.MoveLeft;            
            this.SpritePosition.X -= this.WalkSpeed;
        }
        public void MoveRight()
        {
            this.IsMove = true;
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
        public void Draw(SpriteBatch spriteBatch)
        {
            this.DestinationRectangle = new Rectangle(
                CurrentPosition.X, CurrentPosition.Y,
                Width,
                Height
                );
            CollisionRectangle = GetCollision();
            if (ShowCollision)
            {
                Texture2D collideBox = new Texture2D(this.SpriteSource.GraphicsDevice, 1, 1);
                collideBox.SetData(new Color[] { Color.Blue });
                spriteBatch.Draw(collideBox, CollisionRectangle, Color.White);
            }
            spriteBatch.Draw(this.SpriteSource, this.DestinationRectangle, this.SourceRectangle, Color.White);
        }
        public void Draw(Viewport vp, SpriteBatch spriteBatch, Vector2 position, 
            int limitLeft = 0, int limitRight = 0, int limitUp = 0, int limitDown = 0)
        {
            int spaceWidth = (int)(vp.Width - Width);
            int spaceHeight = (int)(vp.Height - Height);

            int calPositionX = (int)position.X + (int)this.SpritePosition.X;
            int calPositionY = (int)position.Y + (int)this.SpritePosition.Y;

            CurrentPosition.X = MathHelper.Clamp(calPositionX, limitLeft, spaceWidth - limitRight);
            CurrentPosition.Y = MathHelper.Clamp(calPositionY, limitUp, spaceHeight - limitDown);
            this.Draw(spriteBatch);
        }

        public void DrawCenter(Viewport vp, SpriteBatch spriteBatch, 
            int limitLeft = 0, int limitRight = 0, int limitUp = 0, int limitDown = 0)
        {
            int spaceWidth = (int)(vp.Width - Width);
            int spaceHeight = (int)(vp.Height - Height);

            int calPositionX = (spaceWidth / 2) + (int)this.SpritePosition.X;
            int calPositionY = (spaceHeight / 2) + (int)this.SpritePosition.Y;

            CurrentPosition.X = MathHelper.Clamp(calPositionX, limitLeft, spaceWidth - limitRight);
            CurrentPosition.Y = MathHelper.Clamp(calPositionY, limitUp, spaceHeight - limitDown);

            this.Draw(spriteBatch);
        }
        public bool IsCollide(SpriteCharacter compareTo)
        {
            bool bCollide= false;
            if (this.GetCollision().Intersects(compareTo.GetCollision()))
            {
                bCollide = true;
            }
            return bCollide;
        }
        public bool IsFaceToFace(SpriteCharacter compareTo)
        {
            Rectangle playerRect = this.GetCollision();
            Rectangle npcRect = compareTo.GetCollision();
            if (playerRect.Intersects(npcRect))
            {
                SpriteDirection faceDirection = SpriteDirection.NoMove;
                Point playerPoint = this.GetFacePoint();
                bool bOnRange = false;

                switch (this.Direction)
                {
                    case SpriteDirection.MoveUp:
                        faceDirection = SpriteDirection.MoveDown;
                        bOnRange = playerPoint.X >= compareTo.GetFaceRange()[0] 
                            && playerPoint.X <= compareTo.GetFaceRange()[1];
                        break;
                    case SpriteDirection.MoveDown:
                        faceDirection = SpriteDirection.MoveUp;
                        bOnRange = playerPoint.X >= compareTo.GetFaceRange()[0] 
                            && playerPoint.X <= compareTo.GetFaceRange()[1];
                        break;
                    case SpriteDirection.MoveLeft:
                        faceDirection = SpriteDirection.MoveRight;
                        bOnRange = playerPoint.Y >= compareTo.GetFaceRange()[0] 
                            && playerPoint.Y <= compareTo.GetFaceRange()[1];
                        break;
                    case SpriteDirection.MoveRight:
                        faceDirection = SpriteDirection.MoveLeft;
                        bOnRange = playerPoint.Y >= compareTo.GetFaceRange()[0] 
                            && playerPoint.Y <= compareTo.GetFaceRange()[1];
                        break;
                }

                if (compareTo.Direction == faceDirection)
                {
                    return bOnRange;
                }
            }
            return false;
        }
        public int[] GetFaceRange()
        {
            int[] arr = new int[2];
            switch (this.Direction)
            {
                case SpriteDirection.MoveUp:
                case SpriteDirection.MoveDown:
                    arr[0] = CollisionRectangle.X;
                    arr[1] = CollisionRectangle.X + CollisionRectangle.Width;
                    break;
                case SpriteDirection.MoveLeft:
                case SpriteDirection.MoveRight:
                    arr[0] = CollisionRectangle.Y;
                    arr[1] = CollisionRectangle.Y + CollisionRectangle.Height;
                    break;
            }
            return arr;
        }
        public Point GetFacePoint()
        {
            Point facePoint = new Point(0, 0);
            switch (this.Direction)
            {
                case SpriteDirection.MoveUp:
                    facePoint = new Point(
                        this.CollisionRectangle.X + (this.CollisionRectangle.Width / 2), 
                        this.CollisionRectangle.Y
                        );
                    break;
                case SpriteDirection.MoveDown:
                    facePoint = new Point(
                        this.CollisionRectangle.X + (this.CollisionRectangle.Width / 2), 
                        this.CollisionRectangle.Y+(this.CollisionRectangle.Height)
                        );
                    break;
                case SpriteDirection.MoveLeft:
                    facePoint = new Point(
                        this.CollisionRectangle.X, 
                        this.CollisionRectangle.Y + (this.CollisionRectangle.Height / 2)
                        );
                    break;
                case SpriteDirection.MoveRight:
                    facePoint = new Point(
                        this.CollisionRectangle.X + this.CollisionRectangle.Width, 
                        this.CollisionRectangle.Y+(this.CollisionRectangle.Height/2)
                        );
                    break;
            }
            return facePoint;
        }
        public virtual void SetCollision()
        {

        }
        public Rectangle GetCollision()
        {
            SetCollision();   
            float currentX = this.CurrentPosition.X-AdjustX;
            float currentY = this.CurrentPosition.Y-AdjustY;
            float currentW = this.Width-AdjustWidth;
            float currentH = this.Height-AdjustHeight;
            return new Rectangle(
                (int)currentX,
                (int)currentY,
                (int)currentW,
                (int)currentH);
        }
    }
    public class Tile
    {
        public int Width, Height, X, Y;
        public Vector2 Position;
        public Tile() { }
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
        public List<Rectangle> CollisionObjects;
        public TileSet(Texture2D source)
        {
            CollisionObjects = new List<Rectangle>();
            SpriteSource = source;
        }
        public void CreateMap(int rows,int cols)
        {
            TileMap = new TileMap(rows, cols);
        }
        public void DrawObjects(SpriteBatch spriteBatch, Point[] points, int Wide, int High, 
            Tile tile, bool AddCollision = false)
        {
            foreach(Point point in points)
            {
                Rectangle frame = new Rectangle(
                    point, new Point(Wide, High)
                    );
                if (AddCollision)
                    CollisionObjects.Add(frame);
                spriteBatch.Draw(SpriteSource, frame, tile.GetRectangle(), Color.White);
            }
        }
        public void DrawObject(SpriteBatch spriteBatch, int xDest, int yDest, int Wide, int High, 
            Tile tile,bool AddCollision=false)
        {
            Rectangle frame = new Rectangle(
                new Point(xDest, yDest), new Point(Wide, High)
            );
            if (AddCollision)
                CollisionObjects.Add(frame);
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