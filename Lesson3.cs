using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LearningGame
{
    public enum SpriteMotion
    {
        WalkRight=0,
        WalkIdle=1,
        WalkLeft=2
    }
    public enum SpriteMove
    {
        MoveDown=0,
        MoveLeft=1,
        MoveRight=2,
        MoveUp=3
    }
    public class Lesson3 : Game
    {
        GraphicsDeviceManager graphics;
        Texture2D backgroundSource;
        Texture2D spriteSource;
        SpriteBatch spriteBatch;
        SpriteMove spriteMove;
        SpriteMotion spriteMotion;
        Vector2 spritePosition;
        KeyboardState keyboardCurrent;
        KeyboardState keyboardPrevious;
        bool isChangeScene = false;
        public Lesson3()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferWidth = 768;
            graphics.IsFullScreen = true;
            IsMouseVisible = true;
            
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            spriteMove = SpriteMove.MoveDown;
            spriteMotion = SpriteMotion.WalkIdle;
            spritePosition = Vector2.Zero;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            spriteSource = Content.Load<Texture2D>("moogle1");
            backgroundSource = Content.Load<Texture2D>("titlescreen");
            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            keyboardPrevious = keyboardCurrent;
            keyboardCurrent = Keyboard.GetState();
            
            if (keyboardCurrent.IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardCurrent.IsKeyUp(Keys.Enter) && keyboardPrevious.IsKeyDown(Keys.Enter))
                isChangeScene=!isChangeScene;

            int spriteWalkSpeed = 1;
            int spriteRunSpeed = 4;
            bool onMove = false;
            if (keyboardCurrent.IsKeyDown(Keys.W))
            {
                spriteMove = SpriteMove.MoveUp;
                if (keyboardPrevious.IsKeyDown(Keys.W))
                    spriteWalkSpeed += spriteRunSpeed;
                spritePosition.Y -= spriteWalkSpeed;
                onMove = true;
            }

            if (keyboardCurrent.IsKeyDown(Keys.A))
            {
                spriteMove = SpriteMove.MoveLeft;
                if (keyboardPrevious.IsKeyDown(Keys.A))
                    spriteWalkSpeed += spriteRunSpeed;
                spritePosition.X -= spriteWalkSpeed;
                onMove = true;
            }

            if (keyboardCurrent.IsKeyDown(Keys.S))
            {
                spriteMove = SpriteMove.MoveDown;
                if (keyboardPrevious.IsKeyDown(Keys.S))
                    spriteWalkSpeed += spriteRunSpeed;
                spritePosition.Y += spriteWalkSpeed;
                onMove = true;
            }      
            
            if (keyboardCurrent.IsKeyDown(Keys.D))
            {
                spriteMove = SpriteMove.MoveRight;                
                if (keyboardPrevious.IsKeyDown(Keys.D))
                    spriteWalkSpeed += spriteRunSpeed;
                spritePosition.X += spriteWalkSpeed;
                onMove = true;                
            }

            if (keyboardCurrent.IsKeyUp(Keys.Down) && keyboardPrevious.IsKeyDown(Keys.Down))
            {
                spriteMove = SpriteMove.MoveDown;
                onMove = true;
            }

            if (keyboardCurrent.IsKeyUp(Keys.Left) && keyboardPrevious.IsKeyDown(Keys.Left))
            {
                spriteMove = SpriteMove.MoveLeft;
                onMove = true;
            }            

            if (keyboardCurrent.IsKeyUp(Keys.Right) && keyboardPrevious.IsKeyDown(Keys.Right))
            {
                spriteMove = SpriteMove.MoveRight;
                onMove = true;
            }

            if (keyboardCurrent.IsKeyUp(Keys.Up) && keyboardPrevious.IsKeyDown(Keys.Up))
            {
                spriteMove= SpriteMove.MoveUp;
                onMove = true;
            }

            if (onMove)
            {
                if ((int)spriteMotion >= 2)
                    spriteMotion = 0;
                else
                    spriteMotion++;
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);

            if (!isChangeScene)
                DrawNoBackground();
            else
                DrawWithBackground();

            base.Draw(gameTime);
        }
        private void DrawNoBackground()
        {
            Viewport viewPort = graphics.GraphicsDevice.Viewport;

            int spriteRows = 4;
            int spriteCols = 3;

            int spriteWidth = (int)(spriteSource.Width / spriteCols);
            int spriteHeight = (int)(spriteSource.Height / spriteRows);

            int roomWidth = (int)(viewPort.Width - spriteWidth);
            int roomHeight = (int)(viewPort.Height - spriteHeight);

            int framePositionX = MathHelper.Clamp( (roomWidth / 2) + (int)spritePosition.X , 0 ,roomWidth);
            int framePositionY = MathHelper.Clamp( (roomHeight / 2) + (int)spritePosition.Y , 0 ,roomHeight);

            Vector2 spriteLocation = new Vector2(spriteWidth * (int)spriteMotion, spriteHeight * (int)spriteMove);

            Rectangle spriteRectangle = new Rectangle(
                (int)spriteLocation.X,
                (int)spriteLocation.Y,
                spriteWidth,
                spriteHeight
                );

            Rectangle frameRectangle = new Rectangle(
                framePositionX,
                framePositionY,
                spriteWidth,
                spriteHeight
                );

            spriteBatch.Begin();
            spriteBatch.Draw(spriteSource, frameRectangle, spriteRectangle, Color.White);
            spriteBatch.End();

            Window.Title = "X=" + framePositionX + ",Y=" + framePositionY;
            Window.Title += ",Width=" + spriteWidth + ",Height=" + spriteHeight;
            Window.Title += ",Resolution=" + viewPort.Width + "x" + viewPort.Height;
            Window.Title += ",Move=" + spriteMove + ",Motion=" + spriteMotion;

        }
        private void DrawWithBackground()
        {
            Viewport viewPort = graphics.GraphicsDevice.Viewport;

            Rectangle backgroundRectangle = new Rectangle(
            0, 0,
            viewPort.Width,
            viewPort.Height
            );

            int spriteRows = 4;
            int spriteCols = 3;

            int spriteWidth = (int)(spriteSource.Width / spriteCols);
            int spriteHeight = (int)(spriteSource.Height / spriteRows);

            int frameWidth = (int)(spriteWidth * 2);
            int frameHeight = (int)(spriteHeight * 2);

            int roomWidth = (int)(viewPort.Width - frameWidth);
            int roomHeight = (int)(viewPort.Height - frameHeight);

            int framePositionX = MathHelper.Clamp((roomWidth / 2) + (int)spritePosition.X, 10, roomWidth - 10);
            int framePositionY = MathHelper.Clamp((roomHeight / 2) + (int)spritePosition.Y, 100, roomHeight - 10);
            //int framePositionX = MathHelper.Clamp( (roomWidth / 2) + (int)spritePosition.X , 0 ,roomWidth);
            //int framePositionY = MathHelper.Clamp( (roomHeight / 2) + (int)spritePosition.Y , 0 ,roomHeight);

            Vector2 spriteLocation = new Vector2(spriteWidth * (int)spriteMotion, spriteHeight * (int)spriteMove);

            Rectangle spriteRectangle = new Rectangle(
                (int)spriteLocation.X,
                (int)spriteLocation.Y,
                spriteWidth,
                spriteHeight
                );

            Rectangle frameRectangle = new Rectangle(
                framePositionX,
                framePositionY,
                frameWidth,
                frameHeight
                );

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundSource, backgroundRectangle, Color.White);
            spriteBatch.Draw(spriteSource, frameRectangle, spriteRectangle, Color.White);
            spriteBatch.End();

            Window.Title = "X=" + framePositionX + ",Y=" + framePositionY;
            Window.Title += ",Width=" + spriteWidth + ",Height=" + spriteHeight;
            Window.Title += ",Resolution=" + viewPort.Width + "x" + viewPort.Height;
            Window.Title += ",Move=" + spriteMove + ",Motion=" + spriteMotion;
        }
    }
}
