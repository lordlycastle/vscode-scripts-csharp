using System;
using Scripts.DerivedClassesConundrum;

namespace Scripts
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello Darkness!");
            var useableBaseClass = BaseMVCContainer.Test();


            Console.ReadKey();
        }
    }
}