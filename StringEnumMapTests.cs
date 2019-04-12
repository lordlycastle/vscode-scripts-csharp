using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Scripts
{
    using NUnit.Framework;
    
    [TestFixture]
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

        [TestCase]
        public void TestFromStringCast()
        {
            Assert.AreSame(Monday, (WeekDaysEnum) "Monday");
            Assert.AreSame(Monday, (WeekDaysEnum) "Mon");
        }

        [TestCase]
        public void TestToStringCast()
        {
            Assert.AreEqual("Tuesday", (string) Tuesday);
        }

        [TestCase]
        public void TestFromIntCast()
        {
            Assert.AreEqual(Monday, (WeekDaysEnum) 1);
        }

        [TestCase]
        public void TestToIntCast()
        {
            Assert.AreEqual(2, (int) Tuesday);
        }

        [TestCase]
        public void TestToString()
        {
            Assert.AreEqual("Monday", Monday.ToString());
        }

        [TestCase]
        public void TestEquality()
        {
            Assert.IsTrue(Monday != Tuesday);
            Assert.IsTrue(Monday == Monday);
        }

        [TestCase]
        public void TestEqualityString()
        {
            Assert.IsTrue("Mon" == Monday);
            Assert.IsTrue(Tuesday == "Tuesday");
        }

        [TestCase]
        public void TestEqualityInt()
        {
            Assert.IsTrue(1 == Monday);
            Assert.IsTrue(Tuesday == 2);
        }

        [TestCase]
        public void TestCompare()
        {
            // The Assert.Greater/.Less result in an inconclusive test. No idea why.
            Assert.IsTrue(Monday < Tuesday);
            Assert.IsTrue(Wednesday > Tuesday);
            Assert.IsTrue(Wednesday > Tuesday);
            Assert.IsTrue("Wed" > Tuesday);
            Assert.IsTrue(Thursday < "Fri");
            Assert.IsTrue(7 > Saturday);
            Assert.IsTrue(Saturday < 7);
        }

        [TestCase]
        public void TestHashCode()
        {
            Assert.AreEqual(Monday.GetHashCode(), ((WeekDaysEnum) "Mon").GetHashCode());
        }

        [TestCase]
        public void TestCast()
        {
            var ex = Assert.Throws<InvalidCastException>(() =>
            {
                WeekDaysEnum funday = (WeekDaysEnum) "Funday";
            });
            Assert.That(ex.GetType() == typeof(InvalidCastException));
        }

        [TestCase]
        public void TestGetAll()
        {
            Assert.AreEqual(7, WeekDaysEnum.GetAll().Count());
        }
    }
    
    [TestFixture]
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
            "Square",
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
    
}