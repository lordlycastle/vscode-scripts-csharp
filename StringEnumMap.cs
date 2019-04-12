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

    public class Map<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>
    {
        private readonly Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private readonly Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

        public Map()
        {
            Forward = new Indexer<T1, T2>(_forward);
            Reverse = new Indexer<T2, T1>(_reverse);
        }

        public Indexer<T1, T2> Forward { get; }
        public Indexer<T2, T1> Reverse { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        {
            return _forward.GetEnumerator();
        }

        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _reverse.Add(t2, t1);
        }

        public class Indexer<T3, T4>
        {
            private readonly Dictionary<T3, T4> _dictionary;

            public Indexer(Dictionary<T3, T4> dictionary)
            {
                _dictionary = dictionary;
            }

            public T4 this[T3 index]
            {
                get => _dictionary[index];
                set => _dictionary[index] = value;
            }

            public bool Contains(T3 key)
            {
                return _dictionary.ContainsKey(key);
            }
        }
    }

    public abstract class Enumeration : IComparable
    {
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; }

        public int Id { get; }

        public int CompareTo(object other)
        {
            return Id.CompareTo(((Enumeration) other).Id);
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        // Other utility methods ... 
    }

    public class CardType : Enumeration
    {
        public static CardType Amex = new CardType(1, "Amex");
        public static CardType Visa = new CardType(2, "Visa");
        public static CardType MasterCard = new CardType(3, "MasterCard");

        public CardType(int id, string name)
            : base(id, name)
        {
        }
    }
}