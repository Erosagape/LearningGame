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
        public Moogle(int width, int height,string name,SpriteDirection direction) : this(0, 0, width, height)
        {
            this.Name = name;
            this.Direction = direction;
            this.IsPlayer = false;
            this.IsAnimated = true;
        }
        public Moogle(int width,int height) : this(0,0,width,height)
        {
            this.Direction = SpriteDirection.MoveDown;
            this.IsPlayer = false;
            this.IsAnimated = true;
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
