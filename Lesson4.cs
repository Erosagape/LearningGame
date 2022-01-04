using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace LearningGame
{
    public class Lesson4 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool isAnimated;
        Texture2D backgroundSource;
        TileSet surface;
        SpriteCharacter player;

        Chocobo chocoboYellow=new Chocobo(0,0,120,100);
        Chocobo chocoboWhite=new Chocobo(150,0,50,50);
        Chocobo chocoboRed=new Chocobo(300,0,100,80);
        Chocobo chocoboBlue=new Chocobo(450,0,120,80);
        Chocobo chocoboGreen = new Chocobo(0, 192, 60, 70);
        Chocobo chocoboBlack = new Chocobo(150, 192, 100, 90);
        Chocobo chocoboGold = new Chocobo(300, 192, 80, 90);
        Chocobo chocoboOrange = new Chocobo(450, 192, 90, 100);

        Moogle mog=new Moogle(0,0,60,70);

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

            chocoboWhite.Direction = SpriteDirection.MoveRight;
            chocoboBlue.Direction = SpriteDirection.MoveRight;
            chocoboBlack.Direction = SpriteDirection.MoveRight;
            chocoboGreen.Direction = SpriteDirection.MoveLeft;
            chocoboGold.Direction = SpriteDirection.MoveLeft;            
            chocoboRed.Direction = SpriteDirection.MoveLeft;
            chocoboOrange.Direction = SpriteDirection.MoveUp;

            mog.Direction = SpriteDirection.MoveDown;

            player = mog;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            backgroundSource = Content.Load<Texture2D>("tileset1");

            mog.SpriteSource = Content.Load<Texture2D>("moogle1");

            chocoboYellow.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboWhite.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboRed.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboBlue.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboGreen.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboBlack.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboGold.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboOrange.SpriteSource = Content.Load<Texture2D>("chocobo");

            surface = new TileSet(backgroundSource);
            surface.CreateMap(50, 50);
            surface.TileMap.FrameWidth = 32;
            surface.TileMap.FrameHeight = 32;
            surface.TileMap.FillMap(new Tile(0, 0, 32, 32));

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
                if (player.Equals(mog))
                {
                    player = chocoboYellow;
                }
                else if (player.Equals(chocoboYellow))
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

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);
            Viewport viewPort = graphics.GraphicsDevice.Viewport;

            spriteBatch.Begin();

            surface.DrawFloor(spriteBatch);

            Tile grass = new Tile(32, 0, 32, 32);
            List<Point> pos = new List<Point>();
            for(int i = 200; i <= 600; i += 32)
            {
                for (int j = 200; j <= 600; j += 32)
                {
                    pos.Add(new Point(i, j));
                }
            }
            surface.DrawObjects(spriteBatch, pos.ToArray(), 32, 32, grass);

            Tile tree1 = new Tile(96, 32, 32, 32);
            List<Point> positions = new List<Point>() { 
                new Point(550,100),
                new Point(470,40),
                new Point(490,520),
                new Point(350,420),
                new Point(320,50),
                new Point(260,200),
                new Point(300,400),
                new Point(200,150)
            };            
            surface.DrawObjects(spriteBatch, positions.ToArray(), 64, 64,tree1);
                        
            surface.DrawObject(spriteBatch, 500, 300, 64, 64, new Tile(192, 32, 32, 32));

            chocoboYellow.DrawCenter(viewPort, spriteBatch);            
            chocoboWhite.Draw(viewPort, spriteBatch, new Vector2(50,0));
            chocoboRed.Draw(viewPort, spriteBatch, new Vector2(500, 350));
            chocoboBlue.Draw(viewPort, spriteBatch, new Vector2(0, 250));
            chocoboGreen.Draw(viewPort, spriteBatch, new Vector2(750, 150));
            chocoboBlack.Draw(viewPort, spriteBatch, new Vector2(0, 500));
            chocoboGold.Draw(viewPort, spriteBatch, new Vector2(650, 450));
            chocoboOrange.Draw(viewPort, spriteBatch, 
                new Vector2((int)(viewPort.Width - mog.Width) / 2, (int)(viewPort.Height - chocoboOrange.Height)-50) 
                );
            
            mog.Draw(viewPort, spriteBatch, 
                new Vector2((int)(viewPort.Width - mog.Width)/2, 100) 
                );

            Tile tree2 = new Tile(0, 32, 32, 32);
            surface.DrawObject(spriteBatch, 50, 100, 64, 64, tree2);
            surface.DrawObject(spriteBatch, 70, 40, 32, 64, tree2);
            surface.DrawObject(spriteBatch, 150, 320, 64, 32, tree2);
            surface.DrawObject(spriteBatch, 170, 240, 50, 60, tree2);
            surface.DrawObject(spriteBatch, 550, 450, 60, 50, tree2);
            surface.DrawObject(spriteBatch, 660, 200, 45, 70, tree2);
            surface.DrawObject(spriteBatch, 700, 400, 80, 60, tree2);
            surface.DrawObject(spriteBatch, 600, 150, 60, 80, tree2);
            surface.DrawObject(spriteBatch, 400, 200, 48, 64, tree2);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}



