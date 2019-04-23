using System;
using System.Collections.Generic;
using System.Data;
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

        [Test]
        public void TestFromStringCast()
        {
            Assert.AreSame(Monday, (WeekDaysEnum) "Monday");
            Assert.AreSame(Monday, (WeekDaysEnum) "Mon");
        }

        [Test]
        public void TestToStringCast()
        {
            Assert.AreEqual("Tuesday", (string) Tuesday);
        }

        [Test]
        public void TestFromIntCast()
        {
            Assert.AreEqual(Monday, (WeekDaysEnum) 1);
        }

        [Test]
        public void TestToIntCast()
        {
            Assert.AreEqual(2, (int) Tuesday);
        }

        [Test]
        public void TestToString()
        {
            Assert.AreEqual("Monday", Monday.ToString());
        }

        [Test]
        public void TestEquality()
        {
            Assert.IsTrue(Monday != Tuesday);
//            Assert.IsTrue(Monday == Monday);
            Assert.IsTrue(Monday == Monday);
            WeekDaysEnum nullEnum = null;
            Assert.IsTrue(nullEnum == (WeekDaysEnum) null);
            Assert.IsTrue(nullEnum != Monday);
        }

        [Test]
        public void TestEqualityString()
        {
            Assert.IsTrue("Mon" == Monday);
            Assert.IsTrue("Mon" != Tuesday);
            Assert.IsTrue(Tuesday == "Tuesday");
            Assert.IsTrue(Tuesday != "Mon");
            Assert.AreNotEqual(Monday, (string) null);
        }

        [Test]
        public void TestEqualityInt()
        {
            Assert.IsTrue(1 == Monday);
            Assert.IsTrue(Tuesday == 2);
            Assert.IsTrue(1 != Tuesday);
            Assert.IsTrue(Tuesday != 1);
        }
        

        [Test]
        public void TestCompare()
        {
            // The Assert.Greater/.Less result in an inconclusive test. No idea why.
            Assert.IsTrue(Monday < Tuesday);
            Assert.IsTrue(Wednesday > Tuesday);
            Assert.IsTrue(Wednesday > Tuesday);
            Assert.IsTrue("Wed" > Tuesday);
            Assert.IsTrue("Wed" < Thursday);
            Assert.IsTrue(Thursday < "Fri");
            Assert.IsTrue(Saturday > "Fri");
            Assert.IsTrue(7 > Saturday);
            Assert.IsTrue(5 < Saturday);
            Assert.IsTrue(Saturday < 7);
            Assert.IsTrue(Saturday > Tuesday);
            
            Assert.IsFalse(Monday > "Mon");
            Assert.IsFalse(Monday < 1);
        }

        [Test]
        public void TestHashCode()
        {
            Assert.AreEqual(Monday.GetHashCode(), ((WeekDaysEnum) "Mon").GetHashCode());
        }

        [Test]
        public void TestCast()
        {
            Assert.Throws<InvalidCastException>(() =>
            {
                WeekDaysEnum __ = (WeekDaysEnum) "Funday";
            });
        }

        [Test]
        public void TestGetAll()
        {
            Assert.AreEqual(7, WeekDaysEnum.GetAll().Count());
        }

        [Test]
        public void TestSameIdEnums()
        {
            // Test that creates another var with same ID.
            Assert.Throws<DuplicateNameException>(() => new WeekDaysEnum(1, "Not Monday"));
            // Test that tries to pass in null for string repr.
            Assert.Throws<TypeInitializationException>(() => new WeekDaysEnum(10, null));
            // Test that passes in non-unique key.
            Assert.Throws<DuplicateNameException>(() => new WeekDaysEnum(99, "Monday"));
            Assert.Throws<DuplicateNameException>(() => new WeekDaysEnum(99,
                "Someday",
                new List<string> {"Mon"}));
        }
        
        [Test]
        public void TestGetEnumerator()
        {
            var i = 1;
            foreach (WeekDaysEnum weekDaysEnum in WeekDaysEnum._)
            {
                Assert.AreEqual(i, (int) weekDaysEnum);
                i++;
            }

            i = 1;
            foreach (WeekDaysEnum weekDaysEnum in WeekDaysEnum.GetAll())
            {
                Assert.AreEqual(i, (int) weekDaysEnum);
                i++;
                
            }
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