using System;
using System.Collections.Generic;

// Problem: How to create a base class with generics for MVC architecture?
// where all the MVC share basic code such as update view when model is set, vice versa.
namespace Scripts.DerivedClassesConundrum
{
    #region BaseClasses

    // Abstract definition of property.
    public abstract class BasePropertyContainer
    {
        public abstract void Print();
    }

    // This class needs to exists because C# generic inheritance is not smart.
    // E.g. If you sub-class from BaseClass<TProperty> where TProperty : BaseProperty
    // called DeriviedClass<DerivedProperty> where DerivedProperty is sub-classed from
    // BaseProperty. C# doesn't recognize DeriviedClass as a sub class of BaseClass hence
    // can't cast. You know who to blame.
    public abstract class BaseMVCContainer : object
    {
        // Want to store chlidren of any type derived from base class.
        public IList<BaseMVCContainer> children = new List<BaseMVCContainer>();

        public virtual void AddChild(BaseMVCContainer child)
        {
            if (children == null) children = new List<BaseMVCContainer>();

            children.Add(child);
        }

        public abstract void Print();

        public static UseableBaseClass Test()
        {
            var useableBaseClass = new UseableBaseClass();

            useableBaseClass.AddChild(new UseableDerivedClass());
            useableBaseClass.Print();

            useableBaseClass.Use();
            var useableDerivedClass = useableBaseClass.children[0] as UseableDerivedClass;
            useableDerivedClass.DerivedUse();

            return useableBaseClass;
        }
    }


    // --- Level 1 of Base classes. 
    public class BaseProperty : BasePropertyContainer
    {
        public int myInt = 1;

        public override void Print()
        {
            Console.WriteLine($"Base class: {GetType()} : {myInt}");
        }
    }

    // An abstract class from which all classes will inherit from. 
    // Uses generics to define type of properties.
    public abstract class BaseClass<TProperty> : BaseMVCContainer
        where TProperty : BaseProperty, new()
    {
        protected TProperty _property;

        public BaseClass()
        {
            _property = new TProperty();
        }

        public TProperty Property
        {
            get => _property;
            set => _property = value;
        }

        public override void Print()
        {
            Property.Print();
            foreach (var child in children) child.Print();
        }

        public abstract void Use();
    }


    // --- Level 2 of Base Classes and Properties.
    public abstract class DerivedProperty : BaseProperty
    {
        public int myDerivedInt = 2;

        public override void Print()
        {
            Console.WriteLine($"Derived class: {GetType()} : {myInt}, {myDerivedInt}");
        }
    }

    // A sub type of abstract class which will be inherited.
    public abstract class DerivedClass<TProperty> : BaseClass<TProperty>
        where TProperty : DerivedProperty, new()
    {
        public DerivedClass()
        {
            _property = new TProperty();
        }

        public abstract void DerivedUse();
    }

    #endregion

// Actual classes which will can be instantiated ie not abstract.

    #region Useable classes

    public class UseableDerivedProperty : DerivedProperty
    {
        public int myUseableInt = 3;

        public override void Print()
        {
            Console.WriteLine($"Useable derived class: {GetType()} : {myInt}, {myDerivedInt}, {myUseableInt}");
        }
    }

    public class UseableDerivedClass : DerivedClass<UseableDerivedProperty>
    {
        public UseableDerivedClass()
        {
            _property = new UseableDerivedProperty();
        }


        public override void DerivedUse()
        {
            Console.WriteLine($"{GetType()}: Class derived use action called.");
        }

        public override void Use()
        {
            Console.WriteLine($"{GetType()}: Class base use action called.");
        }
    }

    // --- Level 1 Useable classes.    
    public class UseableBaseProperty : BaseProperty
    {
        public int myUseableInt = 2;

        public override void Print()
        {
            Console.WriteLine($"Useable base class: {GetType()} : {myInt}, {myUseableInt}");
        }
    }

    public class UseableBaseClass : BaseClass<UseableBaseProperty>
    {
        public override void Use()
        {
            Console.WriteLine($"{GetType()}: Class base use action called.");
        }
    }

    #endregion
}