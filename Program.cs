using System;

namespace LearningGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Lesson5())
                game.Run();
        }
    }
}
