using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Scripts
{
    /// <summary>
    ///     An abstract class which functions as string Enums.
    ///     Using it allows you to specify string value along with the usual int
    ///     for a enum option and convert between them.
    ///     It also allows you have potential string values. When converting to string
    ///     it will return the _stringRepresentation but any of the provided strings can
    ///     be used to convert from string to enum.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public abstract class StringEnumMap<TEnum> : IEnumerable<TEnum>, IComparable, IEquatable<TEnum>
        where TEnum : StringEnumMap<TEnum>, new()
    {
        /// <summary>
        ///     Dictionary with {this, this._stringRepresentation}.
        /// </summary>
        private static readonly Dictionary<StringEnumMap<TEnum>, string> _toString =
            new Dictionary<StringEnumMap<TEnum>, string>();

        /// <summary>
        ///     Dictionary with all {this._otherPossibleRepresentation, this}.
        /// </summary>
        private static readonly Dictionary<string, StringEnumMap<TEnum>> _fromString =
            new Dictionary<string, StringEnumMap<TEnum>>();

        /// <summary>
        ///     Dictionary with {this._id, this}.
        /// </summary>
        private static readonly Dictionary<int, StringEnumMap<TEnum>> _fromId =
            new Dictionary<int, StringEnumMap<TEnum>>();

        /// <summary>
        ///     Unique id of an enum. Used for IComparable.
        /// </summary>
        private readonly int _id;

        /// <summary>
        ///     All possible string representations which can be used to convert/parse from
        ///     string.
        /// </summary>
        private readonly List<string> _otherPossibleRepresentation;

        /// <summary>
        ///     Default string representation which is returned when converting to string.
        ///     It should be visually formatted i.e. not "MyEnum" but "My Enum".
        /// </summary>
        private readonly string _stringRepresentation;

        public StringEnumMap()
        {
            CachedType = GetType();
        }

        public StringEnumMap(int id, string stringRepresentation, List<string> otherPossibleRepresentation = null)
        {
            CachedType = GetType();
            _id = id;
            if (_fromId.ContainsKey(_id))
                throw new DuplicateNameException(
                    $"Cannot have two options with same id ({_id}). Used by: {_fromId[id]}. Tried to set to: {stringRepresentation}.");

            _fromId[_id] = this;
            // Store string repr to dictionary for access later.
            _stringRepresentation = stringRepresentation;
            if (!string.IsNullOrEmpty(_stringRepresentation))
            {
                // Store the enum to string repr.
                _toString[this] = _stringRepresentation;
                _fromString[_stringRepresentation] = this;
            }
            else
            {
                throw new TypeInitializationException(CachedType.ToString(),
                    new Exception("A string representation is required for elements."));
            }

            if (otherPossibleRepresentation != null)
            {
                // Store other possible strings reprs
                _otherPossibleRepresentation = otherPossibleRepresentation;
                foreach (var str in _otherPossibleRepresentation)
                {
                    if (_fromString.ContainsKey(str))
                        throw new TypeInitializationException(CachedType.ToString(),
                            new Exception($"Key: {str} is not unique. Also belongs to: {_fromString[str]}."));

                    _fromString[str] = this;
                }
            }
        }

        public Type CachedType { get; }

        /// <summary>
        ///     Static property pointing to singleton instance which is used in iterators and dictionary syntax.
        ///     E.g.
        ///     <code>
        /// foreach (var option in MyEnum._) {
        ///    Console.WriteLine(option);
        /// }
        /// </code>
        ///     <code>
        /// var myEnum = MyEnum._["My Enum"];
        /// // Same as:
        /// var myEnum = (MyEnum) "My Enum";
        /// </code>
        /// </summary>
        public static TEnum _ { get; } = new TEnum();

        public override string ToString()
        {
            return _stringRepresentation;
        }

        #region Static Method

        public static IEnumerable<TEnum> GetAll()
        {
            // To improve performance when there are many fields,
            // this could return _toString.Keys.
            // Currently it is creating a new IEnumerable every time.
            var fields = typeof(TEnum).GetFields(BindingFlags.Public
                                                 | BindingFlags.Static
                                                 | BindingFlags.DeclaredOnly);
            return fields.Select(field => field.GetValue(null)).Cast<TEnum>();
        }

        #endregion

        #region IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TEnum> GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        #endregion

        #region IComparable

        public int CompareTo(object obj)
        {
            return _id.CompareTo(((TEnum) obj)._id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = (TEnum) obj;
            if (other.CachedType != CachedType) return false;
            return Equals(other);
        }

        #endregion

        #region IEquatable

        public bool Equals(TEnum other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id == other._id;
        }

        public override int GetHashCode()
        {
            return _id;
        }

        #endregion

        #region Operators

        public static bool operator >(StringEnumMap<TEnum> lhs, StringEnumMap<TEnum> rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }

        public static bool operator <(StringEnumMap<TEnum> lhs, StringEnumMap<TEnum> rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }

        public static bool operator >(StringEnumMap<TEnum> lhs, string rhs)
        {
            return lhs > _[rhs];
        }

        public static bool operator <(StringEnumMap<TEnum> lhs, string rhs)
        {
            return lhs < _[rhs];
        }

        public static bool operator >(string lhs, StringEnumMap<TEnum> rhs)
        {
            return rhs > lhs;
        }

        public static bool operator <(string lhs, StringEnumMap<TEnum> rhs)
        {
            return rhs < lhs;
        }

        public static bool operator ==(StringEnumMap<TEnum> lhs, StringEnumMap<TEnum> rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null))
                return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;

            return lhs.Equals(rhs);
        }

        public static bool operator !=(StringEnumMap<TEnum> lhs, StringEnumMap<TEnum> rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator ==(StringEnumMap<TEnum> lhs, string rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null))
                return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;

            if (lhs._stringRepresentation.Equals(rhs))
                return true;
            if (lhs._otherPossibleRepresentation.Contains(rhs))
                return true;
            return false;
        }

        public static bool operator !=(StringEnumMap<TEnum> lhs, string rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator ==(string lhs, StringEnumMap<TEnum> rhs)
        {
            return rhs == lhs;
        }

        public static bool operator !=(string lhs, StringEnumMap<TEnum> rhs)
        {
            return !(lhs == rhs);
        }

        public TEnum this[string key] => _fromString[key] as TEnum;

        public TEnum this[int id] => _fromId[id] as TEnum;


        public static int ToInt(TEnum e)
        {
            return e._id;
        }

        public static implicit operator int(StringEnumMap<TEnum> _enum)
        {
            return _enum._id;
        }

        public static explicit operator StringEnumMap<TEnum>(int id)
        {
            if (_fromId.ContainsKey(id)) return _fromId[id];

            throw new InvalidCastException($"Could not find value with id: {id}");
        }

        public static explicit operator StringEnumMap<TEnum>(string stringRepr)
        {
            if (_fromString.ContainsKey(stringRepr)) return _fromString[stringRepr];
            throw new InvalidCastException($"Could not find value with string repr: {stringRepr}");
        }

        public static explicit operator string(StringEnumMap<TEnum> _enum)
        {
            return _enum._stringRepresentation;
        }

        #endregion
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