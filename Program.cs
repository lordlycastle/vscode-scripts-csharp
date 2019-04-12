using System;
using System.Linq;

namespace Scripts
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello Darkness!");
//            var useableBaseClass = BaseMVCContainer.Test();
//            var classObj = MyClass.Test();

            Console.Write("Get all IEnumerable: ");
            WeekDaysEnum.GetAll().ToList().ForEach(e => Console.WriteLine(e.ToString()));
            foreach (var option in WeekDaysEnum._) Console.WriteLine($"Instance Enumerator: {option}");

            try
            {
                var _ = (WeekDaysEnum) "Funday";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Invalid cast test: {e.Message}");
            }

            var myStringEnum = (WeekDaysEnum) "Mon";
            var otherEnum = (string) WeekDaysEnum.Tuesday;
            var anotherEnum = WeekDaysEnum._[1];
            Console.WriteLine($"Parsed in from (string, TEnum, int): {myStringEnum}, {otherEnum}, {anotherEnum}");
            Console.WriteLine($"To String: {WeekDaysEnum.Monday}");
            Console.WriteLine($"CompareTo String (true): {WeekDaysEnum.Tuesday == "Tuesday"}");
            Console.WriteLine($"CompareTo String (true): {"Value 1" == WeekDaysEnum.Monday}");
            Console.WriteLine($"CompareTo Enum (false): {WeekDaysEnum.Tuesday == WeekDaysEnum.Monday}");
            Console.WriteLine($"Val 1 < Val 2 (true): {WeekDaysEnum.Monday < WeekDaysEnum.Tuesday}");
            Console.WriteLine($"Val 1 > Val 2 (false): {"Mon" > WeekDaysEnum.Tuesday}");
            Console.WriteLine($"Cast ToInt(): {WeekDaysEnum.ToInt(WeekDaysEnum.Monday)}");
            Console.WriteLine($"Cast to int (1): {(int) WeekDaysEnum.Monday}");
            Console.WriteLine($"Cast from int (Monday): {(WeekDaysEnum) 1}");
            Console.WriteLine($"Cast to string (Monday): {(string) WeekDaysEnum.Monday}");
            Console.WriteLine($"Cast from string (Monday): {(WeekDaysEnum) "Monday"}");
            // Compile error.
//            Console.WriteLine($"Compare between enums (false): {ShapesEnum.Line == WeekDaysEnum.Monday}");
            Console.WriteLine($"Hash Code: {WeekDaysEnum.Monday.GetHashCode()}");

//            Console.ReadKey();
        }
    }
}