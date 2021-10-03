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
        public Rectangle SpriteRectangle;
        public Vector2 SpritePosition;
        public SpriteAnimation Animation;
        public SpriteDirection Direction;
        public int SpriteCols;
        public int SpriteRows;
        public int OriginX;
        public int OriginY;
        public int WalkSpeed;
        public int RunSpeed;
        public int AnimationSpeed;
        public int Width;
        public int Height;
        public bool IsMove;
        public bool IsAnimated;
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
            this.Direction = SpriteDirection.MoveUp;
            this.SpritePosition.Y -= this.WalkSpeed;
            this.IsMove = true;
        }
        public void MoveDown()
        {
            this.Direction = SpriteDirection.MoveDown;
            this.SpritePosition.Y += this.WalkSpeed;
            this.IsMove = true;
        }
        public void MoveLeft()
        {
            this.Direction = SpriteDirection.MoveLeft;
            this.SpritePosition.X -= this.WalkSpeed;
            this.IsMove = true;
        }
        public void MoveRight()
        {
            this.Direction = SpriteDirection.MoveRight;
            this.SpritePosition.X += this.WalkSpeed;
            this.IsMove = true;
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

            this.SpriteRectangle = new Rectangle(
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

            int charPositionX = MathHelper.Clamp(calPositionX, limitLeft, spaceWidth - limitRight);
            int charPositionY = MathHelper.Clamp(calPositionY, limitUp, spaceHeight - limitDown);

            Rectangle frameRect = new Rectangle(
                charPositionX, charPositionY,
                Width,
                Height
                );

            spriteBatch.Draw(this.SpriteSource, frameRect, this.SpriteRectangle, Color.White);
        }

        public void DrawCenter(Viewport vp, SpriteBatch spriteBatch, int limitLeft = 0, int limitRight = 0, int limitUp = 0, int limitDown = 0)
        {
            int spaceWidth = (int)(vp.Width - Width);
            int spaceHeight = (int)(vp.Height - Height);

            int calPositionX = (spaceWidth / 2) + (int)this.SpritePosition.X;
            int calPositionY = (spaceHeight / 2) + (int)this.SpritePosition.Y;

            int charPositionX = MathHelper.Clamp(calPositionX, limitLeft, spaceWidth - limitRight);
            int charPositionY = MathHelper.Clamp(calPositionY, limitUp, spaceHeight - limitDown);

            Rectangle frameRect = new Rectangle(
                charPositionX, charPositionY,
                Width,
                Height
                );

            spriteBatch.Draw(this.SpriteSource, frameRect, this.SpriteRectangle, Color.White);
        }
    }
    public class Cell
    {
        public int Row;
        public int Col;
        public object Value;
    }
    public class Tile
    {
        public int Width;
        public int Height;
        public int X;
        public int Y;
        public Vector2 Position;
        public Tile()
        {

        }
        public Tile(int x, int y)
        {
            X = x;
            Y = y;         
        }
        public Tile(int x,int y,int w,int h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }
    }
    public class TileMap
    {
        Tile[,] tiles;
        public int Rows;
        public int Cols;
        public int TileWidth;
        public int TileHeight;
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
            for(int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    int posX = (int)col * TileWidth;
                    int posY = (int)row * TileHeight;
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
            t.Position = tile.Position;
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
        public void Draw(SpriteBatch spriteBatch)
        {
            for(int r = 0; r < TileMap.Rows; r++)
            {
                for (int c = 0; r < TileMap.Cols; r++)
                {
                    Tile tile = TileMap.GetTile(r, c);

                    Rectangle frame = new Rectangle(
                        (int)tile.Position.X,
                        (int)tile.Position.Y,
                        tile.Width,
                        tile.Height
                        );

                    Rectangle sprite = new Rectangle(
                        tile.X,
                        tile.Y,
                        TileMap.TileWidth,
                        TileMap.TileHeight
                        );
                    spriteBatch.Draw(SpriteSource,frame,sprite, Color.White);
                }
            }
        }
    }

}