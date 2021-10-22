using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningGame
{
    public class Moogle : SpriteCharacter
    {        
        private Moogle()
        {
            IsMove = false;
            SpriteCols = 3;
            SpriteRows = 4;
            AnimationSpeed = 10;
        }
        public void SetCollision()
        {
            CollisionRectangle = new Rectangle(
                CurrentPosition.X+10, CurrentPosition.Y+15,
                Width-20,
                Height-20
            );
        }
        public Moogle(int X, int Y, int width, int height) : this()
        {
            OriginX = X;
            OriginY = Y;
            Width = width;
            Height = height;
        }
        public void SetSpeed(int walkSpeed, int runSpeed)
        {
            WalkSpeed = walkSpeed;
            RunSpeed = runSpeed;
        }
    }
}
