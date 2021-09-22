using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace LearningGame
{
    public class Lesson3 : Game
    {
        GraphicsDeviceManager graphics;
        Texture2D chocoSource;
        Rectangle chocoYellow;
        SpriteBatch spriteBatch;
        int spritePosX = 50;
        int spritePosY = 48;
        int spriteMotion = 1;
        int spriteIndex = 0;
        float frameCount;
        bool isAnimated=true;
        KeyboardState previousKeyState;
        KeyboardState currentKeyState;
        public Lesson3()
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
            base.Initialize();
        }
        protected override void LoadContent()
        {
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
            if (isMove == false && isAnimated==true)
            {
                if((frameCount % 20) == 0)
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

            Vector2 spritePos = new Vector2(
                (viewPort.Width-frameWidth) /2,
                (viewPort.Height-frameHeight) /2
                );

            Rectangle sourceRectangle = new Rectangle(
                (int)spritePos.X, (int)spritePos.Y,
                frameWidth,
                frameHeight
                );

            Window.Title = "FPS:" + frameCount + " MODE: " + (isAnimated ? "AUTO":"MANUAL");

            spriteBatch.Begin();
            spriteBatch.Draw(chocoSource, sourceRectangle,chocoYellow, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
