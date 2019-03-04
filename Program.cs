using System;
// using Scripts;

namespace Scripts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var u_baseclass = new UseableBaseClass();
            var u_derivedclass = new UseableDerivedClass();
            // var derivedclass = new DerivedClass();
            var type = u_derivedclass.GetType();
            u_baseclass.AddChild(u_derivedclass);
            
        }
    }
}
