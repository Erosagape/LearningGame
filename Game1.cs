using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LearningGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int spritePosX;
        int spritePosY;
        int spriteRow = 0;
        int spriteIndex = 0;
        int spriteSet = 0;
        int spriteWidth = 50;
        int spriteHeight = 48;
        Texture2D chocoSource;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";          
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            chocoSource = Content.Load<Texture2D>("chocobo");
            // TODO: use this.Content to load your game content here
            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                
            if(Keyboard.GetState().IsKeyUp(Keys.Space)){
                if (spriteIndex < 2)
                {
                    spriteIndex++;
                }                    
                else
                {
                    spriteIndex = 0;
                }
            }
            
            if (Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                if (spriteRow < 3)
                {
                    spriteRow++;
                }
                else
                {
                    spriteRow = 0;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);
            Viewport viewPort = graphics.GraphicsDevice.Viewport;

            spritePosX = (viewPort.Width - spriteWidth) / 2;
            spritePosY = (viewPort.Height - spriteHeight) / 2; 
            
            Vector2 spritePosition = new Vector2(
                spritePosX,spritePosY
            );

            int col_modifier = (spriteIndex * spriteWidth);
            int row_modifier = (spriteRow * spriteHeight);

            Rectangle characterPosition = new Rectangle(
                spriteSet+row_modifier, spriteWidth+col_modifier,
                spriteWidth,spriteHeight
                );

            spriteBatch.Begin();
            spriteBatch.Draw(chocoSource, spritePosition, characterPosition,Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }

    }
}
