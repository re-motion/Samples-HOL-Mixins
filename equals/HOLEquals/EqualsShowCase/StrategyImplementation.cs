using System;
using System.Reflection;

namespace EqualsShowCase.StrategyImplementation
{

public interface IEqualsStrategy
{
  bool CustomEquals(object first, object second);
  int CustomGetHashCode (object obj);
}

public class EquatableByValueStrategy<T> : IEqualsStrategy
{
  static FieldInfo[] s_targetFields = typeof(T).GetFields (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

  public bool CustomEquals (object first, object second)
  {
    if ((first == null) && (second == null))
      return true;

    if ((first == null) || (second == null))
      return false;

    if (first.GetType () != second.GetType ())
      return false;

    for (int i = 0; i < s_targetFields.Length; i++)
    {
      object thisFieldValue = s_targetFields[i].GetValue (first);
      object otherFieldValue = s_targetFields[i].GetValue (second);

      if (!Equals (thisFieldValue, otherFieldValue))
        return false;
    }

    return true;
  }

  public int CustomGetHashCode (object obj)
  {
    int i = 0;
    foreach (FieldInfo f in s_targetFields)
      i ^= f.GetValue (obj).GetHashCode ();
    return i;
  }
}

public class Address : IEquatable<Address>
{
  private IEqualsStrategy _equalsStrategy;

  public string City;
  public string ZipCode;
  public string Country;

  public Address () : this (new EquatableByValueStrategy<Address>())
  {}

  public Address (IEqualsStrategy equalsStrategy)
  {
    _equalsStrategy = equalsStrategy;
  }

  public override bool Equals (object obj)
  {
    return Equals (obj as Address);
  }

  public bool Equals (Address other)
  {
    return _equalsStrategy.CustomEquals(this, other);
  }

  public override int GetHashCode ()
  {
    return _equalsStrategy.CustomGetHashCode (this);
  }
}

public class StreetAddress : Address
{
  public StreetAddress () : this (new EquatableByValueStrategy<StreetAddress>()) 
  {}

  public StreetAddress (IEqualsStrategy equalsStrategy ) : base (equalsStrategy)
  {}

  public string Street;
  public string StreetNumber;
}

}
