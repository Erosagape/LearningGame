using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace LearningGame
{
    public class CollisionSample : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Texture2D playerBox;
        Rectangle playerRect;
        Texture2D[] objectBoxs;
        Rectangle[] objectRects;
        Vector2 motion = Vector2.Zero;
        string currentPos = "Player={0}";
        public CollisionSample()
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
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("Fonts/defaultfont");

            playerBox = new Texture2D(graphics.GraphicsDevice, 1, 1);
            playerBox.SetData(new Color[] { Color.Black });
            playerRect = new Rectangle(10, 10, 30, 40);

            objectBoxs = new Texture2D[5];
            objectRects = new Rectangle[5];

            for (int i = 0; i < 5; i++)
            {
                objectBoxs[i] = new Texture2D(graphics.GraphicsDevice, 1, 1);
                objectBoxs[i].SetData(new Color[] { Color.Blue });                
            }
            objectRects[0] = new Rectangle(150, 150, 30, 40);
            objectRects[1] = new Rectangle(150+ 60, 150 + 40, 30, 40);
            objectRects[2] = new Rectangle(150 + 125, 150 + 80, 30, 40);
            objectRects[3] = new Rectangle(150 + 210, 150 + 120, 30, 40);
            objectRects[4] = new Rectangle(150 + 240, 150 + 160, 30, 40);

            motion = new Vector2(playerRect.X, playerRect.Y);

            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            Vector2 currentPos = motion;
            if(Input.CurrentDirection==DirectionState.UpButtonKeyDown||
                Input.CurrentDirection == DirectionState.UpButtonKeyPress||
                Input.CurrentDirection == DirectionState.UpButtonKeyHold)
            {
                motion.Y -= 1;
            }

            if (Input.CurrentDirection == DirectionState.DownButtonKeyDown ||
    Input.CurrentDirection == DirectionState.DownButtonKeyPress ||
    Input.CurrentDirection == DirectionState.DownButtonKeyHold)
            {
                motion.Y += 1;
            }

            if (Input.CurrentDirection == DirectionState.LeftButtonKeyDown ||
Input.CurrentDirection == DirectionState.LeftButtonKeyPress ||
Input.CurrentDirection == DirectionState.LeftButtonKeyHold)
            {
                motion.X -= 1;
            }

            if (Input.CurrentDirection == DirectionState.RightButtonKeyDown ||
Input.CurrentDirection == DirectionState.RightButtonKeyPress ||
Input.CurrentDirection == DirectionState.RightButtonKeyHold)
            {
                motion.X += 1;
            }
            
            motion.X = MathHelper.Clamp(motion.X, 0, graphics.GraphicsDevice.Viewport.Width - playerRect.Width);
            motion.Y = MathHelper.Clamp(motion.Y, 0, graphics.GraphicsDevice.Viewport.Height - playerRect.Height);

            Rectangle collision = new Rectangle(
                (int)motion.X,
                (int)motion.Y,
                playerRect.Width,
                playerRect.Height);
            
            bool isCollide = false;
            for(int i = 0; i < 5; i++)
            {
                if (collision.Intersects(objectRects[i]))
                {
                    objectBoxs[i].SetData(new Color[] { Color.Red });
                    isCollide = true;
                } else
                {
                    objectBoxs[i].SetData(new Color[] { Color.Blue });
                }
            }
            if (isCollide)
            {
                motion = currentPos;
            }

            playerRect.X = (int)motion.X;
            playerRect.Y = (int)motion.Y;

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Chocolate);
            Viewport viewPort = graphics.GraphicsDevice.Viewport;

            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont,                                
                "Key=" + Input.CurrentDirection + " " + String.Format(currentPos, motion.X + "/" + motion.Y), 
                Vector2.Zero, 
                Color.Black);

            spriteBatch.Draw(playerBox, playerRect, Color.White);
            for(int i = 0; i < 5; i++)
            {
                spriteBatch.Draw(objectBoxs[i], objectRects[i], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
