using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LearningGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Texture2D background;
        Texture2D chocoSource;
        Rectangle chocoYellow;
        SpriteBatch spriteBatch;
        Vector2 spritePos;
        int spritePosX = 50;
        int spritePosY = 48;
        int spriteMotion = 1;
        int spriteIndex = 0;
        float frameCount;
        bool isAnimated = true;
        KeyboardState previousKeyState;
        KeyboardState currentKeyState;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferWidth = 768;
            graphics.IsFullScreen = false;
            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            isAnimated = false;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            background = Content.Load<Texture2D>("titlescreen");
            chocoSource = Content.Load<Texture2D>("chocobo");
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            base.LoadContent();
        }
        private void ChangeMotion()
        {
            if (spriteMotion == 2)
            {
                spriteMotion = 0;
            }
            else
            {
                spriteMotion += 1;
            }
        }
        private void ChangeDirection()
        {
            if (spriteIndex == 3)
            {
                spriteIndex = 0;
            }
            else
            {
                spriteIndex += 1;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            frameCount += 1;
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            bool isMove = false;
            if (currentKeyState.IsKeyUp(Keys.Left) && previousKeyState.IsKeyDown(Keys.Left))
            {
                spriteIndex = 1;
                spritePos.X -= 5;
                ChangeMotion();
                isMove = true;
            }
            if (currentKeyState.IsKeyUp(Keys.Right) && previousKeyState.IsKeyDown(Keys.Right))
            {
                spriteIndex = 2;
                spritePos.X += 5;
                ChangeMotion();
                isMove = true;
            }
            if (currentKeyState.IsKeyUp(Keys.Down) && previousKeyState.IsKeyDown(Keys.Down))
            {
                spriteIndex = 0;
                spritePos.Y += 5;
                ChangeMotion();
                isMove = true;
            }
            if (currentKeyState.IsKeyUp(Keys.Up) && previousKeyState.IsKeyDown(Keys.Up))
            {
                spriteIndex = 3;
                spritePos.Y -= 5;
                ChangeMotion();
                isMove = true;
            }
            if (currentKeyState.IsKeyUp(Keys.Enter) && previousKeyState.IsKeyDown(Keys.Enter))
            {
                ChangeMotion();
                isMove = true;
            }
            if (currentKeyState.IsKeyUp(Keys.Space) && previousKeyState.IsKeyDown(Keys.Space))
            {
                ChangeDirection();
                isMove = true;
            }
            if (currentKeyState.IsKeyUp(Keys.Escape) && previousKeyState.IsKeyDown(Keys.Escape))
            {
                isAnimated = !isAnimated;
            }
            if (isMove == false && isAnimated == true)
            {
                if ((frameCount % 20) == 0)
                {
                    ChangeMotion();
                    if (spriteMotion == 2)
                    {
                        ChangeDirection();
                    }
                }
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);

            Viewport viewPort = graphics.GraphicsDevice.Viewport;

            chocoYellow = new Rectangle(
                spritePosX * spriteMotion, spritePosY * spriteIndex,
                spritePosX, spritePosY
                );

            int frameWidth = spritePosX * 2;
            int frameHeight = spritePosY * 2;

            int framePosX = (int)((viewPort.Width - frameWidth) / 2) + (int)spritePos.X;
            int framePosY = (int)((viewPort.Height - frameHeight) / 2) + (int)spritePos.Y;

            framePosX = MathHelper.Clamp(framePosX, 0, (viewPort.Width - frameWidth));
            framePosY = MathHelper.Clamp(framePosY, 100, (viewPort.Height - frameHeight));

            Rectangle sourceRectangle = new Rectangle(
                framePosX, framePosY,
                frameWidth,
                frameHeight
                );

            Rectangle backgroundRect = new Rectangle(
                0, 0,
                viewPort.Width, viewPort.Height
                );

            Window.Title = "FPS:" + frameCount + " MODE: " + (isAnimated ? "AUTO" : "MANUAL");

            spriteBatch.Begin();
            spriteBatch.Draw(background, backgroundRect, Color.White);
            spriteBatch.Draw(chocoSource, sourceRectangle, chocoYellow, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}