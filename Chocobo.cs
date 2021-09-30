using System;
using System.Collections.Generic;
using System.Text;

namespace LearningGame
{
    public class Chocobo : SpriteCharacter
    {
        private Chocobo()
        {
            IsMove = false;
            SpriteCols = 12;
            SpriteRows = 8;
            AnimationSpeed = 20;
        }        
        public Chocobo(int X,int Y,int width,int height) :this()
        {
            OriginX = X;
            OriginY = Y;
            Width = width;
            Height = height;
        }
        public void SetSpeed(int walkSpeed,int runSpeed)
        {
            WalkSpeed = walkSpeed;
            RunSpeed = runSpeed;
        }
    }
}
