using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningGame
{
    static class Input
    {
        public static KeyboardState previousKeyState;
        public static KeyboardState currentKeyState;
        public static void Update()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
        }
        public static bool IsKeyPress(Keys key)
        {
            return currentKeyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key);
        }
        public static bool IsKeyDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }
        public static bool IsKeyHold(Keys key)
        {
            return previousKeyState.IsKeyDown(key) && currentKeyState.IsKeyDown(key);
        }
        public static void UpdateCharacter(SpriteCharacter character)
        {
            //Check Face Angle 
            if (Input.IsKeyPress(Keys.Up) || Input.IsKeyPress(Keys.Down) || Input.IsKeyPress(Keys.Left) || Input.IsKeyPress(Keys.Right))
            {
                character.Animation = SpriteAnimation.Stand;
                if (Input.IsKeyPress(Keys.Up))
                {
                    character.Direction = SpriteDirection.MoveUp;
                }
                if (Input.IsKeyPress(Keys.Down))
                {
                    character.Direction = SpriteDirection.MoveDown;
                }
                if (Input.IsKeyPress(Keys.Left))
                {
                    character.Direction = SpriteDirection.MoveLeft;
                }
                if (Input.IsKeyPress(Keys.Right))
                {
                    character.Direction = SpriteDirection.MoveRight;
                }
            }
            character.IsMove = false;
            //Check Walking animation
            if (Input.IsKeyHold(Keys.W)
            || Input.IsKeyHold(Keys.S)
            || Input.IsKeyHold(Keys.A)
            || Input.IsKeyHold(Keys.D))
            {
                character.Run();
            }
            if (Input.IsKeyDown(Keys.W))
            {
                character.MoveUp();
            }
            if (Input.IsKeyDown(Keys.S))
            {
                character.MoveDown();
            }
            if (Input.IsKeyDown(Keys.A))
            {
                character.MoveLeft();
            }
            if (Input.IsKeyDown(Keys.D))
            {
                character.MoveRight();
            }

        } 
    }
}
