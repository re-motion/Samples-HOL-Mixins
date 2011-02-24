using System;
using System.Linq;
using System.Reflection;

namespace EqualsShowCase.InheritanceImplementation
{
  public class EquatableByValues<T> : IEquatable<T>
     where T : class
  {
     private static readonly FieldInfo[] s_targetFields = typeof (T).GetFields (
                  BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

    public bool Equals (T other)
    {
      if (other == null)
        return false;

      if (GetType () != other.GetType ())
        return false;

      for (int i = 0; i < s_targetFields.Length; i++)
      {
        object thisFieldValue = s_targetFields[i].GetValue (this);
        object otherFieldValue = s_targetFields[i].GetValue (other);

        if (!Equals (thisFieldValue, otherFieldValue))
          return false;
      }

      return true;
    }

    public override bool Equals (object obj)
    {
      return Equals (obj as T);
    }

    public override int GetHashCode ()
    {
      return s_targetFields.Aggregate (0, (current, t) => current ^ t.GetValue (this).GetHashCode ());
    }
  }

  //// Problem 1: can't have more than one base class like this, e.g. 
  //// public class Address : EquatableByValues<Address>, DisposableBase
  public class Address : EquatableByValues<Address>
  {
    public string City;
    public string ZipCode;
    public string Country;
  }

  //// Problem 2: can't derive class hierarchies from base class
  //// public class StreetAddress : Address // implements IEquatable<Address> instead of IEquatable<StreetAddress>!
  public class StreetAddress : Address
  {
    public string Street;
    public string StreetNumber;
  }
}