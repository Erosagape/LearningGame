using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningGame
{
    public enum ActionState
    {
        NoAction,
        ActionButtonKeyDown,
        ActionButtonKeyPress,
    }
    public enum DirectionState
    {
        NoDirection,
        LeftButtonKeyDown,
        RightButtonKeyDown,
        UpButtonKeyDown,
        DownButtonKeyDown,
        LeftButtonKeyPress,
        RightButtonKeyPress,
        UpButtonKeyPress,
        DownButtonKeyPress,
        LeftButtonKeyHold,
        RightButtonKeyHold,
        UpButtonKeyHold,
        DownButtonKeyHold,
    }
    static class Input
    {
        private static KeyboardState previousKeyState;
        private static KeyboardState currentKeyState;
        private static DirectionState directionState;
        private static ActionState actionState;
        public static DirectionState CurrentDirection => directionState;
        public static void Reset()
        {
            directionState = DirectionState.NoDirection;
            actionState = ActionState.NoAction;
        }
        public static void UpdateCharacter(SpriteCharacter character)
        {
            character.SpritePosition = Microsoft.Xna.Framework.Vector2.Zero;
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
            if (IsKeyPress(Keys.Enter))
            {
                character.IsAnimated = !character.IsAnimated;
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
            character.CurrentPosition = character.CurrentPosition+ character.SpritePosition.ToPoint();
        }
        public static void Update()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            if (Input.IsKeyPress(Keys.W))
            {
                directionState = DirectionState.UpButtonKeyPress;
                Input.Reset();
            }
            if (Input.IsKeyPress(Keys.S))
            {
                directionState = DirectionState.DownButtonKeyPress;
                Input.Reset();
            }
            if (Input.IsKeyPress(Keys.A))
            {
                directionState = DirectionState.LeftButtonKeyPress;
                Input.Reset();
            }
            if (Input.IsKeyPress(Keys.D))
            {
                directionState = DirectionState.RightButtonKeyPress;
                Input.Reset();
            }
            if (Input.IsKeyPress(Keys.Enter))
            {
                actionState = ActionState.ActionButtonKeyPress;
            }
            if (Input.IsKeyDown(Keys.W))
            {
                directionState = DirectionState.UpButtonKeyDown;
            }
            if (Input.IsKeyDown(Keys.S))
            {
                directionState = DirectionState.DownButtonKeyDown;
            }
            if (Input.IsKeyDown(Keys.A))
            {
                directionState = DirectionState.LeftButtonKeyDown;
            }
            if (Input.IsKeyDown(Keys.D))
            {
                directionState = DirectionState.RightButtonKeyDown;
            }
            if (Input.IsKeyDown(Keys.Enter))
            {
                actionState = ActionState.ActionButtonKeyDown;
            }
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
        public static bool IsUpDirection()
        {
            return directionState == DirectionState.UpButtonKeyDown ||
                directionState == DirectionState.UpButtonKeyPress ||
                directionState == DirectionState.UpButtonKeyHold;
        }
        public static bool IsDownDirection()
        {
            return directionState == DirectionState.DownButtonKeyDown ||
                directionState == DirectionState.DownButtonKeyPress ||
                directionState == DirectionState.DownButtonKeyHold;
        }
        public static bool IsLeftDirection()
        {
            return directionState == DirectionState.LeftButtonKeyDown ||
                directionState == DirectionState.LeftButtonKeyPress ||
                directionState == DirectionState.LeftButtonKeyHold;
        }
        public static bool IsRightDirection()
        {
            return directionState == DirectionState.RightButtonKeyDown ||
                directionState == DirectionState.RightButtonKeyPress ||
                directionState == DirectionState.RightButtonKeyHold;
        }
        public static bool IsActionButtonPressed()
        {
            return actionState==ActionState.ActionButtonKeyDown ||
                actionState== ActionState.ActionButtonKeyPress;
        }
    }
}
