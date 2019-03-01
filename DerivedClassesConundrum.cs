using System;

namespace Scripts
{

#region BaseClasses
    

    public  class BaseProperty : Object
    {
        public int myInt = 1;

        public virtual void Print() {
            Console.WriteLine($"Base class: {GetType()} : {myInt}");
        }
    }

    // An abstract class that can be inherited.
    public  class BaseClass
    {
        public BaseProperty myProperty = new BaseProperty();

        public BaseClass()
        {
            
        }

        public virtual void Print(BaseProperty property){
             property.Print();
        }
    }

    public  class DerivedProperty : BaseProperty
    {
        public int myDerivedInt = 2;

        public override void Print(){
            Console.WriteLine($"Derived class: {GetType()} : {myInt}, {myDerivedInt}");
        }
    }

    // A sub type of abstract class which will be inherited.
    public  class DerivedClass : BaseClass
    {
        public new DerivedProperty myProperty = new DerivedProperty();

        public DerivedClass() : base()
        {
            
        }

        // public override void Print(BaseProperty property){
        //     property.Print();
        // }
    }
#endregion

#region Useable classes
    
    public class UseableDerivedProperty : DerivedProperty
    {
        public int myUseableInt = 3;

        public override void Print(){
            Console.WriteLine($"Useable derived class: {GetType()} : {myInt}, {myDerivedInt}, {myUseableInt}");
        }
    }

    public class UseableDerivedClass : DerivedClass
    {
        public new UseableDerivedProperty myProperty = new UseableDerivedProperty();

        public UseableDerivedClass() : base()
        {
            base.Print(myProperty);
        }
    }

    public class UseableBaseProperty : BaseProperty
    {
        public int myUseableInt = 2;

        public override void Print() {
            Console.WriteLine($"Useable base class: {GetType()} : {myInt}, {myUseableInt}");
        }
    }

    public class UseableBaseClass : BaseClass
    {
        public new UseableBaseProperty myProperty = new UseableBaseProperty();

        public UseableBaseClass() : base()
        {
            base.Print(myProperty);
        }


    }
#endregion

}