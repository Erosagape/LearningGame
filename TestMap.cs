using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningGame
{
    class TestMap : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        Texture2D backgroundSource;
        TileSet surface;
        Moogle mog = new Moogle(60, 70,"Mog",SpriteDirection.MoveDown);
        int frameCount;
        public TestMap()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            mog.CurrentPosition = new Point(0, 0);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            mog.SpriteSource = Content.Load<Texture2D>("moogle1");

            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("Fonts/defaultfont");

            backgroundSource = Content.Load<Texture2D>("tileset1");

            surface = new TileSet(backgroundSource);
            surface.CreateMap(50, 50);
            surface.TileMap.FrameWidth = 32;
            surface.TileMap.FrameHeight = 32;
            surface.TileMap.FillMap(new Tile(0, 0, 32, 32));
            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            if (frameCount > mog.AnimationSpeed)
                frameCount = 0;
            frameCount++;
            Point currentPos = mog.CurrentPosition;
            Input.UpdateCharacter(mog);
            foreach(Rectangle obj in surface.CollisionObjects)
            {
                if (mog.GetCollision().Intersects(obj))
                {
                    mog.CurrentPosition = currentPos;
                }
            }
            mog.Update(frameCount);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Chocolate);
            Viewport viewPort = graphics.GraphicsDevice.Viewport;
            spriteBatch.Begin();
            surface.DrawFloor(spriteBatch);
            surface.CollisionObjects.Clear();           

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

            surface.DrawObject(spriteBatch, 500, 300, 64, 64, new Tile(192, 32, 32, 32),true);

            Tile tree = new Tile(0, 32, 32, 32);
            surface.DrawObject(spriteBatch, 50, 100, 64, 64, tree,true);
            surface.DrawObject(spriteBatch, 70, 40, 32, 64, tree, true);
            surface.DrawObject(spriteBatch, 150, 320, 64, 32, tree, true);
            surface.DrawObject(spriteBatch, 170, 240, 50, 60, tree, true);
            surface.DrawObject(spriteBatch, 550, 450, 60, 50, tree, true);
            surface.DrawObject(spriteBatch, 660, 200, 45, 70, tree, true);
            surface.DrawObject(spriteBatch, 700, 400, 80, 60, tree, true);
            surface.DrawObject(spriteBatch, 600, 150, 60, 80, tree, true);
            surface.DrawObject(spriteBatch, 400, 200, 48, 64, tree, true);

            mog.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
