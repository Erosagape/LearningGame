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
            graphics.GraphicsDevice.Clear(Color.Transparent);
            
            Viewport viewPort = graphics.GraphicsDevice.Viewport;   
            
            int imagePositionX = (int)(viewPort.Width*0.1);
            int imagePositionY = (int)(viewPort.Height*0.1);

            int imageWidth = viewPort.Width - (imagePositionX*2);
            int imageHeight = viewPort.Height - (imagePositionY*2);

            Window.Title = "X=" + imagePositionX + ",Y=" + imagePositionY;
            Window.Title += ",Width=" + imageWidth + ",Height=" + imageHeight;
            Window.Title += ",Resolution=" + viewPort.Width + "x" + viewPort.Height;

            Rectangle backgroundRectangle = new Rectangle(
                 imagePositionX,
                 imagePositionY,
                imageWidth,
                imageHeight
                );

            spriteBatch.Begin();
            spriteBatch.Draw(titleBackground, backgroundRectangle,Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
