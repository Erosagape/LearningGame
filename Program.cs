﻿using System;

namespace LearningGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Lesson4())
                game.Run();
        }
    }
}
