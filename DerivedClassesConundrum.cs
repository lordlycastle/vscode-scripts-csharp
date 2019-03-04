using System;
using System.Collections.Generic;

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
    public abstract class BaseClass<TProperty>
        where TProperty : BaseProperty
    {
        public TProperty myProperty;
        public List<BaseClass<TProperty>> childClasses = new List<BaseClass<TProperty>>();

        public BaseClass()
        {
            
        }

        public virtual void Print(TProperty property){
             property.Print();
            //  childProperties.ForEach(p => Print());
        }

        public virtual void AddChild(BaseClass<TProperty> child)
        {
            if (childClasses == null)
                childClasses = new List<BaseClass<TProperty>>();
            childClasses.Add(child);  
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
    public abstract class DerivedClass<TProperty> : BaseClass<TProperty>
    where TProperty : DerivedProperty
    {

        public DerivedClass() : base()
        {
            // base.Print(myProperty);
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

    public class UseableDerivedClass : DerivedClass<UseableDerivedProperty>
    {
        // public UseableDerivedProperty myProperty = new UseableDerivedProperty();

        public UseableDerivedClass() : base()
        {
            myProperty = new UseableDerivedProperty();
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

    public class UseableBaseClass : BaseClass<UseableBaseProperty>
    {
        // public UseableBaseProperty myProperty = new UseableBaseProperty();

        public UseableBaseClass() : base()
        {
            myProperty = new UseableBaseProperty();
            base.Print(myProperty);
            
        }


    }
#endregion

}