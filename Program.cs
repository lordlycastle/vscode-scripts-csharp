using System;
using System.Collections.Generic;
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
                Console.WriteLine($"Invalid cast test: {e}");
            }

            var myStringEnum = (WeekDaysEnum) "Modafan";
            var otherEnum = (string) WeekDaysEnum.Tuesday;
            var anotherEnum = WeekDaysEnum._[1];
            Console.WriteLine($"Parsed in from (string, TEnum): {myStringEnum}, {otherEnum}");
            Console.WriteLine($"To String: {WeekDaysEnum.Monday}");
            Console.WriteLine($"CompareTo String (true): {WeekDaysEnum.Tuesday == "Tuesday"}");
            Console.WriteLine($"CompareTo String (true): {"Value 1" == WeekDaysEnum.Monday}");
            Console.WriteLine($"CompareTo Enum (false): {WeekDaysEnum.Tuesday == WeekDaysEnum.Monday}");
            Console.WriteLine($"Val 1 < Val 2 (true): {WeekDaysEnum.Monday < WeekDaysEnum.Tuesday}");
            Console.WriteLine($"Val 1 > Val 2 (false): {"Mon" > WeekDaysEnum.Tuesday}");
            Console.WriteLine($"Cast ToInt: {WeekDaysEnum.ToInt(WeekDaysEnum.Monday)}");
            Console.WriteLine($"Cast (int): {(int) WeekDaysEnum.Monday}");
            Console.WriteLine($"Cast from (int): {(WeekDaysEnum) 1}");
            Console.WriteLine($"Cast (string): {(string) WeekDaysEnum.Monday}");
            Console.WriteLine($"Cast from (string): {(WeekDaysEnum) "Mon"}");
            Console.WriteLine($"Compare enums:{WeekDaysEnum.Monday == WeekDaysEnum.Monday}");

            Console.ReadKey();
        }

        public class ShapesEnum : StringEnumMap<ShapesEnum>
        {
            public static readonly ShapesEnum Point = new ShapesEnum(0,
                "Point");

            public static readonly ShapesEnum Line = new ShapesEnum(1,
                "Line",
                new List<string>
                {
                    "1D"
                });

            public static readonly ShapesEnum Sqaure = new ShapesEnum(2,
                "Sqaure",
                new List<string>
                {
                    "2D"
                });

            public ShapesEnum(int id, string stringRepresentation, List<string> otherPossibleRepresentation = null) :
                base(id, stringRepresentation, otherPossibleRepresentation)
            {
            }

            public ShapesEnum()
            {
            }
        }

        public class WeekDaysEnum : StringEnumMap<WeekDaysEnum>
        {
            public static readonly WeekDaysEnum Monday = new WeekDaysEnum(1,
                "Monday",
                new List<string>
                {
                    "Mon",
                    "M"
                });

            public static readonly WeekDaysEnum Tuesday = new WeekDaysEnum(2,
                "Tuesday",
                new List<string>
                {
                    "Tue",
                    "Tu"
                });

            public static readonly WeekDaysEnum Wednesday = new WeekDaysEnum(3,
                "Wednesday",
                new List<string>
                {
                    "Wed",
                    "W"
                });

            public static readonly WeekDaysEnum Thursday = new WeekDaysEnum(4,
                "Thursday",
                new List<string>
                {
                    "Thu",
                    "Th"
                });

            public static readonly WeekDaysEnum Friday = new WeekDaysEnum(5,
                "Friday",
                new List<string>
                {
                    "Fri",
                    "F"
                });

            public static readonly WeekDaysEnum Saturday = new WeekDaysEnum(6,
                "Saturday",
                new List<string>
                {
                    "Sat",
                    "Sa"
                });

            public static readonly WeekDaysEnum Sunday = new WeekDaysEnum(7,
                "Sunday",
                new List<string>
                {
                    "Sun",
                    "Su"
                });


            public WeekDaysEnum(int id,
                string stringRepr,
                List<string> possibleStringRepr = null) : base(id, stringRepr, possibleStringRepr)
            {
            }

            public WeekDaysEnum()
            {
            }
        }
    }
}