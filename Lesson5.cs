using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace LearningGame
{
    public class Lesson5 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool isAnimated;
        Texture2D backgroundSource;
        TileSet surface;
        SpriteCharacter player;

        Chocobo chocoboYellow = new Chocobo(0, 0, 120, 100);
        Chocobo chocoboWhite = new Chocobo(150, 0, 50, 50);
        Chocobo chocoboRed = new Chocobo(300, 0, 100, 80);
        Chocobo chocoboBlue = new Chocobo(450, 0, 120, 80);
        Chocobo chocoboGreen = new Chocobo(0, 192, 60, 70);
        Chocobo chocoboBlack = new Chocobo(150, 192, 100, 90);
        Chocobo chocoboGold = new Chocobo(300, 192, 80, 90);
        Chocobo chocoboOrange = new Chocobo(450, 192, 90, 100);

        List<SpriteCharacter> npcs = new List<SpriteCharacter>();

        Moogle mog = new Moogle(0, 0, 60, 70);

        int totalSeconds;
        int totalFrames;
        int framePerSeconds;
        int frameCount;

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
            isAnimated = true;

            chocoboWhite.Direction = SpriteDirection.MoveRight;
            chocoboBlue.Direction = SpriteDirection.MoveRight;
            chocoboBlack.Direction = SpriteDirection.MoveRight;
            chocoboGreen.Direction = SpriteDirection.MoveLeft;
            chocoboGold.Direction = SpriteDirection.MoveLeft;
            chocoboRed.Direction = SpriteDirection.MoveLeft;
            chocoboOrange.Direction = SpriteDirection.MoveUp;

            mog.Direction = SpriteDirection.MoveDown;
            mog.IsPlayer = true;
            player = mog;

            npcs = new List<SpriteCharacter>();
            npcs.Add(mog);
            npcs.Add(chocoboYellow);
            npcs.Add(chocoboGold);
            npcs.Add(chocoboOrange);
            npcs.Add(chocoboRed);
            npcs.Add(chocoboGreen);
            npcs.Add(chocoboWhite);
            npcs.Add(chocoboBlue);
            npcs.Add(chocoboBlack);

            UpdateNPC();
            base.Initialize();
        }
        protected void UpdateNPC()
        {
            foreach (SpriteCharacter npc in npcs)
            {
                npc.IsPlayer = npc.Equals(player);
            }
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
        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            if (Input.IsKeyPress(Keys.Escape))
            {
                Exit();
            }
            if (Input.IsKeyPress(Keys.Enter))
            {
                isAnimated = !isAnimated;
            }
            frameCount += 1;

            if (Input.IsKeyPress(Keys.Tab))
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
                    player = mog;
                }
                UpdateNPC();
            }
            if (Input.IsKeyPress(Keys.Space))
            {
                player.ChangeDirection();
            }
            if (Input.IsKeyHold(Keys.W)
            || Input.IsKeyHold(Keys.S)
            || Input.IsKeyHold(Keys.A)
            || Input.IsKeyHold(Keys.D))
            {
                player.Run();
            }
            bool isCollide = false;
            if (Input.IsKeyPress(Keys.Up))
            {
                player.Direction = SpriteDirection.MoveUp;
            }
            if (Input.IsKeyPress(Keys.Down))
            {
                player.Direction = SpriteDirection.MoveDown;
            }
            if (Input.IsKeyPress(Keys.Left))
            {
                player.Direction = SpriteDirection.MoveLeft;
            }
            if (Input.IsKeyPress(Keys.Right))
            {
                player.Direction = SpriteDirection.MoveRight;
            }
            if (Input.IsKeyDown(Keys.W))
            {
                player.MoveUp();
            }
            if (Input.IsKeyDown(Keys.S))
            {
                player.MoveDown();
            }
            if (Input.IsKeyDown(Keys.A))
            {
                player.MoveLeft();
            }
            if (Input.IsKeyDown(Keys.D))
            {
                player.MoveRight();
            }

            if (!player.SpritePosition.Equals(Vector2.Zero))
            {
                foreach(var npc in npcs)
                {
                    if (!npc.IsPlayer)
                    {
                        if (npc.CollisionRectangle.Intersects(player.CollisionRectangle))
                        {
                            isCollide = true;
                        }
                    }
                }
            }
            if (isCollide)
            {
                switch (player.Direction)
                {
                    case SpriteDirection.MoveUp:
                        player.SpritePosition.Y += player.WalkSpeed;
                        break;
                    case SpriteDirection.MoveDown:
                        player.SpritePosition.Y -= player.WalkSpeed;
                        break;
                    case SpriteDirection.MoveLeft:
                        player.SpritePosition.X += player.WalkSpeed;
                        break;
                    case SpriteDirection.MoveRight:
                        player.SpritePosition.X -= player.WalkSpeed;
                        break;
                }

            }
            mog.IsAnimated = true;
            mog.ShowCollision = true;
            mog.SetCollision();
            mog.SetSpeed(2, 1);

            chocoboYellow.IsAnimated = isAnimated;
            chocoboYellow.ShowCollision = true;
            chocoboYellow.SetCollision();
            chocoboYellow.SetSpeed(1, 4);

            chocoboWhite.IsAnimated = true;
            chocoboWhite.ShowCollision = true;
            chocoboWhite.SetCollision();
            chocoboWhite.SetSpeed(2, 3);

            chocoboRed.IsAnimated = false;
            chocoboRed.ShowCollision = true;
            chocoboRed.SetCollision();
            chocoboRed.SetSpeed(3, 2);

            chocoboBlue.IsAnimated = !isAnimated;
            chocoboBlue.ShowCollision = true;
            chocoboBlue.SetCollision();
            chocoboBlue.SetSpeed(4, 1);

            chocoboGreen.IsAnimated = !isAnimated;
            chocoboGreen.ShowCollision = true;
            chocoboGreen.SetCollision();
            chocoboGreen.SetSpeed(2, 4);

            chocoboBlack.IsAnimated = false;
            chocoboBlack.ShowCollision = true;
            chocoboBlack.SetCollision();
            chocoboBlack.SetSpeed(3, 3);

            chocoboGold.IsAnimated = true;
            chocoboGold.ShowCollision = true;
            chocoboGold.SetCollision();
            chocoboGold.SetSpeed(4, 2);

            chocoboOrange.IsAnimated = isAnimated;
            chocoboOrange.ShowCollision = true;
            chocoboOrange.SetCollision();
            chocoboOrange.SetSpeed(1, 2);
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
            Window.Title = "FPS:" + framePerSeconds + " MODE: " + (isAnimated ? "JOGGING" : "STANDING");
            Window.Title += " Player=" + player.CurrentPosition.X + "/" + player.CurrentPosition.Y;

            graphics.GraphicsDevice.Clear(Color.White);
            Viewport viewPort = graphics.GraphicsDevice.Viewport;

            spriteBatch.Begin();

            surface.DrawFloor(spriteBatch);

            Tile grass = new Tile(32, 0, 32, 32);
            List<Point> pos = new List<Point>();
            for (int i = 200; i <= 600; i += 32)
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
            surface.DrawObjects(spriteBatch, positions.ToArray(), 64, 64, tree1);

            surface.DrawObject(spriteBatch, 500, 300, 64, 64, new Tile(192, 32, 32, 32));

            chocoboYellow.DrawCenter(viewPort, spriteBatch);
            chocoboWhite.Draw(viewPort, spriteBatch, new Vector2(50, 0));
            chocoboRed.Draw(viewPort, spriteBatch, new Vector2(500, 350));
            chocoboBlue.Draw(viewPort, spriteBatch, new Vector2(0, 250));
            chocoboGreen.Draw(viewPort, spriteBatch, new Vector2(750, 150));
            chocoboBlack.Draw(viewPort, spriteBatch, new Vector2(0, 500));
            chocoboGold.Draw(viewPort, spriteBatch, new Vector2(650, 450));
            chocoboOrange.Draw(viewPort, spriteBatch,
                new Vector2((int)(viewPort.Width - mog.Width) / 2, (int)(viewPort.Height - chocoboOrange.Height) - 50)
                );

            mog.Draw(viewPort, spriteBatch,
                new Vector2((int)(viewPort.Width - mog.Width) / 2, 100)
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
