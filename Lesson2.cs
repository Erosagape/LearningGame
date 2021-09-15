using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace LearningGame
{
    public class Lesson2 : Game
    {
        GraphicsDeviceManager graphics;
        Texture2D titleBackground;
        SpriteBatch spriteBatch;
        public Lesson2()
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
            base.Initialize();
        }
        protected override void LoadContent()
        {
            titleBackground = Content.Load<Texture2D>("moogle1");
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);
            
            Viewport viewPort = graphics.GraphicsDevice.Viewport;            
            Rectangle backgroundRectangle = new Rectangle(
                0, 0,
                titleBackground.Width,
                titleBackground.Height
                );

            spriteBatch.Begin();
            spriteBatch.Draw(titleBackground, backgroundRectangle,Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
