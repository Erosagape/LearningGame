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
        SpriteCharacter chocoboYellow;

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
            chocoboYellow.IsMove = false;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            backgroundSource = Content.Load<Texture2D>("titlescreen");
            chocoboYellow.SpriteSource = Content.Load<Texture2D>("chocobo");
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
        private void ChangeAnimation(ref SpriteAnimation animation)
        {
            switch (animation)
            {
                case SpriteAnimation.Stand:
                    animation = SpriteAnimation.WalkLeft;
                    break;
                case SpriteAnimation.WalkLeft:
                    animation = SpriteAnimation.WalkRight;
                    break;
                case SpriteAnimation.WalkRight:
                    animation = SpriteAnimation.Stand;
                    break;
            }
        }
        private void ChangeDirection(ref SpriteDirection direction)
        {
            switch (direction)
            {
                case SpriteDirection.MoveUp:
                    direction = SpriteDirection.MoveRight;
                    break;
                case SpriteDirection.MoveRight:
                    direction = SpriteDirection.MoveDown;
                    break;
                case SpriteDirection.MoveDown:
                    direction = SpriteDirection.MoveLeft;
                    break;
                case SpriteDirection.MoveLeft:
                    direction = SpriteDirection.MoveUp;
                    break;
            }
        }
        private void UpdateDirection(ref SpriteAnimation animation, ref SpriteDirection direction)
        {
            if (IsKeyPress(Keys.Up) || IsKeyPress(Keys.Down) || IsKeyPress(Keys.Left) || IsKeyPress(Keys.Right))
            {
                animation = SpriteAnimation.Stand;
                if (IsKeyPress(Keys.Up))
                {
                    direction = SpriteDirection.MoveUp;
                }
                if (IsKeyPress(Keys.Down))
                {
                    direction = SpriteDirection.MoveDown;
                }
                if (IsKeyPress(Keys.Left))
                {
                    direction = SpriteDirection.MoveLeft;
                }
                if (IsKeyPress(Keys.Right))
                {
                    direction = SpriteDirection.MoveRight;
                }
            }
            if (IsKeyPress(Keys.Space))
            {
                ChangeDirection(ref direction);
            }
        }
        private bool UpdateMovement(ref Vector2 position, ref SpriteDirection direction, ref int walkSpeed, ref int runSpeed)
        {
            bool isMove = false;
            if (IsKeyHold(Keys.W)
            || IsKeyHold(Keys.S)
            || IsKeyHold(Keys.A)
            || IsKeyHold(Keys.D))
            {
                walkSpeed += runSpeed;
            }
            if (IsKeyDown(Keys.W))
            {
                direction = SpriteDirection.MoveUp;
                position.Y -= walkSpeed;
                isMove = true;
            }
            if (IsKeyDown(Keys.S))
            {
                direction = SpriteDirection.MoveDown;
                position.Y += walkSpeed;
                isMove = true;
            }
            if (IsKeyDown(Keys.A))
            {
                direction = SpriteDirection.MoveLeft;
                position.X -= walkSpeed;
                isMove = true;
            }
            if (IsKeyDown(Keys.D))
            {
                direction = SpriteDirection.MoveRight;
                position.X += walkSpeed;
                isMove = true;
            }
            return isMove;
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

            chocoboYellow.WalkSpeed = 1;
            chocoboYellow.RunSpeed = 4;
        }
        private void UpdateAnimation()
        {
            UpdateDirection(ref chocoboYellow.Animation, ref chocoboYellow.Direction);
            chocoboYellow.IsMove = UpdateMovement(
                ref chocoboYellow.SpritePosition, ref chocoboYellow.Direction,
                ref chocoboYellow.WalkSpeed, ref chocoboYellow.RunSpeed);

            if (chocoboYellow.IsMove)
            {
                ChangeAnimation(ref chocoboYellow.Animation);
                chocoboYellow.IsMove = false;
            }
            else
            {
                if (isAnimated)
                {
                    if (frameCount % 20 == 0)
                    {
                        ChangeAnimation(ref chocoboYellow.Animation);
                        frameCount = 0;
                    }
                }
                else
                {
                    chocoboYellow.Animation = SpriteAnimation.Stand;
                }
            }
            UpdateCharacter();
        }
        private void UpdateCharacter()
        {
            int posX = (int)(chocoboYellow.SpriteSource.Width / 12);
            int posY = (int)(chocoboYellow.SpriteSource.Height / 8);

            chocoboYellow.SpriteRectangle = new Rectangle(
                posX * (int)chocoboYellow.Animation, posY * (int)chocoboYellow.Direction,
                posX, posY
                );
        }
        protected override void Update(GameTime gameTime)
        {
            UpdateState(gameTime.TotalGameTime);
            UpdateAnimation();

            base.Update(gameTime);
        }
        private void DrawCharacter(Viewport vp, Texture2D charSource, Rectangle charRect, int charWidth, int charHeight, Vector2 charPos)
        {
            int spaceWidth = (int)(vp.Width - charWidth);
            int spaceHeight = (int)(vp.Height - charHeight);

            int calPositionX = (spaceWidth / 2) + (int)charPos.X;
            int calPositionY = (spaceHeight / 2) + (int)charPos.Y;

            int charPositionX = MathHelper.Clamp(calPositionX, wallLeft, spaceWidth - wallRight);
            int charPositionY = MathHelper.Clamp(calPositionY, wallUp, spaceHeight - wallDown);

            Rectangle frameRect = new Rectangle(
                charPositionX, charPositionY,
                charWidth,
                charHeight
                );

            spriteBatch.Draw(charSource, frameRect, charRect, Color.White);
        }
        private void DrawBackGround(Viewport vp)
        {
            wallLeft = 10;
            wallRight = 10;
            wallUp = 100;
            wallDown = 10;

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
            DrawCharacter(
                viewPort,
                chocoboYellow.SpriteSource,
                chocoboYellow.SpriteRectangle,
                120, 100,
                chocoboYellow.SpritePosition
                );

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
