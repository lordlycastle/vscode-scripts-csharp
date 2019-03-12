using System;
using Scripts.Properties;

namespace Scripts
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello Darkness!");
//            var useableBaseClass = BaseMVCContainer.Test();
            var classObj = MyClass.Test();

            Console.ReadKey();
        }
    }
}