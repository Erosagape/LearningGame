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

        Texture2D conversationBorderSource;
        Texture2D conversationSource;
        Rectangle conversationBorderBox;
        Rectangle conversationBox;

        TileSet surface;
        
        SpriteCharacter player;
        SpriteCharacter[] npcs;

        int frameCount;

        bool isShowDialog = false;
        public Lesson5()
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
            player = new Moogle(60,70,"Mog",SpriteDirection.MoveDown);
            player.IsPlayer = true;
            player.CurrentPosition.X = 50;
            player.CurrentPosition.Y = 50;

            npcs = new SpriteCharacter[8];
            npcs[0] = new Chocobo(0, 0, 120, 100, "YellowChoco", SpriteDirection.MoveDown, new Point(0, 0));
            npcs[1] = new Chocobo(150, 0, 50, 50, "WhiteChoco", SpriteDirection.MoveRight, new Point(120, 360));
            npcs[2] = new Chocobo(300, 0, 100, 80, "RedChoco", SpriteDirection.MoveLeft, new Point(455, 30));
            npcs[3] = new Chocobo(450, 0, 120, 80, "BlueChoco", SpriteDirection.MoveRight, new Point(0, 200));
            npcs[4] = new Chocobo(0, 192, 60, 70, "GreenChoco", SpriteDirection.MoveLeft, new Point(750, 150));
            npcs[5] = new Chocobo(150, 192, 100, 90, "BlackChoco", SpriteDirection.MoveRight, new Point(300, 48));
            npcs[6] = new Chocobo(300, 192, 80, 90, "GoldChoco", SpriteDirection.MoveLeft, new Point(600, 250));
            npcs[7] = new Chocobo(450, 192, 90, 100, "OrangeChoco", SpriteDirection.MoveUp, new Point(400, 350));       

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


            foreach(SpriteCharacter npc in npcs)
                npc.SpriteSource=Content.Load<Texture2D>("chocobo");

            player.SpriteSource = Content.Load<Texture2D>("moogle1");

            conversationSource = new Texture2D(graphics.GraphicsDevice, 1, 1);
            conversationSource.SetData(new Color[] { Color.DarkBlue });
            conversationBorderSource = new Texture2D(graphics.GraphicsDevice, 1, 1);
            conversationBorderSource.SetData(new Color[] { Color.White });
            conversationBox = new Rectangle(10, 10, 780, 100);
            conversationBorderBox = new Rectangle(5, 5, 790, 110);

            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            frameCount++;
            Vector2 currentPos = new Vector2(player.CurrentPosition.X, player.CurrentPosition.Y);
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

            isShowDialog = false;
            bool isCollide = false;
            player.InteractionTo = "";
            player.IsAnimated = true;
            foreach (SpriteCharacter npc in npcs)
            {
                if (player.IsCollide(npc))
                {
                    isCollide = true;
                    npc.IsAnimated = false;
                    player.IsAnimated = false;
                    player.InteractionTo = player.IsFaceToFace(npc) ? npc.Name: "";
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
            isShowDialog = (player.InteractionTo != "");

            player.Update(frameCount);
            foreach (SpriteCharacter npc in npcs)
                npc.Update(frameCount);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Chocolate);
            Viewport viewPort = graphics.GraphicsDevice.Viewport;                        
            spriteBatch.Begin();

            surface.DrawFloor(spriteBatch);
            surface.DrawObject(spriteBatch, 500, 300, 64, 64, new Tile(192, 32, 32, 32));

            player.Draw(spriteBatch);

            npcs[0].DrawCenter(viewPort, spriteBatch);
            foreach (SpriteCharacter npc in npcs)
                npc.Draw(spriteBatch);

            if (isShowDialog)
            {
                spriteBatch.Draw(conversationBorderSource, conversationBorderBox, Color.White);
                spriteBatch.Draw(conversationSource, conversationBox, Color.White);
                spriteBatch.DrawString(
                    spriteFont,
                    player.InteractionTo + " : Kweh!, Hello " + player.Name + "!",
                    new Vector2(20, 20),
                    Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
