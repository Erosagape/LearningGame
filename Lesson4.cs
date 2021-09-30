using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LearningGame
{
    public class Lesson4 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        bool isAnimated;

        Texture2D backgroundSource;
        
        Chocobo player;

        Chocobo chocoboYellow=new Chocobo(0,0,120,100);
        Chocobo chocoboWhite=new Chocobo(150,0,50,50);
        Chocobo chocoboRed=new Chocobo(300,0,100,80);
        Chocobo chocoboBlue=new Chocobo(450,0,120,80);

        Chocobo chocoboGreen = new Chocobo(0, 192, 60, 70);
        Chocobo chocoboBlack = new Chocobo(150, 192, 100, 90);
        Chocobo chocoboGold = new Chocobo(300, 192, 80, 90);
        Chocobo chocoboOrange = new Chocobo(450, 192, 90, 100);

        Moogle mog=new Moogle(0,0,60,70);

        int wallLeft;
        int wallRight;
        int wallUp;
        int wallDown;

        int totalSeconds;
        int totalFrames;
        int framePerSeconds;
        int frameCount;

        KeyboardState previousKeyState;
        KeyboardState currentKeyState;
        public Lesson4()
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
            isAnimated = true;
            wallDown = 10;
            wallLeft = 10;
            wallRight = 10;
            wallUp = 100;

            chocoboWhite.Direction = SpriteDirection.MoveRight;
            chocoboBlue.Direction = SpriteDirection.MoveRight;
            chocoboBlack.Direction = SpriteDirection.MoveRight;
            chocoboGreen.Direction = SpriteDirection.MoveLeft;
            chocoboGold.Direction = SpriteDirection.MoveLeft;            
            chocoboRed.Direction = SpriteDirection.MoveLeft;
            chocoboOrange.Direction = SpriteDirection.MoveUp;

            mog.Direction = SpriteDirection.MoveDown;
            
            player = chocoboYellow;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            backgroundSource = Content.Load<Texture2D>("titlescreen");
            
            mog.SpriteSource = Content.Load<Texture2D>("moogle1");

            chocoboYellow.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboWhite.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboRed.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboBlue.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboGreen.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboBlack.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboGold.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboOrange.SpriteSource = Content.Load<Texture2D>("chocobo");

            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            base.LoadContent();
        }
        private bool IsKeyPress(Keys key)
        {
            return currentKeyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key);
        }
        private bool IsKeyDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }
        private bool IsKeyHold(Keys key)
        {
            return previousKeyState.IsKeyDown(key) && currentKeyState.IsKeyDown(key);
        }       

        private void UpdateCharacter(SpriteCharacter character)
        {
            //Check Face Angle 
            if (IsKeyPress(Keys.Up) || IsKeyPress(Keys.Down) || IsKeyPress(Keys.Left) || IsKeyPress(Keys.Right))
            {
                character.Animation = SpriteAnimation.Stand;
                if (IsKeyPress(Keys.Up))
                {
                    character.Direction = SpriteDirection.MoveUp;
                }
                if (IsKeyPress(Keys.Down))
                {
                    character.Direction = SpriteDirection.MoveDown;
                }
                if (IsKeyPress(Keys.Left))
                {
                    character.Direction = SpriteDirection.MoveLeft;
                }
                if (IsKeyPress(Keys.Right))
                {
                    character.Direction = SpriteDirection.MoveRight;
                }
            }

            //Check Walking animation
            character.IsMove = false;
            if (IsKeyHold(Keys.W)
            || IsKeyHold(Keys.S)
            || IsKeyHold(Keys.A)
            || IsKeyHold(Keys.D))
            {
                character.Run();
            }
            if (IsKeyDown(Keys.W))
            {
                character.MoveUp();
            }
            if (IsKeyDown(Keys.S))
            {
                character.MoveDown();
            }
            if (IsKeyDown(Keys.A))
            {
                character.MoveLeft();
            }
            if (IsKeyDown(Keys.D))
            {
                character.MoveRight();
            }
            
        }
        private void UpdateState(TimeSpan timePass)
        {
            if (IsKeyPress(Keys.Escape))
            {
                Exit();
            }
            if (IsKeyPress(Keys.Enter))
            {
                isAnimated = !isAnimated;
            }           
            if (totalSeconds != (int)timePass.TotalSeconds)
            {
                totalSeconds = (int)timePass.TotalSeconds;
                framePerSeconds = totalFrames;
                totalFrames = 0;
            }

            totalFrames += 1;
            frameCount += 1;

            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            Window.Title = "FPS:" + framePerSeconds + " MODE: " + (isAnimated ? "JOGGING" : "STANDING");
        }    
        private void SetupCharacter()
        {
            mog.IsAnimated = true;
            mog.SetSpeed(2, 1);

            chocoboYellow.IsAnimated = isAnimated;
            chocoboYellow.SetSpeed(1, 4);

            chocoboWhite.IsAnimated = true;
            chocoboWhite.SetSpeed(2, 3);

            chocoboRed.IsAnimated = false;
            chocoboRed.SetSpeed(3, 2);

            chocoboBlue.IsAnimated = !isAnimated;
            chocoboBlue.SetSpeed(4, 1);

            chocoboGreen.IsAnimated = !isAnimated;
            chocoboGreen.SetSpeed(2, 4);

            chocoboBlack.IsAnimated = false;
            chocoboBlack.SetSpeed(3, 3);

            chocoboGold.IsAnimated = true;
            chocoboGold.SetSpeed(4, 2);

            chocoboOrange.IsAnimated = isAnimated;
            chocoboOrange.SetSpeed(1, 2);

            if (IsKeyPress(Keys.Space))
            {
                player.ChangeDirection();
            }
            if (IsKeyPress(Keys.Tab))
            {
                if (player.Equals(chocoboYellow))
                {
                    player = chocoboRed;
                }
                else if (player.Equals(chocoboRed))
                {
                    player = chocoboGreen;
                }
                else if (player.Equals(chocoboGreen))
                {
                    player = chocoboBlue;
                }
                else if (player.Equals(chocoboBlue))
                {
                    player = chocoboBlack;
                }
                else if (player.Equals(chocoboBlack))
                {
                    player = chocoboWhite;
                }
                else if (player.Equals(chocoboWhite))
                {
                    player = chocoboOrange;
                }
                else if (player.Equals(chocoboOrange))
                {
                    player = chocoboGold;
                }
                else if (player.Equals(chocoboGold))
                {
                    player = chocoboYellow;
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
            UpdateState(gameTime.TotalGameTime);
            SetupCharacter();
            UpdateCharacter(player);

            mog.Update(frameCount);
            chocoboYellow.Update(frameCount);
            chocoboWhite.Update(frameCount);
            chocoboRed.Update(frameCount);
            chocoboBlue.Update(frameCount);
            chocoboGreen.Update(frameCount);
            chocoboBlack.Update(frameCount);
            chocoboGold.Update(frameCount);
            chocoboOrange.Update(frameCount);

            base.Update(gameTime);
        }
        private void DrawBackGround(Viewport vp)
        {
            Rectangle backgroundRectangle = new Rectangle(
                0, 0,
                vp.Width,
                vp.Height
            );

            spriteBatch.Draw(backgroundSource, backgroundRectangle, Color.White);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);
            Viewport viewPort = graphics.GraphicsDevice.Viewport;

            spriteBatch.Begin();

            DrawBackGround(viewPort);
            
            chocoboYellow.DrawCenter(viewPort, spriteBatch, wallLeft, wallRight, wallUp, wallDown);            
            chocoboWhite.Draw(viewPort, spriteBatch, new Vector2(50,100), wallLeft, wallRight, wallUp, wallDown);
            chocoboRed.Draw(viewPort, spriteBatch, new Vector2(500, 350), wallLeft, wallRight, wallUp, wallDown);
            chocoboBlue.Draw(viewPort, spriteBatch, new Vector2(100, 250), wallLeft, wallRight, wallUp, wallDown);
            chocoboGreen.Draw(viewPort, spriteBatch, new Vector2(750, 150), wallLeft, wallRight, wallUp, wallDown);
            chocoboBlack.Draw(viewPort, spriteBatch, new Vector2(0, 500), wallLeft, wallRight, wallUp, wallDown);
            chocoboGold.Draw(viewPort, spriteBatch, new Vector2(650, 450), wallLeft, wallRight, wallUp, wallDown);
            chocoboOrange.Draw(viewPort, spriteBatch, 
                new Vector2((int)(viewPort.Width - mog.Width) / 2, (int)(viewPort.Height - chocoboOrange.Height)), 
                wallLeft, wallRight, wallUp, wallDown);
            mog.Draw(viewPort, spriteBatch, 
                new Vector2((int)(viewPort.Width - mog.Width)/2, 100), 
                wallLeft, wallRight, wallUp, wallDown);
            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

