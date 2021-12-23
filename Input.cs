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
        BackButtonKeyDown,
        BackButtonKeyPress,
        SkillButtonKeyDown,
        SkillButtonKeyPress,
        MenuButtonKeyDown,
        MenuButtonKeyPress,
        StartButtonKeyDown,
        StartButtonKeyPress,
        SelectButtonKeyDown,
        SelectButtonKeyPress,        
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
        public static void Reset()
        {
            directionState = DirectionState.NoDirection;
            actionState = ActionState.NoAction;
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
        public static DirectionState CurrentDirection => directionState;
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
