using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace LearningGame
{
    public enum SpriteAnimation
    {
        WalkRight = 0, Stand = 1, WalkLeft = 2
    }
    public enum SpriteDirection
    {
        MoveDown = 0, MoveLeft = 1, MoveRight = 2, MoveUp = 3
    }
    public struct SpriteCharacter
    {
        public Texture2D SpriteSource;
        public Rectangle SpriteRectangle;
        public Vector2 SpritePosition;
        public SpriteAnimation Animation;
        public SpriteDirection Direction;
        public int WalkSpeed;
        public int RunSpeed;
        public bool IsMove;
    }
}