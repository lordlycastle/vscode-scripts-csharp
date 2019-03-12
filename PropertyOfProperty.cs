using System;

namespace Scripts.Properties
{
    public class Property
    {
        private int myInt;

        private string myString = "";


        protected PropertyOfProperty propertyOfProperty = new PropertyOfProperty();

        // One possible solution. But it creates too much repeating code.
        public Action updatedAction;

        public int MyInt
        {
            get => myInt;
            set
            {
                updatedAction?.Invoke();
                myInt = value;
            }
        }

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
            Console.WriteLine(
                $"Property: {_property.MyInt}, child property: {_property.PropertyOfProperty.myInt}, {_property.PropertyOfProperty.myString}");
        }

        public static MyClass Test()
        {
            var classObj = new MyClass();
            // Initial values
            Console.WriteLine("Initial values.");
            classObj.Print();
            // Want the change method to invoke whenever a property is changed.
            Console.WriteLine("Changing values.");
            classObj.Property.MyInt = 1;
            classObj.Property.PropertyOfProperty.myInt = 1;
            classObj.Property.PropertyOfProperty.myString = "Updated.";
            classObj.Print();
            // This invokes the property but requires you to entirely replace the property.
            Console.WriteLine("Replacing properties.");
            classObj.Property = new Property {MyInt = 2};
            classObj.Property.PropertyOfProperty = new PropertyOfProperty {myInt = 2};
            classObj.Print();
            // Possible solution. This is kinda annoying but so are the other solutions.
            // Best of the bad options we got.
            var property = classObj.Property;
            property.MyInt = 3;
            classObj.Property = property;
            classObj.Print();
            // Another possible solution. But then user has to remember to call it and there's
            // no programmer who reads the docs until they run into issues. 
            classObj.Property.MyInt = 4;
            classObj.PropertyUpdated(classObj.Property);
            classObj.Print();


            return classObj;
        }
    }
}