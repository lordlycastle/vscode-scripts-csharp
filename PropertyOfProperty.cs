using System;

namespace Scripts.Properties
{
    public class Property
    {
        public int myInt;
        protected PropertyOfProperty propertyOfProperty = new PropertyOfProperty();

        public PropertyOfProperty PropertyOfProperty
        {
            get => propertyOfProperty;
            set
            {
                ChildPropertyUpdated(value);
                propertyOfProperty = value;
            }
        }

        public void ChildPropertyUpdated(PropertyOfProperty propertyOfProperty)
        {
            Console.WriteLine("Property of property updated.");
        }
    }

    public class PropertyOfProperty
    {
        public int myInt;
        public string myString = "Default";
    }

    public class MyClass
    {
        protected Property _property;

        public MyClass()
        {
            _property = new Property();
        }

        public Property Property
        {
            get => _property;
            set
            {
                PropertyUpdated(value);
                _property = value;
            }
        }

        public void PropertyUpdated(Property property)
        {
            Console.WriteLine("Property changed.");
        }

        public void Print()
        {
            Console.WriteLine($"Property: {_property.myInt}, child property: {_property.PropertyOfProperty.myInt}");
        }

        public static MyClass Test()
        {
            var classObj = new MyClass();
            classObj.Print();
            classObj.Property.myInt = 1;
            classObj.Property.PropertyOfProperty.myInt = 1;
            classObj.Print();
            classObj.Property = new Property();
            classObj.Property.PropertyOfProperty = new PropertyOfProperty();
            classObj.Print();

            return classObj;
        }
    }
}