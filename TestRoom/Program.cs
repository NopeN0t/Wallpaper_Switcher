using BG_Lib;

namespace TestRoom
{
    internal class Program
    {
        public static BG_Switcher bglib = new();
        static void Main()
        {
            bglib.BG_Source = @"R:\TM";
            PrintImgs();

            Console.WriteLine("Base Shuffle");
            bglib.ShuffleImage();
            PrintImgs();
            Console.ReadLine();
            
            Console.WriteLine("Base Shuffle");
            bglib.ShuffleImage();
            PrintImgs();
            Console.ReadLine();

            Console.WriteLine("Reset");
            bglib.ShuffleImage(true);
            PrintImgs();
            Console.ReadLine();
        }
        static void PrintImgs()
        {
            foreach (var s in bglib.GetImages())
                Console.WriteLine(s);
        }
    }
}
