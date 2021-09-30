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
