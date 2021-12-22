﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
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
            IsPlayer = false;
            IsAnimated = true;
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
        public override void SetCollision()
        {
            switch (this.Direction)
            {
                case SpriteDirection.MoveUp:
                    AdjustX = -1*(int)(this.Width * 0.2f);
                    AdjustY = -1 * (int)(this.Height * 0.1f);
                    AdjustWidth = (int)(this.Width * 0.3f);
                    AdjustHeight = (int)(this.Height * 0.1f);
                    break;
                case SpriteDirection.MoveDown:
                    AdjustX = -1*(int)(this.Width * 0.2f);
                    AdjustY = -1 * (int)(this.Height * 0.1f);
                    AdjustWidth = (int)(this.Width * 0.5f);
                    AdjustHeight = (int)(this.Height * 0.1f);
                    break;
                case SpriteDirection.MoveLeft:
                    AdjustY = -(int)(this.Width * 0.1f);
                    AdjustHeight = (int)(this.Height * 0.2f);
                    break;
                case SpriteDirection.MoveRight:
                    AdjustX = -(int)(this.Width * 0.2f);
                    AdjustY = -(int)(this.Width * 0.1f);
                    AdjustWidth = (int)(this.Width * 0.3f);
                    AdjustHeight = (int)(this.Height * 0.2f);
                    break;
            }
        }
    }
}
