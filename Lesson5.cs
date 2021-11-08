using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningGame
{
    public class Lesson5 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Chocobo chocoboYellow = new Chocobo(0, 0, 120, 100);
        Moogle mog = new Moogle(0, 0, 60, 70);
        int frameCount = 0;
        public Lesson5()
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
            mog.SpriteSource = Content.Load<Texture2D>("moogle1");
            chocoboYellow.SpriteSource = Content.Load<Texture2D>("chocobo");
            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            frameCount += 1;

            chocoboYellow.IsAnimated = true;
            //chocoboYellow.ShowCollision = true;
            chocoboYellow.SetCollision();
            chocoboYellow.SetSpeed(1, 4);            
            chocoboYellow.Update(frameCount);

            mog.IsAnimated = true;
            mog.SetSpeed(2, 1);
            //dsmog.ShowCollision = true;
            mog.SetCollision();
            mog.Update(frameCount);

            mog.OnCollide = mog.IsCollide(chocoboYellow);
            Input.UpdateCharacter(mog);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);
            Viewport viewPort = graphics.GraphicsDevice.Viewport;

            string displayText =mog.OnCollide + ": T" + mog.TopOrigin + ",L" + mog.LeftOrigin+ ",R" + mog.RightOrigin + ",B" + mog.BottomOrigin;
            spriteBatch.Begin();
            chocoboYellow.DrawCenter(viewPort, spriteBatch);
            mog.Draw(viewPort, spriteBatch,Vector2.Zero);
            
            spriteBatch.DrawString(
                spriteFont, 
                displayText,
                new Vector2((viewPort.Width - spriteFont.MeasureString(displayText).X), 0),
                Color.Black
                );

            displayText = mog.BlockDirection + ": T" + chocoboYellow.TopOrigin + ",L" + chocoboYellow.LeftOrigin + ",R" + chocoboYellow.RightOrigin + ",B" + chocoboYellow.BottomOrigin;
            spriteBatch.DrawString(
                spriteFont,
                displayText,
                new Vector2((viewPort.Width - spriteFont.MeasureString(displayText).X), spriteFont.MeasureString(displayText).Y),
                Color.Red
                );

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
