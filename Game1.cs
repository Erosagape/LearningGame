using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LearningGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private string currentKeyInput;
        private string previousKeyInput;
        private double secondsPass;
        private string displayText = "";
        private string displayTime = "";
        private SpriteFont displayFont;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            currentKeyInput = "";
            previousKeyInput = "";
            displayText = "Previous = {0} Current = {1}";
            secondsPass = 0;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            displayFont = Content.Load<SpriteFont>("Fonts/defaultfont");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if (previousKeyInput != currentKeyInput && currentKeyInput != "")
            {
                previousKeyInput = currentKeyInput;
            }
            currentKeyInput = "";
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                currentKeyInput = "Left";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                currentKeyInput = "Right";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                currentKeyInput = "Up";
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                currentKeyInput = "Down";
            }

            secondsPass += gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            displayTime = "Game Time " + (int)secondsPass;
            string showText = string.Format(displayText,previousKeyInput,currentKeyInput);
            GraphicsDevice.Clear(Color.Blue);
            Viewport vp = GraphicsDevice.Viewport;
            // TODO: Add your drawing code here
            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.DrawString(displayFont, displayTime, Vector2.Zero, Color.Yellow);
            spriteBatch.DrawString(
                displayFont,
                showText,
                new Vector2(
                    vp.Width / 2  - displayFont.MeasureString(showText).X / 2,
                    vp.Height / 2 - displayFont.MeasureString(showText).Y / 2
                    ),
                Color.White);
            spriteBatch.End();
        }
    }
}
