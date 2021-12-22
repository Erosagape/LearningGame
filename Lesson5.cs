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
        SpriteFont spriteFont;

        Texture2D backgroundSource;
        TileSet surface;

        Chocobo chocoboYellow = new Chocobo(0, 0, 120, 100);
        Chocobo chocoboWhite = new Chocobo(150, 0, 50, 50);
        Chocobo chocoboRed = new Chocobo(300, 0, 100, 80);
        Chocobo chocoboBlue = new Chocobo(450, 0, 120, 80);
        Chocobo chocoboGreen = new Chocobo(0, 192, 60, 70);
        Chocobo chocoboBlack = new Chocobo(150, 192, 100, 90);
        Chocobo chocoboGold = new Chocobo(300, 192, 80, 90);
        Chocobo chocoboOrange = new Chocobo(450, 192, 90, 100);
        Moogle mog = new Moogle(60, 70);

        SpriteCharacter player;
        SpriteCharacter[] npcs;
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
            /*
            mog.ShowCollision = true;
            chocoboYellow.ShowCollision = true;
            chocoboWhite.ShowCollision = true;
            chocoboBlue.ShowCollision = true;
            chocoboBlack.ShowCollision = true;
            chocoboGreen.ShowCollision = true;
            chocoboGold.ShowCollision = true;
            chocoboRed.ShowCollision = true;
            chocoboOrange.ShowCollision = true;
            */
            mog.Name = "Mog";
            chocoboYellow.Name= "BananaChoc";
            chocoboWhite.Name = "WhiteChoc";
            chocoboBlue.Name = "SkyChoc";
            chocoboBlack.Name = "DarkChoc";
            chocoboGreen.Name = "GreenChoc";
            chocoboGold.Name = "GoldChoc";
            chocoboRed.Name = "RedChoc";
            chocoboOrange.Name = "OrangeChoc";

            chocoboWhite.Direction = SpriteDirection.MoveRight;
            chocoboBlue.Direction = SpriteDirection.MoveRight;
            chocoboBlack.Direction = SpriteDirection.MoveRight;
            chocoboGreen.Direction = SpriteDirection.MoveLeft;
            chocoboGold.Direction = SpriteDirection.MoveLeft;
            chocoboRed.Direction = SpriteDirection.MoveLeft;
            chocoboOrange.Direction = SpriteDirection.MoveUp;

            player = mog;
            player.IsPlayer = true;
            player.CurrentPosition.X = 50;
            player.CurrentPosition.Y = 50;

            npcs = new SpriteCharacter[8];
            npcs[0] = chocoboYellow;
            npcs[1] = chocoboWhite;
            npcs[2] = chocoboRed;
            npcs[3] = chocoboBlue;
            npcs[4] = chocoboGreen;
            npcs[5] = chocoboBlack;
            npcs[6] = chocoboGold;
            npcs[7] = chocoboOrange;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("Fonts/defaultfont");

            backgroundSource = Content.Load<Texture2D>("tileset1");

            surface = new TileSet(backgroundSource);
            surface.CreateMap(50, 50);
            surface.TileMap.FrameWidth = 32;
            surface.TileMap.FrameHeight = 32;
            surface.TileMap.FillMap(new Tile(0, 0, 32, 32));

            chocoboYellow.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboWhite.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboRed.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboBlue.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboGreen.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboBlack.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboGold.SpriteSource = Content.Load<Texture2D>("chocobo");
            chocoboOrange.SpriteSource = Content.Load<Texture2D>("chocobo");
            
            player.SpriteSource = Content.Load<Texture2D>("moogle1");

            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            frameCount++;
            Vector2 currentPos = new Vector2(player.CurrentPosition.X,player.CurrentPosition.Y);
            if (Input.IsUpDirection())
            {                
                player.ChangeDirection(SpriteDirection.MoveUp);
                player.CurrentPosition.Y -= 1;
            }

            if (Input.IsDownDirection())
            {
                player.ChangeDirection(SpriteDirection.MoveDown);
                player.CurrentPosition.Y += 1;
            }

            if (Input.IsLeftDirection())
            {
                player.ChangeDirection(SpriteDirection.MoveLeft);
                player.CurrentPosition.X -= 1;
            }

            if (Input.IsRightDirection())
            {
                player.ChangeDirection(SpriteDirection.MoveRight);
                player.CurrentPosition.X += 1;
            }

            player.CurrentPosition.X = MathHelper.Clamp(player.CurrentPosition.X, 0, graphics.GraphicsDevice.Viewport.Width - player.Width);
            player.CurrentPosition.Y = MathHelper.Clamp(player.CurrentPosition.Y, 0, graphics.GraphicsDevice.Viewport.Height - player.Height);

            bool isCollide = false;
            player.InteractionTo = "N/A";
            foreach(SpriteCharacter npc in npcs)
            {
                if (player.IsCollide(npc))
                {
                    isCollide = true;
                    player.InteractionTo = npc.Name;                    
                }
                else
                {
                    npc.IsAnimated = true;
                }
            }
            if (isCollide)
            {
                player.CurrentPosition = currentPos.ToPoint();
            }          

            player.Update(frameCount);
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
            graphics.GraphicsDevice.Clear(Color.Chocolate);
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

            player.Draw(spriteBatch);
            chocoboYellow.DrawCenter(viewPort, spriteBatch);
            chocoboWhite.Draw(viewPort, spriteBatch, new Vector2(120, 0));
            chocoboRed.Draw(viewPort, spriteBatch, new Vector2(500, 350));
            chocoboBlue.Draw(viewPort, spriteBatch, new Vector2(0, 200));
            chocoboGreen.Draw(viewPort, spriteBatch, new Vector2(750, 150));
            chocoboBlack.Draw(viewPort, spriteBatch, new Vector2(0, 500));
            chocoboGold.Draw(viewPort, spriteBatch, new Vector2(600, 450));
            chocoboOrange.Draw(viewPort, spriteBatch,
                new Vector2((int)(viewPort.Width - mog.Width) / 2-20, (int)(viewPort.Height - chocoboOrange.Height)-20)
                );
            
            spriteBatch.DrawString(
                spriteFont,
                "Key=" + Input.CurrentDirection + " With=" + player.InteractionTo,
                Vector2.Zero,
                Color.Black);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
