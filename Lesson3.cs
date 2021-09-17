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
        protected override void Update(GameTime gameTime)
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            if (currentKeyState.IsKeyUp(Keys.Enter) && previousKeyState.IsKeyDown(Keys.Enter))
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
            if (currentKeyState.IsKeyUp(Keys.Space) && previousKeyState.IsKeyDown(Keys.Space))
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
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            
            Viewport viewPort = graphics.GraphicsDevice.Viewport;
            chocoYellow = new Rectangle(
                spritePosX * spriteMotion, spritePosY * spriteIndex,
                spritePosX, spritePosY
                );
            Vector2 spritePos = new Vector2(
                (viewPort.Width-spritePosX) /2,
                (viewPort.Height-spritePosY) /2
                );
            Rectangle sourceRectangle = new Rectangle(
                (int)spritePos.X, (int)spritePos.Y,
                spritePosX,
                spritePosY
                );

            spriteBatch.Begin();
            spriteBatch.Draw(chocoSource, sourceRectangle,chocoYellow, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
