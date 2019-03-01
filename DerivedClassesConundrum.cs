using System;

namespace Scripts
{

    public  class BaseProperty : Object
    {
        public int myInt = 1;


    }

    // An abstract class that can be inherited.
    public  class BaseClass
    {
        public BaseProperty myProperty = new BaseProperty();

        public BaseClass()
        {
            Console.WriteLine($"Base class: {myProperty.myInt}");
        }
    }

    public  class DerivedProperty : BaseProperty
    {
        public int myDerivedInt = 2;
    }

    // A sub type of abstract class which will be inherited.
    public  class DerivedClass : BaseClass
    {
        public new DerivedProperty myProperty = new DerivedProperty();

        public DerivedClass() : base()
        {

            Console.WriteLine($"Derived class: {myProperty.myInt}, {myProperty.myDerivedInt}");
        }
    }

    public class UseableDerivedProperty : DerivedProperty
    {
        public int myUseableInt = 3;

    }

    public class UseableDerivedClass : DerivedClass
    {
        public new UseableDerivedProperty myProperty = new UseableDerivedProperty();

        public UseableDerivedClass() : base()
        {
            Console.WriteLine($"Useable derived class: {myProperty.myInt}, {myProperty.myDerivedInt}, {myProperty.myUseableInt}");
        }
    }

    public class UseableBaseProperty : BaseProperty
    {
        public int myUseableInt = 2;
    }

    public class UseableBaseClass : BaseClass
    {
        public new UseableBaseProperty myProperty = new UseableBaseProperty();

        public UseableBaseClass() : base()
        {
            Console.WriteLine($"Useable base class: {myProperty.myInt}, {myProperty.myUseableInt}");
        }
    }

}